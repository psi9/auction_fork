using Backend.Application.Dto;
using Backend.Application.Interfaces;
using Backend.Domain.Entity;

namespace Backend.Application.UseCases.CreateItems;

/// <summary>
/// Создание пользователя
/// </summary>
public class CreateUserHandler
{
    /// <summary>
    /// Репозиторий пользователя
    /// </summary>
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="userRepository">Репозиторий пользователя</param>
    public CreateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Создать пользователя
    /// </summary>
    /// <param name="entity">Пользователь</param>
    public async Task CreateUserAsync(UserDto entity)
    {
        await _userRepository.CreateAsync(new User(
            entity.Id,
            entity.Name,
            entity.Email,
            entity.Password));
    }
}