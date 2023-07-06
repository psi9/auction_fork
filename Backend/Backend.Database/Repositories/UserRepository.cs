using Backend.Database.Interfaces;
using Backend.Domain.Entity;

namespace Backend.Database.Repositories;

public class UserRepository : IUserRepository {
    public async Task<bool> Create(User entity) {
        throw new NotImplementedException();
    }

    public async Task<User> Get(int id) {
        throw new NotImplementedException();
    }

    public async Task<List<User>> Select() {
        throw new NotImplementedException();
    }

    public async Task<bool> Delete(User entity) {
        throw new NotImplementedException();
    }

    public async Task<User> Update(User entity) {
        throw new NotImplementedException();
    }
}