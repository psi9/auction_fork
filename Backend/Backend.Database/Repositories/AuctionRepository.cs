using Backend.Database.Interfaces;
using Backend.Domain.Entity;

namespace Backend.Database.Repositories;

/// <summary>
/// Репозиторий Аукциона
/// </summary>
public class AuctionRepository : IAuctionRepository
{
    public async Task<bool> CreateAsync(Auction entity)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Auction>> SelectAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Auction entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Auction> UpdateAsync(Auction entity)
    {
        throw new NotImplementedException();
    }
}