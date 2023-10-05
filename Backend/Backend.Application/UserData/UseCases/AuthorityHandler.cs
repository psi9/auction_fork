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
    public int KeySize { get; set; }

    /// <summary>
    /// Количество итераций хеширования
    /// </summary>
    public int Iterations { get; set; }

    /// <summary>
    /// Соль для хеширования пароля
    /// </summary>
    public string Salt { get; set; } = string.Empty;

    /// <summary>
    /// Ассиметричный ключ для токена
    /// </summary>
    public string Secret { get; set; } = string.Empty;

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
            HashAlgorithmName.SHA256,
            KeySize);

        return Convert.ToHexString(hash);
    }

    /// <summary>
    /// Валидация входа пользователя
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <param name="email">Почта пользователя</param>
    /// <param name="user">Пользователь</param>
    /// <returns>True или False</returns>
    public bool VerifyUserData(string email, string password, User user)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.ASCII.GetBytes(password),
            Encoding.ASCII.GetBytes(Salt),
            Iterations,
            HashAlgorithmName.SHA256,
            KeySize);

        if (!CryptographicOperations.FixedTimeEquals(
                hashToCompare, Convert.FromHexString(user.Password)))
            return false;

        if (email != user.Email)
            return false;

        return true;
    }

    /// <summary>
    /// Создать JWT токен
    /// </summary>
    /// <param name="email">Почта пользователя</param>
    /// <returns>Токен</returns>
    public string CreateToken(string email)
    {
        var symmetricKey = Encoding.ASCII.GetBytes(Secret);
        var tokenHandler = new JwtSecurityTokenHandler();

        var now = DateTime.UtcNow;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, email)
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