﻿using System.Data;
using Backend.Application.LotData.IRepository;
using Backend.Database.PostgreSQL;
using Backend.Domain.Entity;
using Backend.Domain.Enum;

namespace Backend.Database.Repositories;

/// <summary>
/// Репозиторий Лота
/// </summary>
public class LotRepository : ILotRepository
{
    /// <summary>
    /// Обработчик запросов к базе данных
    /// </summary>
    private readonly PgsqlHandler _pgsqlHandler;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="pgsqlHandler">Обработчик запросов к базе данных</param>
    public LotRepository(PgsqlHandler pgsqlHandler)
    {
        _pgsqlHandler = pgsqlHandler;
    }

    /// <summary>
    /// Запрос на добавление Лота
    /// </summary>
    /// <param name="entity">Лот</param>
    /// <returns>True или False</returns>
    public async Task<bool> CreateAsync(Lot entity)
    {
        await _pgsqlHandler.ExecuteAsync("InsertLot",
            new KeyValuePair<string, object>("id", entity.Id),
            new KeyValuePair<string, object>("name", entity.Name),
            new KeyValuePair<string, object>("description", entity.Description),
            new KeyValuePair<string, object>("auctionId", entity.AuctionId),
            new KeyValuePair<string, object>("startPrice", entity.StartPrice),
            new KeyValuePair<string, object>("betStep", entity.BetStep),
            new KeyValuePair<string, object>("images", entity.Images));

        foreach (var image in entity.Images)
        {
            await _pgsqlHandler.ExecuteAsync("InsertImage",
                new KeyValuePair<string, object>("id", image.Id),
                new KeyValuePair<string, object>("lotId", image.LotId),
                new KeyValuePair<string, object>("path", image.Path!));
        }

        return true;
    }

    /// <summary>
    /// Запрос на получение Лота
    /// </summary>
    /// <param name="id">Уникальный идентификатор лота</param>
    /// <returns>Лот</returns>
    public async Task<Lot> SelectAsync(Guid id)
    {
        var lot = await _pgsqlHandler.ReadAsync<Lot>(
            "SelectLot",
            "id",
            id,
            dataReader => new Lot(
                dataReader.GetGuid("id"),
                dataReader.GetString("name"),
                dataReader.GetString("description"),
                dataReader.GetGuid("auctionId"),
                dataReader.GetDecimal("startPrice"),
                dataReader.GetDecimal("buyoutPrice"),
                dataReader.GetDecimal("betStep"),
                (State)dataReader.GetInt32("state")));

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

        lot.SetImages(images);
        lot.SetBets(bets);

        return lot;
    }

    /// <summary>
    /// Запрос на получение списка Лотов
    /// </summary>
    /// <returns>Список лотов</returns>
    public async Task<IReadOnlyCollection<Lot>> SelectManyAsync()
    {
        var lots = await _pgsqlHandler.ReadManyAsync<Lot>("SelectLots",
            dataReader => new Lot(
                dataReader.GetGuid("id"),
                dataReader.GetString("name"),
                dataReader.GetString("description"),
                dataReader.GetGuid("auctionId"),
                dataReader.GetDecimal("startPrice"),
                dataReader.GetDecimal("buyoutPrice"),
                dataReader.GetDecimal("betStep"),
                (State)dataReader.GetInt32("state")));

        var newLots = new List<Lot>();

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

            lot.SetImages(images);
            lot.SetBets(bets);

            newLots.Add(lot);
        }

        return newLots;
    }

    /// <summary>
    /// Запрос на обновление Лота
    /// </summary>
    /// <param name="entity">Лот</param>
    /// <returns>Лот</returns>
    public async Task<bool> UpdateAsync(Lot entity)
    {
        await _pgsqlHandler.ExecuteAsync("UpdateLot",
            new KeyValuePair<string, object>("id", entity.Id),
            new KeyValuePair<string, object>("name", entity.Name),
            new KeyValuePair<string, object>("description", entity.Description),
            new KeyValuePair<string, object>("betStep", entity.BetStep));

        await _pgsqlHandler.ExecuteAsync("DeleteImage",
            new KeyValuePair<string, object>("lotId", entity.Id));

        foreach (var image in entity.Images)
        {
            await _pgsqlHandler.ExecuteAsync("InsertImage",
                new KeyValuePair<string, object>("id", image.Id),
                new KeyValuePair<string, object>("lotId", image.LotId),
                new KeyValuePair<string, object>("path", image.Path!));
        }

        return true;
    }

    /// <summary>
    /// Запрос на удаление Лота
    /// </summary>
    /// <param name="id">Уникальный идентификатор лота</param>
    /// <returns>True или False</returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        await _pgsqlHandler.ExecuteAsync("DeleteLot",
            new KeyValuePair<string, object>("id", id));

        await _pgsqlHandler.ExecuteAsync("DeleteImage",
            new KeyValuePair<string, object>("lotId", id));

        await _pgsqlHandler.ExecuteAsync("DeleteBet",
            new KeyValuePair<string, object>("lotId", id));

        return true;
    }
}