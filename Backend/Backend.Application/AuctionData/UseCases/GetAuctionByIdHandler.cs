using Backend.Application.AuctionData.Dto;
using Backend.Application.AuctionData.IRepository;
using Backend.Application.LotData.Dto;

namespace Backend.Application.AuctionData.UseCases;

/// <summary>
/// Получение аукциона
/// </summary>
public class GetAuctionByIdHandler
{
    /// <summary>
    /// Репозиторий аукциона
    /// </summary>
    private readonly IAuctionRepository _auctionRepository;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="auctionRepository">Репозиторий аукциона</param>
    public GetAuctionByIdHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    /// <summary>
    /// Получить аукцион
    /// </summary>
    /// <param name="id">Уникальный идентификатор аукциона</param>
    /// <returns>Модель аукциона</returns>
    public async Task<AuctionDto> GetAuctionByIdAsync(Guid id)
    {
        var auction = await _auctionRepository.SelectAsync(id);

        var lotsDto = new List<LotDto>();
        var lots = auction.Lots;

        foreach (var lot in lots)
        {
            var imagesData = new List<object>();

            var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Backend.Images", lot.Value.Name);
            if (!Directory.Exists(imagesPath)) continue;

            var imageFiles = Directory.GetFiles(imagesPath);

            foreach (var imageFile in imageFiles)
            {
                var imageBytes = await File.ReadAllBytesAsync(imageFile);
                var base64Image = Convert.ToBase64String(imageBytes);
                var imageName = Path.GetFileName(imageFile);

                var imageData = new { name = imageName, data = base64Image };
                imagesData.Add(imageData);
            }

            lotsDto.Add(new LotDto()
            {
                Id = lot.Value.Id,
                Name = lot.Value.Name,
                Description = lot.Value.Description,
                StartPrice = lot.Value.StartPrice,
                BuyoutPrice = lot.Value.BuyoutPrice,
                BetStep = lot.Value.BetStep,
                State = lot.Value.State,
                Bets = lot.Value.Bets,
                Images = imagesData
            });
        }

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