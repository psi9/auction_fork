using Backend.Application.Dto;
using Backend.Application.Interfaces;

namespace Backend.Application.UseCases.GetItems;

/// <summary>
/// Получение списка аукционов
/// </summary>
public class GetAuctionsHandler
{
    /// <summary>
    /// Репозиторий аукциона
    /// </summary>
    private readonly IAuctionRepository _auctionRepository;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="auctionRepository">Репозиторий аукциона</param>
    public GetAuctionsHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    /// <summary>
    /// Получить список аукционов
    /// </summary>
    /// <returns>Список моделей аукциона</returns>
    public async Task<IReadOnlyCollection<AuctionDto>> GetAuctions()
    {
        var auctions = await _auctionRepository.SelectManyAsync();

        return (from auction in auctions
            let lots = auction.Lots.Values
            let lotsDto = (from lot in lots
                let bets = lot.Bets
                let betsDto = bets.Select(bet => new BetDto
                    {
                        Id = bet.Id,
                        Value = bet.Value,
                        LotId = bet.LotId,
                        UserId = bet.UserId,
                        DateTime = bet.DateTime
                    })
                    .AsParallel()
                    .ToList()
                let images = lot.Images
                let imagesDto = images.Select(image => new ImageDto
                    {
                        Id = image.Id,
                        LotId = image.LotId,
                        Path = image.Path
                    })
                    .AsParallel()
                    .ToList()
                select new LotDto
                {
                    Id = lot.Id,
                    Name = lot.Name,
                    Description = lot.Description,
                    StartPrice = lot.StartPrice,
                    BuyoutPrice = lot.BuyoutPrice,
                    BetStep = lot.BetStep,
                    State = lot.State,
                    Bets = betsDto,
                    Images = imagesDto
                }).AsParallel().ToList()
            select new AuctionDto
            {
                Id = auction.Id,
                Name = auction.Name,
                Description = auction.Description,
                DateStart = auction.DateStart,
                DateEnd = auction.DateEnd,
                AuthorId = auction.AuthorId,
                State = auction.State,
                Lots = lotsDto
            }).AsParallel().ToList();
    }
}