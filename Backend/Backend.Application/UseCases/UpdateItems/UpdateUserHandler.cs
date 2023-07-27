using Backend.Application.Dto;
using Backend.Application.Interfaces;
using Backend.Domain.Entity;

namespace Backend.Application.UseCases.UpdateItems;

/// <summary>
/// Обновление пользователя
/// </summary>
public class UpdateUserHandler
{
    /// <summary>
    /// Репозиторий пользователя
    /// </summary>
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// .ctor 
    /// </summary>
    /// <param name="userRepository">Репозиторий пользователя</param>
    public UpdateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Обновить пользователя
    /// </summary>
    /// <param name="entity">Модель пользователя</param>
    public async Task UpdateUserAsync(UserDto entity)
    {
        await _userRepository.UpdateAsync(new User(
            entity.Id,
            entity.Name,
            entity.Email,
            entity.Password));
    }
}