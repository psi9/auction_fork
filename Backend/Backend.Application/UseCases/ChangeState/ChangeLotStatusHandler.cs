using Backend.Application.Interfaces;
using Backend.Domain.Enum;

namespace Backend.Application.UseCases.ChangeState;

/// <summary>
/// Смена статуса лота
/// </summary>
public class ChangeLotStatusHandler
{
    /// <summary>
    /// Репозиторий аукциона
    /// </summary>
    private readonly IAuctionRepository _auctionRepository;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="auctionRepository">Репозиторий аукциона</param>
    public ChangeLotStatusHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    /// <summary>
    /// Сменить статус лота
    /// </summary>
    /// <param name="auctionId">Уникальный идентификатор аукциона</param>
    /// <param name="lotId">Уникальный идентификатор лота</param>
    /// <param name="state">Новый статус</param>
    public async Task ChangeLotStatusAsync(Guid auctionId, Guid lotId, State state)
    {
        var auction = await _auctionRepository.SelectAsync(auctionId);

        auction.ChangeLotStatus(lotId, state);

        await _auctionRepository.UpdateAsync(auction);
    }
}