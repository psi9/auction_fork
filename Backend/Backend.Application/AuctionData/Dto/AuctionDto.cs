using Backend.Application.LotData.Dto;
using Backend.Domain.Enum;

namespace Backend.Application.AuctionData.Dto;

/// <summary>
/// Аукцион
/// </summary>
public class AuctionDto
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название аукциона
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Описание аукциона
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Начало аукциона
    /// </summary>
    public DateTime DateStart { get; set; }

    /// <summary>
    /// Завершение аукциона
    /// </summary>
    public DateTime DateEnd { get; set; }

    /// <summary>
    /// Уникальный идентификатор пользователя-создателя
    /// </summary>
    public Guid AuthorId { get; set; }

    /// <summary>
    /// Статус ставки
    /// </summary>
    public State State { get; set; }

    /// <summary>
    /// Лоты аукциона
    /// </summary>
    public IEnumerable<LotDto> Lots { get; set; } = new List<LotDto>();
}