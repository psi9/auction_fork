using Backend.Application.Dto;
using Backend.Application.Interfaces;
using Backend.Domain.Entity;

namespace Backend.Application.UseCases.CreateItems;

/// <summary>
/// Создание лота
/// </summary>
public class CreateLotHandler
{
    /// <summary>
    /// Репозиторий лота
    /// </summary>
    private readonly ILotRepository _lotRepository;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="lotRepository">Репозиторий лота</param>
    public CreateLotHandler(ILotRepository lotRepository)
    {
        _lotRepository = lotRepository;
    }

    /// <summary>
    /// Создать лот
    /// </summary>
    /// <param name="entity">Лот</param>
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