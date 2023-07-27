using Backend.Application.Dto;
using Backend.Application.Interfaces;
using Backend.Domain.Entity;

namespace Backend.Application.UseCases.UpdateItems;

public class UpdateLotHandler
{
    private readonly IAuctionRepository _auctionRepository;

    public UpdateLotHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public async Task UpdateLotAsync(LotDto entity)
    {
        var auction = await _auctionRepository.SelectAsync(entity.AuctionId);

        var lot = new Lot(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.AuctionId,
            entity.StartPrice,
            entity.BuyoutPrice,
            entity.BetStep,
            entity.State);

        var images = lot.Images.Select(image => new Image
        {
            Id = image.Id,
            LotId = image.LotId,
            Path = image.Path
        });

        auction.UpdateLot(lot.Id, lot.Name, lot.Description, lot.BetStep, images);

        await _auctionRepository.UpdateAsync(auction);
    }
}