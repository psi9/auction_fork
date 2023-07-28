using System.Data;
using Backend.Application.AuctionData.IRepository;
using Backend.Database.PostgreSQL;
using Backend.Domain.Entity;
using Backend.Domain.Enum;

namespace Backend.Database.Repositories;

/// <summary>
/// Репозиторий Аукциона
/// </summary>
public class AuctionRepository : IAuctionRepository
{
    /// <summary>
    /// Обработчик запросов к базе данных
    /// </summary>
    private readonly PgsqlHandler _pgsqlHandler;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="pgsqlHandler">Обработчик запросов к базе данных</param>
    public AuctionRepository(PgsqlHandler pgsqlHandler)
    {
        _pgsqlHandler = pgsqlHandler;
    }

    /// <summary>
    /// Запрос на создание Аукциона
    /// </summary>
    /// <param name="entity">Аукцион</param>
    /// <returns>True или False</returns>
    public async Task<bool> CreateAsync(Auction entity)
    {
        await _pgsqlHandler.ExecuteAsync("InsertAuction",
            new KeyValuePair<string, object>("id", entity.Id),
            new KeyValuePair<string, object>("name", entity.Name!),
            new KeyValuePair<string, object>("description", entity.Description!),
            new KeyValuePair<string, object>("authorId", entity.AuthorId));

        return true;
    }

    /// <summary>
    /// Запрос на получение Аукциона
    /// </summary>
    /// <param name="id">Уникальный идентификатор аукциона</param>
    /// <returns>Аукцион</returns>
    public async Task<Auction> SelectAsync(Guid id)
    {
        var auction = await _pgsqlHandler.ReadAsync<Auction>(
            "SelectAuction",
            "id",
            id,
            dataReader => new Auction(
                dataReader.GetGuid("id"),
                dataReader.GetString("name"),
                dataReader.GetString("description"),
                dataReader.GetDateTime("dataStart"),
                dataReader.GetDateTime("dataEnd"),
                dataReader.GetGuid("authorId"),
                (State)dataReader.GetInt32("state")));

        var lots = await _pgsqlHandler.ReadManyByParameterAsync<Lot>(
            "SelectLotByAuction",
            dataReader => new Lot(
                dataReader.GetGuid("id"),
                dataReader.GetString("name"),
                dataReader.GetString("description"),
                dataReader.GetGuid("auctionId"),
                dataReader.GetDecimal("startPrice"),
                dataReader.GetDecimal("buyoutPrice"),
                dataReader.GetDecimal("betStep"),
                (State)dataReader.GetInt32("state")),
            new KeyValuePair<string, object>("auctionId", id));

        foreach (var lot in lots)
        {
            var bets = await _pgsqlHandler.ReadManyByParameterAsync(
                "SelectBetsByLot",
                dataReader => new Bet
                {
                    Id = dataReader.GetGuid("id"),
                    Value = dataReader.GetDecimal("value"),
                    LotId = dataReader.GetGuid("lotId"),
                    UserId = dataReader.GetGuid("userId"),
                    DateTime = dataReader.GetDateTime("dateTime")
                },
                new KeyValuePair<string, object>("lotId", lot.Id));

            var images = await _pgsqlHandler.ReadManyByParameterAsync(
                "SelectImagesByLot",
                dataReader => new Image
                {
                    Id = dataReader.GetGuid("id"),
                    LotId = dataReader.GetGuid("lotId"),
                    Path = dataReader.GetString("path")
                },
                new KeyValuePair<string, object>("lotId", lot.Id));

            auction.AddLot(lot, images, bets);
        }

        return auction;
    }

    /// <summary>
    /// Запрос на получение списка Аукционов
    /// </summary>
    /// <returns>Список аукционов</returns>
    public async Task<IReadOnlyCollection<Auction>> SelectManyAsync()
    {
        var auctions = await _pgsqlHandler.ReadManyAsync<Auction>("SelectAuctions",
            dataReader => new Auction(
                dataReader.GetGuid("id"),
                dataReader.GetString("name"),
                dataReader.GetString("description"),
                dataReader.GetDateTime("dataStart"),
                dataReader.GetDateTime("dataEnd"),
                dataReader.GetGuid("authorId"),
                (State)dataReader.GetInt32("state")));

        var newAuctions = new List<Auction>();

        foreach (var auction in auctions)
        {
            var lots = await _pgsqlHandler.ReadManyByParameterAsync<Lot>(
                "SelectLotByAuction",
                dataReader => new Lot(
                    dataReader.GetGuid("id"),
                    dataReader.GetString("name"),
                    dataReader.GetString("description"),
                    dataReader.GetGuid("auctionId"),
                    dataReader.GetDecimal("startPrice"),
                    dataReader.GetDecimal("buyoutPrice"),
                    dataReader.GetDecimal("betStep"),
                    (State)dataReader.GetInt32("state")),
                new KeyValuePair<string, object>("auctionId", auction.Id));

            foreach (var lot in lots)
            {
                var bets = await _pgsqlHandler.ReadManyByParameterAsync(
                    "SelectBetsByLot",
                    dataReader => new Bet
                    {
                        Id = dataReader.GetGuid("id"),
                        Value = dataReader.GetDecimal("value"),
                        LotId = dataReader.GetGuid("lotId"),
                        UserId = dataReader.GetGuid("userId"),
                        DateTime = dataReader.GetDateTime("dateTime")
                    },
                    new KeyValuePair<string, object>("lotId", lot.Id));

                var images = await _pgsqlHandler.ReadManyByParameterAsync(
                    "SelectImagesByLot",
                    dataReader => new Image
                    {
                        Id = dataReader.GetGuid("id"),
                        LotId = dataReader.GetGuid("lotId"),
                        Path = dataReader.GetString("path")
                    },
                    new KeyValuePair<string, object>("lotId", lot.Id));

                auction.AddLot(lot, images, bets);
                newAuctions.Add(auction);
            }
        }

        return newAuctions;
    }

    /// <summary>
    /// Запрос на обновление Аукциона
    /// </summary>
    /// <param name="entity">Аукцион</param>
    /// <returns>Аукцион</returns>
    public async Task<bool> UpdateAsync(Auction entity)
    {
        await _pgsqlHandler.ExecuteAsync("UpdateAuction",
            new KeyValuePair<string, object>("id", entity.Id),
            new KeyValuePair<string, object>("name", entity.Name!),
            new KeyValuePair<string, object>("description", entity.Description!));

        foreach (var lot in entity.Lots.Values)
        {
            await _pgsqlHandler.ExecuteAsync("UpdateLot",
                new KeyValuePair<string, object>("id", entity.Id),
                new KeyValuePair<string, object>("name", lot.Name),
                new KeyValuePair<string, object>("description", lot.Description),
                new KeyValuePair<string, object>("betStep", lot.BetStep));

            await _pgsqlHandler.ExecuteAsync("DeleteImage",
                new KeyValuePair<string, object>("lotId", lot.Id));

            foreach (var image in lot.Images)
            {
                await _pgsqlHandler.ExecuteAsync("InsertImage",
                    new KeyValuePair<string, object>("id", image.Id),
                    new KeyValuePair<string, object>("lotId", image.LotId),
                    new KeyValuePair<string, object>("path", image.Path!));
            }
        }

        return true;
    }

    /// <summary>
    /// Запрос на удаление Аукциона
    /// </summary>
    /// <param name="id">Уникальный идентификатор аукциона</param>
    /// <returns>True или False</returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        await _pgsqlHandler.ExecuteAsync("DeleteAuction",
            new KeyValuePair<string, object>("id", id));

        var lots = await _pgsqlHandler.ReadManyByParameterAsync<Lot>(
            "SelectLotByAuction",
            dataReader => new Lot(
                dataReader.GetGuid("id"),
                dataReader.GetString("name"),
                dataReader.GetString("description"),
                dataReader.GetGuid("auctionId"),
                dataReader.GetDecimal("startPrice"),
                dataReader.GetDecimal("buyoutPrice"),
                dataReader.GetDecimal("betStep"),
                (State)dataReader.GetInt32("state")),
            new KeyValuePair<string, object>("auctionId", id));

        foreach (var lot in lots)
        {
            await _pgsqlHandler.ExecuteAsync("DeleteImage",
                new KeyValuePair<string, object>("lotId", lot.Id));

            await _pgsqlHandler.ExecuteAsync("DeleteBet",
                new KeyValuePair<string, object>("lotId", lot.Id));
        }

        await _pgsqlHandler.ExecuteAsync("DeleteLot",
            new KeyValuePair<string, object>("auctionId", id));

        return true;
    }
}