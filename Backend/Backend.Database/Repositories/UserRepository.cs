using System.Data;
using Backend.Application.Interfaces;
using Backend.Database.PostgreSQL;
using Backend.Domain.Entity;
using Npgsql;

namespace Backend.Database.Repositories;

/// <summary>
/// Репозиторий Пользователя
/// </summary>
public class UserRepository : IUserRepository
{
    /// <summary>
    /// Обработчик запросов к базе данных
    /// </summary>
    private readonly PgsqlHandler _pgsqlHandler;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="pgsqlHandler">Обработчик запросов к базе данных</param>
    public UserRepository(PgsqlHandler pgsqlHandler)
    {
        _pgsqlHandler = pgsqlHandler;
    }

    /// <summary>
    /// Запрос на добавление Пользователя
    /// </summary>
    /// <param name="entity">Пользователь</param>
    /// <returns>True или False</returns>
    public async Task<bool> CreateAsync(User entity)
    {
        await _pgsqlHandler.ExecuteAsync("InsertUser", command =>
        {
            using var cmd = new NpgsqlCommand(command.Key, command.Value);
            cmd.Parameters.AddWithValue("id", entity.Id);
            cmd.Parameters.AddWithValue("name", entity.Name);
            cmd.Parameters.AddWithValue("email", entity.Email);
            cmd.Parameters.AddWithValue("password", entity.Password);

            return cmd;
        });

        return true;
    }

    /// <summary>
    /// Запрос на получение пользователя
    /// </summary>
    /// <param name="id">Уникальный идентификатор пользователя</param>
    /// <returns>Пользователь</returns>
    public async Task<User> SelectAsync(Guid id)
    {
        var user = await _pgsqlHandler.ReadAsync<User>(
            "SelectUser",
            "id",
            id,
            dataReader => new User(
                dataReader.GetGuid("id"),
                dataReader.GetString("name"),
                dataReader.GetString("email"),
                dataReader.GetString("password")));

        return user;
    }

    /// <summary>
    /// Запрос на получение списка Пользователей
    /// </summary>
    /// <returns>Список пользователей</returns>
    public async Task<IReadOnlyCollection<User>> SelectManyAsync()
    {
        var users = await _pgsqlHandler.ReadManyAsync<User>("SelectUsers",
            dataReader => new User(
                dataReader.GetGuid("id"),
                dataReader.GetString("name"),
                dataReader.GetString("email"),
                dataReader.GetString("password")));

        return users;
    }

    public Task<IReadOnlyCollection<User>> SelectManyByParameterAsync(string resourceName,
        params KeyValuePair<string, object>[] commandParameters)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Запрос на обновление Пользователя
    /// </summary>
    /// <param name="entity">Пользователь</param>
    /// <returns>Пользователь</returns>
    public async Task<User> UpdateAsync(User entity)
    {
        await _pgsqlHandler.ExecuteAsync("UpdateUser", command =>
        {
            using var cmd = new NpgsqlCommand(command.Key, command.Value);
            cmd.Parameters.AddWithValue("id", entity.Id);
            cmd.Parameters.AddWithValue("name", entity.Name);
            cmd.Parameters.AddWithValue("email", entity.Email);
            cmd.Parameters.AddWithValue("password", entity.Password);

            return cmd;
        });

        return entity;
    }

    /// <summary>
    /// Запрос на удаление Пользователя
    /// </summary>
    /// <param name="id">Уникальный идентификатор пользователя</param>
    /// <returns>True или False</returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        await _pgsqlHandler.ExecuteAsync("DeleteUser", command =>
        {
            using var cmd = new NpgsqlCommand(command.Key, command.Value);
            cmd.Parameters.AddWithValue("id", id);

            return cmd;
        });

        return true;
    }
}