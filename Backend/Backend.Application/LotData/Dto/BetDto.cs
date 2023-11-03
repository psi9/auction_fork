namespace Backend.Application.LotData.Dto;

/// <summary>
/// Ставка
/// </summary>
public class BetDto // todo Class 'BetDto' is never used
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Значение ставки
    /// </summary>
    public decimal Value { get; set; }

    /// <summary>
    /// Уникальный идентификатор лота
    /// </summary>
    public Guid LotId { get; set; }

    /// <summary>
    /// Уникальный идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// VВремя ставки
    /// </summary>
    public DateTime DateTime { get; set; }
}