using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketEventSearch.Application.Abstractions.Persistence;

namespace TicketEventSearch.Infrastructure.Persistence;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _applicationDbContext;

    public BaseRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<TEntity[]> GetListAsync(int? offset = null, int? limit = null, Expression<Func<TEntity, bool>>? predicate = null,
        Expression<Func<TEntity, object>>? orderBy = null, bool? descending = null, CancellationToken cancellationToken = default)
    {
        var queryable = _applicationDbContext.Set<TEntity>().AsQueryable();

        if (predicate is not null)
        {
            queryable = queryable.Where(predicate);
        }

        if (orderBy is not null)
        {
            queryable = descending == true ? queryable.OrderByDescending(orderBy) : queryable.OrderBy(orderBy);
        }

        if (offset.HasValue)
        {
            queryable = queryable.Skip(offset.Value);
        }

        if (limit.HasValue)
        {
            queryable = queryable.Take(limit.Value);
        }

        return await queryable.ToArrayAsync(cancellationToken);
    }

    public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        var set = _applicationDbContext.Set<TEntity>();
        return predicate == null ? await set.SingleOrDefaultAsync(cancellationToken) : await set.SingleOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>>? predicate, CancellationToken cancellationToken = default)
    {
        var set = _applicationDbContext.Set<TEntity>();
        return predicate == null ? await set.SingleAsync(cancellationToken) : await set.SingleAsync(predicate, cancellationToken);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        var queryable = _applicationDbContext.Set<TEntity>().AsQueryable();

        if (predicate is not null)
        {
            queryable = queryable.Where(predicate);
        }

        return await queryable.CountAsync(cancellationToken);
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var set = _applicationDbContext.Set<TEntity>();
        await set.AddAsync(entity, cancellationToken);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var set = _applicationDbContext.Set<TEntity>();
        set.Update(entity);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<TEntity[]> UpdateRangeAsync(TEntity[] entities, CancellationToken cancellationToken = default)
    {
        var set = _applicationDbContext.Set<TEntity>();
        set.UpdateRange(entities);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        return entities;
    }

    public async Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var set = _applicationDbContext.Set<TEntity>();
        set.Remove(entity);
        return await _applicationDbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<TEntity[]> AddRangeAsync(TEntity[] entity, CancellationToken cancellationToken = default)
    {
        var set = _applicationDbContext.Set<TEntity>();
        await set.AddRangeAsync(entity, cancellationToken);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<bool> DeleteRangeAsync(TEntity[] entities, CancellationToken cancellationToken = default)
    {
        var set = _applicationDbContext.Set<TEntity>();
        set.RemoveRange(entities);
        return await _applicationDbContext.SaveChangesAsync(cancellationToken) == entities.Length;
    }
}
