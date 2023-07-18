using Backend.Domain.Entity;

namespace Backend.Database.Interfaces;

/// <summary>
/// Интерфейс репозитория Пользователя
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
}