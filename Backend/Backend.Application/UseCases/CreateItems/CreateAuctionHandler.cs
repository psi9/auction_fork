using Backend.Application.Dto;
using Backend.Application.Interfaces;
using Backend.Domain.Entity;

namespace Backend.Application.UseCases.CreateItems;

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