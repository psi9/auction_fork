namespace Backend.Database.Interfaces;

public interface IBaseRepository<T> {
    public Task<bool> Create(T entity);

    public Task<T> Get(int id);

    Task<List<T>> Select();

    public Task<bool> Delete(T entity);

    public Task<T> Update(T entity);
}