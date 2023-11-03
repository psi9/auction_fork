using Backend.Domain.Entity;
using Backend.Domain.Enum;

namespace Backend.Application.LotData.Dto;

/// <summary>
/// Лот
/// </summary>
public class LotDto
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название лота
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Описание лота
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Уникальный идентификатор аукциона лота
    /// </summary>
    public Guid AuctionId { get; set; }

    /// <summary>
    /// Стартовая цена лота
    /// </summary>
    public decimal StartPrice { get; set; }

    /// <summary>
    /// Цена выкупа лота
    /// </summary>
    public decimal BuyoutPrice { get; set; }

    /// <summary>
    /// Шаг ставки лота
    /// </summary>
    public decimal BetStep { get; set; }

    /// <summary>
    /// Статус лота
    /// </summary>
    public State State { get; set; }

    /// <summary>
    /// Ставки лота
    /// </summary>
    public IEnumerable<Bet> Bets { get; set; } = new List<Bet>(); // todo и ниже еще тоже: Auto-property accessor 'Bets.get' is never used 

    /// <summary>
    /// Изображения лота
    /// </summary>
    public IEnumerable<object> Images { get; set; } = new List<object>();
}