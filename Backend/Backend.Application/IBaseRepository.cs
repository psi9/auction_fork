namespace Backend.Application;

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
    public Task<bool> UpdateAsync(T entity);
}