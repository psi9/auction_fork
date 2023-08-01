using Backend.Application.UserData.Dto;
using Backend.Application.UserData.IRepository;

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
    public SignInUserHandler(IUserRepository userRepository, AuthorityHandler authorityHandler)
    {
        _userRepository = userRepository;
        _authorityHandler = authorityHandler;
    }

    /// <summary>
    /// Авторизовать пользователя
    /// </summary>
    /// <param name="username">Имя пользователя</param>
    /// <param name="password"></param>
    public async Task<UserDto> SignInUserAsync(string username, string password)
    {
        var user = await _userRepository.SelectByNameAsync(username);

        if (!_authorityHandler.VerifyUserData(username, password, user))
            return new UserDto();

        var token = _authorityHandler.CreateToken(username);

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