using Backend.Database.Interfaces;
using Backend.Domain.Entity;

namespace Backend.Database.Repositories;

public class AuctionRepository : IAuctionRepository {
    public async Task<bool> Create(Auction entity) {
        throw new NotImplementedException();
    }

    public async Task<Auction> Get(int id) {
        throw new NotImplementedException();
    }

    public async Task<List<Auction>> Select() {
        throw new NotImplementedException();
    }

    public async Task<bool> Delete(Auction entity) {
        throw new NotImplementedException();
    }

    public async Task<Auction> Update(Auction entity) {
        throw new NotImplementedException();
    }
}