using Backend.Application.Dto;
using Backend.Application.Interfaces;

namespace Backend.Application.UseCases.UpdateItems;

public class UpdateAuctionHandler
{
    private readonly IAuctionRepository _auctionRepository;

    public UpdateAuctionHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public async Task UpdateAuctionAsync(AuctionDto entity)
    {
        var auction = await _auctionRepository.SelectAsync(entity.Id);

        auction.UpdateInformation(entity.Name, entity.Description);

        await _auctionRepository.UpdateAsync(auction);
    }
}