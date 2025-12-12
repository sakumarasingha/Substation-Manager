using SM.WebApi.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SM.WebApi.Domain;
using System.Linq.Expressions;

public class EfRepository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _db;
    public EfRepository(AppDbContext db) => _db = db;


    public Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _db.Set<T>();

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return query.ToListAsync();
    }



    public Task<List<T>> GetManyAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _db.Set<T>().Where(predicate);

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return query.ToListAsync();
    }


    public async Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _db.Set<T>();

        // Apply includes
        foreach (var include in includes)
            query = query.Include(include);

        // Filter by key and return
        return await query.FirstOrDefaultAsync(t => EF.Property<Guid>(t, "Id") == id);
    }

    public Task AddAsync(T entity) { _db.Set<T>().Add(entity); return Task.CompletedTask; }
    public void Update(T entity) => _db.Set<T>().Update(entity);
    public void Remove(T entity) => _db.Set<T>().Remove(entity);
    public Task SaveChangesAsync() => _db.SaveChangesAsync();

}