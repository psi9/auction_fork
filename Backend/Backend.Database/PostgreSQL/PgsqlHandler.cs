using Microsoft.Extensions.Options;
using Npgsql;

namespace Backend.Database.PostgreSQL;

/// <summary>
/// Обработчик запросов к базе данных
/// </summary>
public class PgsqlHandler
{
    /// <summary>
    /// Строка подключения
    /// </summary>
    private readonly string _connectionString;

    /// <summary>
    /// Основной путь к ресурсу скрипта
    /// </summary>
    private const string ResourcePath = "Backend.Database.PostgreSQL.Scripts.Auction.";

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="connectData">Данные класса со строкой подключения из файла конфигурации</param>
    public PgsqlHandler(IOptions<PgsqlConnection> connectData)
    {
        _connectionString = connectData.Value.GetConnectionString();
    }

    /// <summary>
    /// Выполнить запрос без возвращаемых данных
    /// </summary>
    /// <param name="resourceName">Имя скрипта</param>
    /// <param name="selector">Делегат, задающий NpgsqlCommand, необходимую для запроса</param>
    public async Task ExecuteAsync(string resourceName,
        Func<KeyValuePair<string, NpgsqlConnection>, NpgsqlCommand> selector)
    {
        var commandText = AssemblyReader.GetScript(ResourcePath + resourceName);

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = selector(new KeyValuePair<string, NpgsqlConnection>(commandText, connection));

        await command.ExecuteNonQueryAsync();
    }

    /// <summary>
    /// Выполнить запрос на чтение списка данных
    /// </summary>
    /// <param name="resourceName">Имя скрипта</param>
    /// <param name="selector">Делегат, задающий конструктор для создания сущности из прочтенных данных</param>
    /// <typeparam name="T">Сущность</typeparam>
    /// <returns>Список сущностей</returns>
    public async Task<IReadOnlyCollection<T>?> ReadManyAsync<T>(string resourceName, Func<NpgsqlDataReader, T> selector)
    {
        var commandText = AssemblyReader.GetScript(ResourcePath + resourceName);

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new NpgsqlCommand(commandText, connection);
        await using var dataReader = await command.ExecuteReaderAsync();

        IList<T>? entities = new List<T>();
        while (await dataReader.ReadAsync())
        {
            var entity = selector(dataReader);
            entities?.Add(entity);
        }

        return entities != null ? new List<T>(entities) : null;
    }

    /// <summary>
    /// Выполнить запрос на получение одной записи из базы данных
    /// </summary>
    /// <param name="id">Уникальный идентификатор сущности</param>
    /// <param name="resourceName">Имя скрипта</param>
    /// <param name="selector">Делегат, задающий конструктор для создания сущности из прочтенных данных</param>
    /// <typeparam name="T">Сущность</typeparam>
    /// <returns>Сущность</returns>
    public async Task<T> ReadAsync<T>(Guid id, string resourceName, Func<NpgsqlDataReader, T> selector)
    {
        var commandText = AssemblyReader.GetScript(ResourcePath + resourceName);

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new NpgsqlCommand(commandText, connection);
        command.Parameters.AddWithValue("id", id);

        await using var dataReader = await command.ExecuteReaderAsync();

        await dataReader.ReadAsync();
        var entity = selector(dataReader);

        return entity;
    }

    /// <summary>
    /// Выполнить запрос на получение списка записей из базы данных по внешнему ключу
    /// </summary>
    /// <param name="resourceName">Имя скрипта</param>
    /// <param name="commandValue">Данные для команды</param>
    /// <param name="entityTemplate">Делегат, задающий конструктор для создания сущности из прочтенных данных</param>
    /// <typeparam name="T">Сущность</typeparam>
    /// <typeparam name="K">Тип параметра для комманды</typeparam>
    /// <returns>Список сущностей</returns>
    public async Task<IReadOnlyCollection<T>?> ReadManyByParameterAsync<T, K>(string resourceName,
        KeyValuePair<string, K> commandValue, Func<NpgsqlDataReader, T> entityTemplate)
    {
        var commandText = AssemblyReader.GetScript(ResourcePath + resourceName);

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new NpgsqlCommand(commandText, connection);
        command.Parameters.AddWithValue(commandValue.Key, commandValue.Value!);

        await using var dataReader = await command.ExecuteReaderAsync();

        IList<T>? entities = new List<T>();
        while (await dataReader.ReadAsync())
        {
            var entity = entityTemplate(dataReader);
            entities?.Add(entity);
        }

        return entities != null ? new List<T>(entities) : null;
    }
}