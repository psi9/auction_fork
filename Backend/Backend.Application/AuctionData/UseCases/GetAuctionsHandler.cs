using Backend.Application.AuctionData.Dto;
using Backend.Application.AuctionData.IRepository;
using Backend.Application.LotData.Dto;

namespace Backend.Application.AuctionData.UseCases;

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
    // todo разбить на private методы
    // todo вижу, что у тебя здесь и в классе выше копипаста метода - надо вынести
    // может быть вообще нет смысла городить 2 разных класса, а сделать один хэндлер с двумя перегрузками: одна с параметром, другая без
    // обе перегрузки ходят в один и тот же private метод
    {
        var auctionsDto = new List<AuctionDto>();
        var auctions = await _auctionRepository.SelectManyAsync();

        foreach (var auction in auctions)
        {
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

            auctionsDto.Add(
                new AuctionDto
                {
                    Id = auction.Id,
                    Name = auction.Name,
                    Description = auction.Description,
                    DateStart = auction.DateStart,
                    DateEnd = auction.DateEnd,
                    AuthorId = auction.AuthorId,
                    State = auction.State,
                    Lots = lotsDto
                });
        }

        return auctionsDto;
    }
}