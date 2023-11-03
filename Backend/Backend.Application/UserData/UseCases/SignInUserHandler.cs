using Backend.Application.UserData.Dto;
using Backend.Application.UserData.IRepository;
using Microsoft.AspNetCore.Http;
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
    /// Доступ к контексту запроса
    /// </summary>
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="userRepository">Репозиторий пользователя</param>
    /// <param name="authorityHandler">Обработчик авторизации</param>
    /// <param name="httpContextAccessor">Доступ к контексту запроса</param>
    public SignInUserHandler(IUserRepository userRepository, IOptions<AuthorityHandler> authorityHandler,
        IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _authorityHandler = authorityHandler.Value;
    }

    /// <summary>
    /// Авторизовать пользователя
    /// </summary>
    /// <param name="email">Почта пользователя</param>
    /// <param name="password">Пароль пользователя</param>
    public async Task<UserDto> SignInUserAsync(string email, string password)
    {
        var user = await _userRepository.SelectByNameAsync(email);

        if (!_authorityHandler.VerifyUserData(email, password, user)) // todo вот это выглядит как костыль. может лучше возвращать Result<UserDto> и показывать на фронте ошибку?
            return new UserDto();

        var token = _authorityHandler.CreateToken(email);

        _httpContextAccessor.HttpContext?.Response.Cookies.Append(".AspNet.Application.Id", token,
            new CookieOptions
            {
                MaxAge = TimeSpan.FromMinutes(60),
                HttpOnly = true,
                Secure = true
            });

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }
}