using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.WebAPI.Data;

namespace Forum.WebAPI.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<TEntity>? GetAll()
    {
        return _context.Set<TEntity>().ToList();
    }

    public TEntity? GetById(int id)
    {
        return _context.Set<TEntity>().Find(id);
    }

    public void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }
}
