using Backend.Application.AuctionData.IRepository;

namespace Backend.Application.LotData.UseCases;

/// <summary>
/// Установка ставки
/// </summary>
public class DoBetHandler
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
    public DoBetHandler(IAuctionRepository auctionRepository, INotificationHandler notificationHandler)
    {
        _auctionRepository = auctionRepository;
        _notificationHandler = notificationHandler;
    }

    /// <summary>
    /// Сделать ставку
    /// </summary>
    /// <param name="auctionId">Уникальный идентификатор аукциона</param>
    /// <param name="lotId">Уникальный идентификатор лота</param>
    /// <param name="userId">Уникальный идентификатор пользователя</param>
    public async Task DoBetAsync(Guid auctionId, Guid lotId, Guid userId)
    {
        var auction = await _auctionRepository.SelectAsync(auctionId);

        auction.DoBet(lotId, userId);

        await _auctionRepository.UpdateAsync(auction);

        await _notificationHandler.MadeBetNoticeAsync();
    }
}