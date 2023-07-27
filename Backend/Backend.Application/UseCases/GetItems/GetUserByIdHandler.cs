using Backend.Application.Dto;
using Backend.Application.Interfaces;

namespace Backend.Application.UseCases.GetItems;

/// <summary>
/// Получить пользователя
/// </summary>
public class GetUserByIdHandler
{
    /// <summary>
    /// Репозиторий пользователя
    /// </summary>
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="userRepository">Репозиторий пользователя</param>
    public GetUserByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Получить пользователя
    /// </summary>
    /// <param name="id">Уникальный идентификатор пользователя</param>
    /// <returns>Модель пользователя</returns>
    public async Task<UserDto> GetUserById(Guid id)
    {
        var user = await _userRepository.SelectAsync(id);

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password
        };
    }
}