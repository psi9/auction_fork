using Backend.Application.Interfaces;

namespace Backend.Application.UseCases.DeleteItems;

public class DeleteLotHandler
{
    private readonly ILotRepository _lotRepository;

    public DeleteLotHandler(ILotRepository lotRepository)
    {
        _lotRepository = lotRepository;
    }

    public async Task DeleteLotAsync(Guid id)
    {
        await _lotRepository.DeleteAsync(id);
    }
}