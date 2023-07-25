using Backend.Domain.Entity;

namespace Backend.Application.Interfaces;

/// <summary>
/// Интерфейс репозитория Пользователя
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
}