using Backend.Application.Interfaces;

namespace Backend.Application.UseCases.ChangeState;

public class DoBetHandler
{
    private readonly IAuctionRepository _auctionRepository;

    public DoBetHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public async Task DoBetAsync(Guid auctionId, Guid lotId, Guid userId)
    {
        var auction = await _auctionRepository.SelectAsync(auctionId);

        auction.DoBet(lotId, userId);

        await _auctionRepository.UpdateAsync(auction);
    }
}