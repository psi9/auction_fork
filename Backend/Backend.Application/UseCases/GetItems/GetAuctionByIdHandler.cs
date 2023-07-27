using Backend.Application.Dto;
using Backend.Application.Interfaces;

namespace Backend.Application.UseCases.GetItems;

public class GetAuctionByIdHandler
{
    private readonly IAuctionRepository _auctionRepository;

    public GetAuctionByIdHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public async Task<AuctionDto> GetAuctionByIdAsync(Guid id)
    {
        var auction = await _auctionRepository.SelectAsync(id);

        var lots = auction.Lots.Values;

        var lotsDto = (from lot in lots
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
                AuctionId = lot.AuctionId,
                StartPrice = lot.StartPrice,
                BuyoutPrice = lot.BuyoutPrice,
                BetStep = lot.BetStep,
                State = lot.State,
                Bets = betsDto,
                Images = imagesDto
            }).AsParallel().ToList();

        return new AuctionDto
        {
            Id = auction.Id,
            Name = auction.Name,
            Description = auction.Description,
            DateStart = auction.DateStart,
            DateEnd = auction.DateEnd,
            AuthorId = auction.AuthorId,
            State = auction.State,
            Lots = lotsDto
        };
    }
}