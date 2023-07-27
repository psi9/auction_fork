using Backend.Application.Dto;
using Backend.Application.Interfaces;
using Backend.Domain.Entity;

namespace Backend.Application.UseCases.CreateItems;

public class CreateAuctionHandler
{
    private readonly IAuctionRepository _auctionRepository;

    public CreateAuctionHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public async Task CreateAuctionAsync(AuctionDto entity)
    {
        await _auctionRepository.CreateAsync(new Auction(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.DateStart,
            entity.DateEnd,
            entity.AuthorId,
            entity.State));
    }
}