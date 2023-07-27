using Backend.Application.Interfaces;
using Backend.Domain.Enum;

namespace Backend.Application.UseCases.ChangeState;

public class ChangeLotStatusHandler
{
    private readonly IAuctionRepository _auctionRepository;

    public ChangeLotStatusHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public async Task ChangeLotStatusAsync(Guid auctionId, Guid lotId, State state)
    {
        var auction = await _auctionRepository.SelectAsync(auctionId);

        auction.ChangeLotStatus(lotId, state);

        await _auctionRepository.UpdateAsync(auction);
    }
}