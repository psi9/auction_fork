using Backend.Application.Dto;
using Backend.Application.Interfaces;
using Backend.Domain.Entity;

namespace Backend.Application.UseCases.CreateItems;

public class CreateUserHandler
{
    private readonly IUserRepository _userRepository;

    public CreateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task CreateUserAsync(UserDto entity)
    {
        await _userRepository.CreateAsync(new User(
            entity.Id,
            entity.Name,
            entity.Email,
            entity.Password));
    }
}