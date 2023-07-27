using Backend.Application.Interfaces;

namespace Backend.Application.UseCases.ChangeState;

/// <summary>
/// Установка ставки
/// </summary>
public class DoBetHandler
{
    /// <summary>
    /// Репозиторий аукциона
    /// </summary>
    private readonly IAuctionRepository _auctionRepository;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="auctionRepository">Репозиторий аукциона</param>
    public DoBetHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    /// <summary>
    /// Сделать ставку
    /// </summary>
    /// <param name="auctionId">Уникальный идентификатор аукциона</param>
    /// <param name="lotId">Уникальный идентификатор лота</param>
    /// <param name="userId">Уникальный идентификатор пользователя</param>
    public async Task DoBetAsync(Guid auctionId, Guid lotId, Guid userId)
    {
        var auction = await _auctionRepository.SelectAsync(auctionId);

        auction.DoBet(lotId, userId);

        await _auctionRepository.UpdateAsync(auction);
    }
}