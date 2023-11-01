using Backend.Domain.Enum;

namespace Backend.Application.AuctionData.Dto;

/// <summary>
/// Класс для смены статуса аукциона
/// </summary>
public class ChangeStatusDto
{
    /// <summary>
    /// Уникальный идентификтор аукциона
    /// </summary>
    public Guid AuctionId { get; set; }

    /// <summary>
    /// Новый статус
    /// </summary>
    public State State { get; set; }
}