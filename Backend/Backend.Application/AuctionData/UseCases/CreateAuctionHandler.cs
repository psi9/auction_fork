using Backend.Application.AuctionData.Dto;
using Backend.Application.AuctionData.IRepository;
using Backend.Domain.Entity;
using Backend.Domain.Enum;

namespace Backend.Application.AuctionData.UseCases;

/// <summary>
/// Создание аукциона
/// </summary>
public class CreateAuctionHandler
{
    /// <summary>
    /// Репозиторий аукциона
    /// </summary>
    private readonly IAuctionRepository _auctionRepository;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="auctionRepository">Репозиторий аукциона</param>
    public CreateAuctionHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    /// <summary>
    /// Создать аукцион
    /// </summary>
    /// <param name="entity">Аукцион</param>
    public async Task CreateAuctionAsync(CreateAuctionDto entity)
    {
        await _auctionRepository.CreateAsync(new Auction(
            Guid.NewGuid(),
            entity.Name,
            entity.Description,
            new DateTime(),
            new DateTime(), // todo это норм что тут 2 даты одинаковые? они ведь еще неизвестны в момент создания аукциона? может быть там NULL-ы должны быть?
                // к тому же у тебя в схеме БД эти 2 поля вот такие:  DEFAULT now() - зачем вообще тогда их пихать туда, БД же сама создаст
            entity.AuthorId,
            State.Awaiting));
    }
}