using System.Data;
using Backend.Application.Interfaces;
using Backend.Database.PostgreSQL;
using Backend.Domain.Entity;
using Npgsql;

namespace Backend.Database.Repositories;

/// <summary>
/// Репозиторий Ставки
/// </summary>
public class BetRepository : IBetRepository
{
    /// <summary>
    /// Обработчик запросов к базе данных
    /// </summary>
    private readonly PgsqlHandler _pgsqlHandler;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="pgsqlHandler">Обработчик запросов к базе данных</param>
    public BetRepository(PgsqlHandler pgsqlHandler)
    {
        _pgsqlHandler = pgsqlHandler;
    }

    /// <summary>
    /// Запрос на добавление ставки
    /// </summary>
    /// <param name="entity">Ставка</param>
    /// <returns>True или False</returns>
    public async Task<bool> CreateAsync(Bet entity)
    {
        await _pgsqlHandler.ExecuteAsync("InsertBet", command =>
        {
            using var cmd = new NpgsqlCommand(command.Key, command.Value);
            cmd.Parameters.AddWithValue("id", entity.Id);
            cmd.Parameters.AddWithValue("value", entity.Value);
            cmd.Parameters.AddWithValue("lotId", entity.LotId);
            cmd.Parameters.AddWithValue("userId", entity.UserId);
            cmd.Parameters.AddWithValue("dateTime", entity.DateTime);

            return cmd;
        });

        return true;
    }

    /// <summary>
    /// Запрос на выбор ставки
    /// </summary>
    /// <param name="id">Уникальный идентификатор ставки</param>
    /// <returns>Ставка</returns>
    public async Task<Bet> SelectAsync(Guid id)
    {
        var bet = await _pgsqlHandler.ReadAsync(
            "SelectBet",
            "id",
            id,
            dataReader => new Bet
            {
                Id = dataReader.GetGuid("id"),
                Value = dataReader.GetDecimal("value"),
                LotId = dataReader.GetGuid("lotId"),
                UserId = dataReader.GetGuid("userId"),
                DateTime = dataReader.GetDateTime("dateTime")
            });

        return bet;
    }

    /// <summary>
    /// Запрос на выбор ставки
    /// </summary>
    /// <returns>Ставка</returns>
    public async Task<IReadOnlyCollection<Bet>> SelectManyAsync()
    {
        var bets = await _pgsqlHandler.ReadManyAsync<Bet>("SelectBets",
            dataReader => new Bet
            {
                Id = dataReader.GetGuid("id"),
                Value = dataReader.GetDecimal("value"),
                LotId = dataReader.GetGuid("lotId"),
                UserId = dataReader.GetGuid("userId"),
                DateTime = dataReader.GetDateTime("dateTime")
            });

        return bets;
    }

    /// <summary>
    /// Запрос на выбор ставок по параметру
    /// </summary>
    /// <param name="resourceName">Имя скрипта запроса</param>
    /// <param
    ///     name="commandParameters">Массив параметров для команды
    ///             (string - Название параметра, object - Параметр)
    /// </param>    /// <returns>Список сущностей</returns>
    public async Task<IReadOnlyCollection<Bet>> SelectManyByParameterAsync(string resourceName,
        params KeyValuePair<string, object>[] commandParameters)
    {
        var bets = await _pgsqlHandler.ReadManyByParameterAsync<Bet>(
            resourceName,
            dataReader => new Bet
            {
                Id = dataReader.GetGuid("id"),
                Value = dataReader.GetDecimal("value"),
                LotId = dataReader.GetGuid("lotId"),
                UserId = dataReader.GetGuid("userId"),
                DateTime = dataReader.GetDateTime("dateTime")
            },
            commandParameters
        );

        return bets;
    }

    public Task<Bet> UpdateAsync(Bet entity)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Запрос на удаление Ставки
    /// </summary>
    /// <param name="id">Уникальный идентификатор ставки</param>
    /// <returns>True или False</returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        await _pgsqlHandler.ExecuteAsync("DeleteBet", command =>
        {
            using var cmd = new NpgsqlCommand(command.Key, command.Value);
            cmd.Parameters.AddWithValue("id", id);

            return cmd;
        });

        return true;
    }
}