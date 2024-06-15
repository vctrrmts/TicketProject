using System.Linq.Expressions;

namespace TicketEventManagement.Application.Abstractions.Persistence;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity[]> GetListAsync(
        int? offset = null,
        int? limit = null,
        Expression<Func<TEntity, bool>>? expression = null,
        Expression<Func<TEntity, object>>? orderBy = null,
        bool? descending = null,
        CancellationToken cancellationToken = default);
    Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate, CancellationToken cancellationToken = default);
    Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>>? predicate, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? expression = null, CancellationToken cancellationToken = default);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity[]> AddRangeAsync(TEntity[] entity, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity[]> UpdateRangeAsync(TEntity[] entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteRangeAsync(TEntity[] entity, CancellationToken cancellationToken = default);
}
