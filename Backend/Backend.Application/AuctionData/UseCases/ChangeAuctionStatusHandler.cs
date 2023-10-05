using Backend.Application.AuctionData.IRepository;
using Backend.Domain.Enum;

namespace Backend.Application.AuctionData.UseCases;

/// <summary>
/// Смена статуса аукциона
/// </summary>
public class ChangeAuctionStatusHandler
{
    /// <summary>
    /// Репозиторий аукциона
    /// </summary>
    private readonly IAuctionRepository _auctionRepository;

    /// <summary>
    /// Обработчик уведомлений
    /// </summary>
    private readonly INotificationHandler _notificationHandler;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="auctionRepository">Репозиторий аукциона</param>
    /// <param name="notificationHandler"></param>
    public ChangeAuctionStatusHandler(IAuctionRepository auctionRepository, INotificationHandler notificationHandler)
    {
        _auctionRepository = auctionRepository;
        _notificationHandler = notificationHandler;
    }

    /// <summary>
    /// Изменить статус аукциона
    /// </summary>
    /// <param name="id">Уникальный идентификатор аукциона</param>
    /// <param name="state">Новый статус</param>
    public async Task ChangeAuctionStatus(Guid id, State state)
    {
        var auction = await _auctionRepository.SelectAsync(id);

        auction.ChangeStatus(state);

        await _auctionRepository.UpdateAsync(auction);

        await _notificationHandler.ChangedAuctionStatusNoticeAsync();
    }
}