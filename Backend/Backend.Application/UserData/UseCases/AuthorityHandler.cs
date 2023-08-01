using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Backend.Domain.Entity;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Application.UserData.UseCases;

/// <summary>
/// Обработчик авторизации
/// </summary>
public class AuthorityHandler
{
    /// <summary>
    /// Длина хеша
    /// </summary>
    private const int KeySize = 30;

    /// <summary>
    /// Количество итераций хеширования
    /// </summary>
    private const int Iterations = 350000;

    /// <summary>
    /// Соль для хеширования пароля
    /// </summary>
    private const string Salt = "ASdkjhikuj98210as2l3kai32io4i0";

    /// <summary>
    /// Алгоритм хеширования
    /// </summary>
    private readonly HashAlgorithmName _algorithmName = HashAlgorithmName.SHA256;

    /// <summary>
    /// Ассиметричный ключ для токена
    /// </summary>
    private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2";

    /// <summary>
    /// Захешировать пароль с солью
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <returns>Хеш</returns>
    public string HashAndSaltPassword(string password)
    {
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.ASCII.GetBytes(password),
            Encoding.ASCII.GetBytes(Salt),
            Iterations,
            _algorithmName,
            KeySize);

        return Convert.ToHexString(hash);
    }

    /// <summary>
    /// Валидация входа пользователя
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <param name="username">Имя пользователя</param>
    /// <param name="user">Пользователь</param>
    /// <returns>True или False</returns>
    public bool VerifyUserData(string username, string password, User user)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.ASCII.GetBytes(password),
            Encoding.ASCII.GetBytes(Salt),
            Iterations,
            _algorithmName,
            KeySize);

        if (!CryptographicOperations.FixedTimeEquals(
                hashToCompare, Convert.FromHexString(user.Password)))
            return false;

        if (username != user.Name)
            return false;

        return true;
    }

    /// <summary>
    /// Создать JWT токен
    /// </summary>
    /// <param name="username">Имя пользователя</param>
    /// <returns>Токен</returns>
    public string CreateToken(string username)
    {
        var symmetricKey = Encoding.ASCII.GetBytes(Secret);
        var tokenHandler = new JwtSecurityTokenHandler();

        var now = DateTime.UtcNow;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            }),

            Expires = now.AddDays(7),

            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(symmetricKey),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

        return token;
    }
}