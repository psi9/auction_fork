using Backend.Application.Interfaces;

namespace Backend.Application.UseCases.ChangeState;

public class SetDateStartAuctionHandler
{
    private readonly IAuctionRepository _auctionRepository;

    public SetDateStartAuctionHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public async Task SetDateStartAuctionAsync(Guid id)
    {
        var auction = await _auctionRepository.SelectAsync(id);

        auction.SetDateStart();

        await _auctionRepository.UpdateAsync(auction);
    }
}