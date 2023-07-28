using System.Data;
using Backend.Application.UserData.IRepository;
using Backend.Database.PostgreSQL;
using Backend.Domain.Entity;

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
        await _pgsqlHandler.ExecuteAsync("InsertUser",
            new KeyValuePair<string, object>("id", entity.Id),
            new KeyValuePair<string, object>("name", entity.Name),
            new KeyValuePair<string, object>("email", entity.Email),
            new KeyValuePair<string, object>("password", entity.Password));

        return true;
    }

    /// <summary>
    /// Запрос на получение пользователя
    /// </summary>
    /// <param name="id">Уникальный идентификатор пользователя</param>
    /// <returns>Пользователь</returns>
    public async Task<User> SelectAsync(Guid id)
    {
        return await _pgsqlHandler.ReadAsync<User>(
            "SelectUser",
            "id",
            id,
            dataReader => new User(
                dataReader.GetGuid("id"),
                dataReader.GetString("name"),
                dataReader.GetString("email"),
                dataReader.GetString("password")));
    }

    /// <summary>
    /// Запрос на получение списка Пользователей
    /// </summary>
    /// <returns>Список пользователей</returns>
    public async Task<IReadOnlyCollection<User>> SelectManyAsync()
    {
        return await _pgsqlHandler.ReadManyAsync<User>("SelectUsers",
            dataReader => new User(
                dataReader.GetGuid("id"),
                dataReader.GetString("name"),
                dataReader.GetString("email"),
                dataReader.GetString("password")));
    }

    /// <summary>
    /// Запрос на обновление Пользователя
    /// </summary>
    /// <param name="entity">Пользователь</param>
    /// <returns>Пользователь</returns>
    public async Task<bool> UpdateAsync(User entity)
    {
        await _pgsqlHandler.ExecuteAsync("UpdateUser",
            new KeyValuePair<string, object>("id", entity.Id),
            new KeyValuePair<string, object>("name", entity.Name),
            new KeyValuePair<string, object>("email", entity.Email),
            new KeyValuePair<string, object>("password", entity.Password));

        return true;
    }

    /// <summary>
    /// Запрос на удаление Пользователя
    /// </summary>
    /// <param name="id">Уникальный идентификатор пользователя</param>
    /// <returns>True или False</returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        await _pgsqlHandler.ExecuteAsync("DeleteUser",
            new KeyValuePair<string, object>("id", id));

        await _pgsqlHandler.ExecuteAsync("DeleteBet",
            new KeyValuePair<string, object>("userId", id));

        return true;
    }
}