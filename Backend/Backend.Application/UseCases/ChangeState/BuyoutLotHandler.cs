using Backend.Application.Interfaces;

namespace Backend.Application.UseCases.ChangeState;

public class BuyoutLotHandler
{
    private readonly IAuctionRepository _auctionRepository;

    public BuyoutLotHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public async Task BuyoutLotAsync(Guid auctionId, Guid lotId)
    {
        var auction = await _auctionRepository.SelectAsync(auctionId);

        auction.BuyoutLot(lotId);

        await _auctionRepository.UpdateAsync(auction);
    }
}