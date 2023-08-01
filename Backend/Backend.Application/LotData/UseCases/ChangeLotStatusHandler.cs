using Backend.Application.AuctionData.IRepository;
using Backend.Domain.Enum;

namespace Backend.Application.LotData.UseCases;

/// <summary>
/// Смена статуса лота
/// </summary>
public class ChangeLotStatusHandler
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
    /// <param name="notificationHandler">Обработчик уведомлений</param>
    public ChangeLotStatusHandler(IAuctionRepository auctionRepository, INotificationHandler notificationHandler)
    {
        _auctionRepository = auctionRepository;
        _notificationHandler = notificationHandler;
    }

    /// <summary>
    /// Сменить статус лота
    /// </summary>
    /// <param name="auctionId">Уникальный идентификатор аукциона</param>
    /// <param name="lotId">Уникальный идентификатор лота</param>
    /// <param name="state">Новый статус</param>
    public async Task ChangeLotStatusAsync(Guid auctionId, Guid lotId, State state)
    {
        var auction = await _auctionRepository.SelectAsync(auctionId);

        auction.ChangeLotStatus(lotId, state);

        await _auctionRepository.UpdateAsync(auction);

        await _notificationHandler.ChangedLotStatusNoticeAsync();
    }
}