using System.Data;
using Backend.Application.Interfaces;
using Backend.Database.PostgreSQL;
using Backend.Domain.Entity;
using Backend.Domain.Enum;
using Npgsql;

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
        await _pgsqlHandler.ExecuteAsync("InsertAuction", command =>
        {
            using var cmd = new NpgsqlCommand(command.Key, command.Value);
            cmd.Parameters.AddWithValue("id", entity.Id);
            cmd.Parameters.AddWithValue("name", entity.Name!);
            cmd.Parameters.AddWithValue("description", entity.Description!);
            cmd.Parameters.AddWithValue("authorId", entity.AuthorId);

            return cmd;
        });

        return true;
    }

    /// <summary>
    /// Запрос на получение Аукциона
    /// </summary>
    /// <param name="id">Уникальный идентификатор аукциона</param>
    /// <returns>Аукцион</returns>
    public async Task<Auction?> SelectAsync(Guid id)
    {
        var auction = await _pgsqlHandler.ReadAsync<Auction>(id, "SelectAuction",
            dataReader => new Auction(
                dataReader.GetGuid("id"),
                dataReader.GetString("name"),
                dataReader.GetString("description"),
                dataReader.GetDateTime("dataStart"),
                dataReader.GetDateTime("dataEnd"),
                dataReader.GetGuid("authorId"),
                (State)dataReader.GetInt32("state")));

        return auction;
    }

    /// <summary>
    /// Запрос на получение списка Аукционов
    /// </summary>
    /// <returns>Список аукционов</returns>
    public async Task<IReadOnlyCollection<Auction>?> SelectManyAsync()
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

        return auctions != null ? new List<Auction>(auctions) : null;
    }

    public Task<IReadOnlyCollection<Auction>?> SelectManyByParameterAsync<K>(string parameterName, K parameter,
        string resourceName)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Запрос на обновление Аукциона
    /// </summary>
    /// <param name="entity">Аукцион</param>
    /// <returns>Аукцион</returns>
    public async Task<Auction> UpdateAsync(Auction entity)
    {
        await _pgsqlHandler.ExecuteAsync("UpdateAuction", command =>
        {
            using var cmd = new NpgsqlCommand(command.Key, command.Value);
            cmd.Parameters.AddWithValue("id", entity.Id);
            cmd.Parameters.AddWithValue("name", entity.Name!);
            cmd.Parameters.AddWithValue("description", entity.Description!);

            return cmd;
        });

        return entity;
    }

    /// <summary>
    /// Запрос на удаление Аукциона
    /// </summary>
    /// <param name="id">Уникальный идентификатор аукциона</param>
    /// <returns>True или False</returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        await _pgsqlHandler.ExecuteAsync("DeleteAuction", command =>
        {
            using var cmd = new NpgsqlCommand(command.Key, command.Value);
            cmd.Parameters.AddWithValue("id", id);

            return cmd;
        });

        return true;
    }
}