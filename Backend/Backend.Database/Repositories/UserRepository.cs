using Backend.Database.Interfaces;
using Backend.Domain.Entity;

namespace Backend.Database.Repositories;

/// <summary>
/// Репозиторий Пользователя
/// </summary>
public class UserRepository : IUserRepository
{
    public async Task<bool> CreateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public async Task<List<User>> SelectAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public async Task<User> UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }
}