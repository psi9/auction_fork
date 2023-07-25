using System.Data;
using Backend.Application.Interfaces;
using Backend.Database.PostgreSQL;
using Backend.Domain.Entity;
using Npgsql;

namespace Backend.Database.Repositories;

/// <summary>
/// 
/// </summary>
public class ImageRepository : IImageRepository
{
    /// <summary>
    /// Обработчик запросов к базе данных
    /// </summary>
    private readonly PgsqlHandler _pgsqlHandler;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="pgsqlHandler">Обработчик запросов к базе данных</param>
    public ImageRepository(PgsqlHandler pgsqlHandler)
    {
        _pgsqlHandler = pgsqlHandler;
    }

    /// <summary>
    /// Запрос на добавление ставки
    /// </summary>
    /// <param name="entity">Ставка</param>
    /// <returns>True или False</returns>
    public async Task<bool> CreateAsync(Image entity)
    {
        await _pgsqlHandler.ExecuteAsync("InsertImage", command =>
        {
            using var cmd = new NpgsqlCommand(command.Key, command.Value);
            cmd.Parameters.AddWithValue("id", entity.Id);
            cmd.Parameters.AddWithValue("lotId", entity.LotId);
            cmd.Parameters.AddWithValue("path", entity.Path);

            return cmd;
        });

        return true;
    }

    /// <summary>
    /// Запрос на выбор ставки
    /// </summary>
    /// <param name="id">Уникальный идентификатор ставки</param>
    /// <returns>Ставка</returns>
    public async Task<Image?> SelectAsync(Guid id)
    {
        var image = await _pgsqlHandler.ReadAsync(id, "SelectImage",
            dataReader => new Image
            {
                Id = dataReader.GetGuid("id"),
                LotId = dataReader.GetGuid("lotId"),
                Path = dataReader.GetString("path")
            });

        return image;
    }

    /// <summary>
    /// Запрос на выбор ставки
    /// </summary>
    /// <returns>Ставка</returns>
    public async Task<IReadOnlyCollection<Image>?> SelectManyAsync()
    {
        var images = await _pgsqlHandler.ReadManyAsync("SelectImages",
            dataReader => new Image
            {
                Id = dataReader.GetGuid("id"),
                LotId = dataReader.GetGuid("lotId"),
                Path = dataReader.GetString("path")
            });

        return images != null ? new List<Image>(images) : null;
    }

    /// <summary>
    /// Запрос на выбор изображений по параметру
    /// </summary>
    /// <param name="parameterName">Название параметра поиска в базе данных</param>
    /// <param name="parameter">Параметр поиска</param>
    /// <param name="resourceName">Имя скрипта запроса</param>
    /// <typeparam name="K">Тип параметра поиска</typeparam>
    /// <returns>Список сущностей</returns>
    public async Task<IReadOnlyCollection<Image>?> SelectManyByParameterAsync<K>(string parameterName, K parameter,
        string resourceName)
    {
        var images = await _pgsqlHandler.ReadManyByParameterAsync<Image, K>(
            resourceName,
            new KeyValuePair<string, K>(parameterName, parameter),
            dataReader => new Image
            {
                Id = dataReader.GetGuid("id"),
                LotId = dataReader.GetGuid("lotId"),
                Path = dataReader.GetString("Path")
            });

        return images != null ? new List<Image>(images) : null;
    }

    public Task<Image> UpdateAsync(Image entity)
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
        await _pgsqlHandler.ExecuteAsync("DeleteImage", command =>
        {
            using var cmd = new NpgsqlCommand(command.Key, command.Value);
            cmd.Parameters.AddWithValue("id", id);

            return cmd;
        });

        return true;
    }
}