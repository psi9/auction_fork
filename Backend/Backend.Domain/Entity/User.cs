namespace Backend.Domain.Entity;

/// <summary>
/// Пользователь
/// </summary>
public class User
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Почта пользователя
    /// </summary>
    public string Email { get; init; }

    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { get; init; }
}