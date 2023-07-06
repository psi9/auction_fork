using Backend.Database.Interfaces;
using Backend.Domain.Entity;

namespace Backend.Database.Repositories;

public class LotRepository : ILotRepository {
    public async Task<bool> Create(Lot entity) {
        throw new NotImplementedException();
    }

    public async Task<Lot> Get(int id) {
        throw new NotImplementedException();
    }

    public async Task<List<Lot>> Select() {
        throw new NotImplementedException();
    }

    public async Task<bool> Delete(Lot entity) {
        throw new NotImplementedException();
    }

    public async Task<Lot> Update(Lot entity) {
        throw new NotImplementedException();
    }
}