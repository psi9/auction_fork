using Backend.Database.Interfaces;
using Backend.Domain.Entity;

namespace Backend.Database.Repositories;

public class BetRepository : IBetRepository {
    public async Task<bool> Create(Bet entity) {
        throw new NotImplementedException();
    }

    public async Task<Bet> Get(int id) {
        throw new NotImplementedException();
    }

    public async Task<List<Bet>> Select() {
        throw new NotImplementedException();
    }

    public async Task<bool> Delete(Bet entity) {
        throw new NotImplementedException();
    }

    public async Task<Bet> Update(Bet entity) {
        throw new NotImplementedException();
    }
}