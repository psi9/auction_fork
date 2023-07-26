namespace Backend.Application.Interfaces;

/// <summary>
/// Базовый репозиторий
/// </summary>
/// <typeparam name="T">Сущность</typeparam>
public interface IBaseRepository<T>
{
    /// <summary>
    /// Создать сущность
    /// </summary>
    /// <param name="entity">Создаваемая сущность</param>
    /// <returns>True или false</returns>
    public Task<bool> CreateAsync(T entity);

    /// <summary>
    /// Получить сущность
    /// </summary>
    /// <returns>Сушность</returns>
    public Task<T> SelectAsync(Guid id);

    /// <summary>
    /// Получить сущности
    /// </summary>
    /// <returns>Сушности</returns>
    public Task<IReadOnlyCollection<T>> SelectManyAsync();

    /// <summary>
    /// Получить сущности, выбраннные по параметру
    /// </summary>
    /// <param name="resourceName">Имя скрипта для поиска по параметру</param>
    /// <param
    ///     name="commandParameters">Массив параметров для команды
    ///             (string - Название параметра, object - Параметр)
    /// </param>
    /// <returns>Сушности</returns>
    public Task<IReadOnlyCollection<T>> SelectManyByParameterAsync(string resourceName,
        params KeyValuePair<string, object>[] commandParameters);

    /// <summary>
    /// Удаление сущности
    /// </summary>
    /// <param name="id">Уникальный идентификатор</param>
    /// <returns>True или False</returns>
    public Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// Обновление сущности
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>Сущность</returns>
    public Task<T> UpdateAsync(T entity);
}