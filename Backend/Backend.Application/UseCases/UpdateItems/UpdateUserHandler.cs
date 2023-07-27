using Backend.Application.Dto;
using Backend.Application.Interfaces;
using Backend.Domain.Entity;

namespace Backend.Application.UseCases.UpdateItems;

public class UpdateUserHandler
{
    private readonly IUserRepository _userRepository;

    public UpdateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task UpdateUserAsync(UserDto entity)
    {
        await _userRepository.UpdateAsync(new User(
            entity.Id,
            entity.Name,
            entity.Email,
            entity.Password));
    }
}