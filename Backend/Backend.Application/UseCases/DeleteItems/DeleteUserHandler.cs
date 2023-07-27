using Backend.Application.Interfaces;

namespace Backend.Application.UseCases.DeleteItems;

public class DeleteUserHandler
{
    private readonly IUserRepository _userRepository;

    public DeleteUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task DeleteUserAsync(Guid id)
    {
        await _userRepository.DeleteAsync(id);
    }
}