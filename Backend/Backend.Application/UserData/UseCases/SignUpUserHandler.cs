using Backend.Application.UserData.Dto;
using Backend.Application.UserData.IRepository;
using Backend.Domain.Entity;

namespace Backend.Application.UserData.UseCases;

/// <summary>
/// Регистрация пользователя
/// </summary>
public class SignUpUserHandler
{
    /// <summary>
    /// Репозиторий пользователя
    /// </summary>
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Обработчик авторизации
    /// </summary>
    private readonly AuthorityHandler _authorityHandler;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="userRepository">Репозиторий пользователя</param>
    /// <param name="authorityHandler">Обработчик авторизации</param>
    public SignUpUserHandler(IUserRepository userRepository, AuthorityHandler authorityHandler)
    {
        _userRepository = userRepository;
        _authorityHandler = authorityHandler;
    }

    /// <summary>
    /// Зарегистрировать пользователя
    /// </summary>
    /// <param name="entity">Пользователь</param>
    public async Task SignUpUserAsync(UserDto entity)
    {
        var password = _authorityHandler.HashAndSaltPassword(entity.Password);

        await _userRepository.CreateAsync(new User(
            entity.Id,
            entity.Name,
            entity.Email,
            password));
    }
}