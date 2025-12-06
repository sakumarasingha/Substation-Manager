using SM.WebApi.Domain;

namespace SM.WebApi.Infrastructure;
public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task SaveChangesAsync();

    public Task<List<Transformer>> GetAllTransformersAsync();
}