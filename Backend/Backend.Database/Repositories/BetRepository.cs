using Backend.Database.Interfaces;
using Backend.Domain.Entity;

namespace Backend.Database.Repositories;

/// <summary>
/// Репозиторий Ставки
/// </summary>
public class BetRepository : IBetRepository
{
    public async Task<bool> CreateAsync(Bet entity)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Bet>> SelectAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Bet entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Bet> UpdateAsync(Bet entity)
    {
        throw new NotImplementedException();
    }
}