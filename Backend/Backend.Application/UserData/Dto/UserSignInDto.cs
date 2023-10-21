namespace Backend.Application.UserData.Dto;

/// <summary>
/// Регистрация пользователя
/// </summary>
public class UserSignInDto
{
    /// <summary>
    /// Почта пользователя
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { get; set; } = string.Empty;
}