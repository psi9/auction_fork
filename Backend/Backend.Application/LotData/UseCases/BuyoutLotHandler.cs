using Backend.Application.AuctionData.IRepository;

namespace Backend.Application.LotData.UseCases;

/// <summary>
/// Выкуп лота
/// </summary>
public class BuyoutLotHandler
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
    public BuyoutLotHandler(IAuctionRepository auctionRepository, INotificationHandler notificationHandler)
    {
        _auctionRepository = auctionRepository;
        _notificationHandler = notificationHandler;
    }

    /// <summary>
    /// Выкупить лот
    /// </summary>
    /// <param name="auctionId">Уникальный идентификатор аукциона</param>
    /// <param name="lotId">Уникальный идентификатор лота</param>
    public async Task BuyoutLotAsync(Guid auctionId, Guid lotId)
    {
        var auction = await _auctionRepository.SelectAsync(auctionId);

        auction.BuyoutLot(lotId);

        await _auctionRepository.UpdateAsync(auction);

        await _notificationHandler.SoldLotNoticeAsync();
    }
}