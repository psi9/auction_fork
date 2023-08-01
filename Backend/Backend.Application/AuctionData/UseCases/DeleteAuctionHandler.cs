using Backend.Application.AuctionData.IRepository;

namespace Backend.Application.AuctionData.UseCases;

/// <summary>
/// Удаление аукциона
/// </summary>
public class DeleteAuctionHandler
{
    /// <summary>
    /// Репозиторий аукциона
    /// </summary>
    private readonly IAuctionRepository _auctionRepository;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="auctionRepository">Репозиторий аукциона</param>
    public DeleteAuctionHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    /// <summary>
    /// Удалить пользователя
    /// </summary>
    /// <param name="id">Уникальный идентификатор аукциона</param>
    public async Task DeleteAuctionAsync(Guid id)
    {
        await _auctionRepository.DeleteAsync(id);
    }
}