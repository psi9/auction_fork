using Backend.Application.Interfaces;
using Backend.Domain.Enum;

namespace Backend.Application.UseCases.ChangeState;

public class ChangeAuctionStatusHandler
{
    private readonly IAuctionRepository _auctionRepository;

    public ChangeAuctionStatusHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public async Task ChangeAuctionStatus(Guid id, State state)
    {
        var auction = await _auctionRepository.SelectAsync(id);

        auction.ChangeStatus(state);

        await _auctionRepository.UpdateAsync(auction);
    }
}