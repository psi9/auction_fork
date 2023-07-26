using System.Data;
using Backend.Application.Interfaces;
using Backend.Database.PostgreSQL;
using Backend.Domain.Entity;
using Backend.Domain.Enum;
using Npgsql;

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
        await _pgsqlHandler.ExecuteAsync("InsertLot", command =>
        {
            using var cmd = new NpgsqlCommand(command.Key, command.Value);
            cmd.Parameters.AddWithValue("id", entity.Id);
            cmd.Parameters.AddWithValue("name", entity.Name);
            cmd.Parameters.AddWithValue("description", entity.Description);
            cmd.Parameters.AddWithValue("startPrice", entity.StartPrice);
            cmd.Parameters.AddWithValue("betStep", entity.BetStep);
            cmd.Parameters.AddWithValue("images", entity.Images);

            return cmd;
        });

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
                dataReader.GetDecimal("startPrice"),
                dataReader.GetDecimal("buyoutPrice"),
                dataReader.GetDecimal("betStep"),
                (State)dataReader.GetInt32("state")));

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
                dataReader.GetDecimal("startPrice"),
                dataReader.GetDecimal("buyoutPrice"),
                dataReader.GetDecimal("betStep"),
                (State)dataReader.GetInt32("state")));

        return lots;
    }

    public Task<IReadOnlyCollection<Lot>> SelectManyByParameterAsync(string resourceName,
        params KeyValuePair<string, object>[] commandParameters)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Запрос на обновление Лота
    /// </summary>
    /// <param name="entity">Лот</param>
    /// <returns>Лот</returns>
    public async Task<Lot> UpdateAsync(Lot entity)
    {
        await _pgsqlHandler.ExecuteAsync("UpdateLot", command =>
        {
            using var cmd = new NpgsqlCommand(command.Key, command.Value);
            cmd.Parameters.AddWithValue("id", entity.Id);
            cmd.Parameters.AddWithValue("name", entity.Name);
            cmd.Parameters.AddWithValue("description", entity.Description);
            cmd.Parameters.AddWithValue("betStep", entity.BetStep);
            cmd.Parameters.AddWithValue("images", entity.Images);

            return cmd;
        });

        return entity;
    }

    /// <summary>
    /// Запрос на удаление Лота
    /// </summary>
    /// <param name="id">Уникальный идентификатор лота</param>
    /// <returns>True или False</returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        await _pgsqlHandler.ExecuteAsync("DeleteLot", command =>
        {
            using var cmd = new NpgsqlCommand(command.Key, command.Value);
            cmd.Parameters.AddWithValue("id", id);

            return cmd;
        });

        return true;
    }
}