namespace Backend.Domain.Entity;

/// <summary>
/// Класс Изображения лота
/// </summary>
public class Image
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Уникальный идентификатор лота
    /// </summary>
    public Guid LotId { get; init; }

    /// <summary>
    /// Путь изображения
    /// </summary>
    public string? Path { get; init; }
}