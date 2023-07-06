namespace Backend.Database.Interfaces;

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
    /// <returns>Список сущностей</returns>
    Task<List<T>> SelectAsync();

    /// <summary>
    /// Удаление сущности
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>True или False</returns>
    public Task<bool> DeleteAsync(T entity);

    /// <summary>
    /// Обновление сущности
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>Сущность</returns>
    public Task<T> UpdateAsync(T entity);
}