using Backend.Application.LotData.IRepository;

namespace Backend.Application.LotData.UseCases;

/// <summary>
/// Удаление лота
/// </summary>
public class DeleteLotHandler
{
    /// <summary>
    /// Репозиторий лота
    /// </summary>
    private readonly ILotRepository _lotRepository;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="lotRepository">Репозиторий лота</param>
    public DeleteLotHandler(ILotRepository lotRepository)
    {
        _lotRepository = lotRepository;
    }

    /// <summary>
    /// Удаление лота
    /// </summary>
    /// <param name="id">Уникальный идентификатор лота</param>
    public async Task DeleteLotAsync(Guid id)
    {
        await _lotRepository.DeleteAsync(id);
    }
}