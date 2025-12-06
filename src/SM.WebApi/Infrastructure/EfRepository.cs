using SM.WebApi.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SM.WebApi.Domain;

public class EfRepository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _db;
    public EfRepository(AppDbContext db) => _db = db;

    public Task<List<T>> GetAllAsync() => _db.Set<T>().ToListAsync();
    public Task<T?> GetByIdAsync(int id) => _db.Set<T>().FindAsync(id).AsTask();
    public Task AddAsync(T entity) { _db.Set<T>().Add(entity); return Task.CompletedTask; }
    public void Update(T entity) => _db.Set<T>().Update(entity);
    public void Remove(T entity) => _db.Set<T>().Remove(entity);
    public Task SaveChangesAsync() => _db.SaveChangesAsync();

    public Task<List<Transformer>> GetAllTransformersAsync()
    => _db.Transformers
          .Include(t => t.Asset)
          .ToListAsync();

}