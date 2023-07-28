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
    /// .ctor
    /// </summary>
    /// <param name="auctionRepository">Репозиторий аукциона</param>
    public BuyoutLotHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
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
    }
}