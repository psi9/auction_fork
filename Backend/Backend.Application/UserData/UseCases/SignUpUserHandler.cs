using Backend.Application.UserData.Dto;
using Backend.Application.UserData.IRepository;
using Backend.Domain.Entity;

namespace Backend.Application.UserData.UseCases;

/// <summary>
/// Создание пользователя
/// </summary>
public class SignUpUserHandler
{
    /// <summary>
    /// Репозиторий пользователя
    /// </summary>
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="userRepository">Репозиторий пользователя</param>
    public SignUpUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Создать пользователя
    /// </summary>
    /// <param name="entity">Пользователь</param>
    public async Task SignUpUserAsync(UserDto entity)
    {
        await _userRepository.CreateAsync(new User(
            entity.Id,
            entity.Name,
            entity.Email,
            entity.Password));
    }
}