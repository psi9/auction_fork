using Backend.Application.Interfaces;

namespace Backend.Application.UseCases.ChangeState;

public class SetDateEndAuctionHandler
{
    private readonly IAuctionRepository _auctionRepository;

    public SetDateEndAuctionHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public async Task SetDateEndAuctionAsync(Guid id)
    {
        var auction = await _auctionRepository.SelectAsync(id);

        auction.SetDateEnd();

        await _auctionRepository.UpdateAsync(auction);
    }
}