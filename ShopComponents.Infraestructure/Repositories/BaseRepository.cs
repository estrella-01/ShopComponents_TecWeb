using Microsoft.EntityFrameworkCore;
using ShopComponents.Infraestructure.Data;

namespace ShopComponents.Infraestructure.Repositories;

public class BaseRepository<TEntity> where TEntity : class
{
    protected readonly SistemaDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(SistemaDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public void Add(TEntity entity) => _dbSet.Add(entity);
    public void Update(TEntity entity) => _dbSet.Update(entity);
    public void Delete(TEntity entity) => _dbSet.Remove(entity);
}