using Backend.Application.Dto;
using Backend.Application.Interfaces;
using Backend.Domain.Entity;

namespace Backend.Application.UseCases.CreateItems;

public class CreateLotHandler
{
    private readonly ILotRepository _lotRepository;

    public CreateLotHandler(ILotRepository lotRepository)
    {
        _lotRepository = lotRepository;
    }

    public async Task CreateLotAsync(LotDto entity)
    {
        var imagesDto = entity.Images;

        var images = imagesDto.Select(imageDto => new Image
        {
            Id = imageDto.Id,
            LotId = imageDto.LotId,
            Path = imageDto.Path
        });

        var lot = new Lot(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.AuctionId,
            entity.StartPrice,
            entity.BuyoutPrice,
            entity.BetStep,
            entity.State);

        lot.SetImages(images);

        await _lotRepository.CreateAsync(lot);
    }
}