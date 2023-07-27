using Backend.Application.Interfaces;

namespace Backend.Application.UseCases.DeleteItems;

public class DeleteAuctionHandler
{
    private readonly IAuctionRepository _auctionRepository;

    public DeleteAuctionHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public async Task DeleteAuctionAsync(Guid id)
    {
        await _auctionRepository.DeleteAsync(id);
    }
}