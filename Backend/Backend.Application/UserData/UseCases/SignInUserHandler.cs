using Backend.Application.UserData.Dto;
using Backend.Application.UserData.IRepository;
using Microsoft.Extensions.Options;

namespace Backend.Application.UserData.UseCases;

/// <summary>
/// Авторизация пользователя
/// </summary>
public class SignInUserHandler
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
    public SignInUserHandler(IUserRepository userRepository, IOptions<AuthorityHandler> authorityHandler)
    {
        _userRepository = userRepository;
        _authorityHandler = authorityHandler.Value;
    }

    /// <summary>
    /// Авторизовать пользователя
    /// </summary>
    /// <param name="email">Почта пользователя</param>
    /// <param name="password"></param>
    public async Task<UserDto> SignInUserAsync(string email, string password)
    {
        var user = await _userRepository.SelectByNameAsync(email);

        if (!_authorityHandler.VerifyUserData(email, password, user))
            return new UserDto();

        var token = _authorityHandler.CreateToken(email);

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            Token = token
        };
    }
}