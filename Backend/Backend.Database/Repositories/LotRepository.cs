using Backend.Database.Interfaces;
using Backend.Domain.Entity;

namespace Backend.Database.Repositories;

/// <summary>
/// Репозиторий Лота
/// </summary>
public class LotRepository : ILotRepository
{
    public async Task<bool> CreateAsync(Lot entity)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Lot>> SelectAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Lot entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Lot> UpdateAsync(Lot entity)
    {
        throw new NotImplementedException();
    }
}