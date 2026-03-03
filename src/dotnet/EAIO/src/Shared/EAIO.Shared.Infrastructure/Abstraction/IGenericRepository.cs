using EAIO.Shared.Domain.Abstraction;
using System.Linq.Expressions;

namespace EAIO.Shared.Infrastructure.Abstraction
{
    public interface IGenericRepository<TEntity>
        where TEntity : IEntity
    {
        // ── Query by ID ──────────────────────────────────────────────
        Task<TEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);

        // ── Query all ────────────────────────────────────────────────
        Task<IEnumerable<TEntity>> FindAllAsync(CancellationToken cancellationToken = default);

        // ── Query with predicate ─────────────────────────────────────
        Task<IEnumerable<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);

        Task<TEntity?> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);

        Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);

        Task<int> CountAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            CancellationToken cancellationToken = default);

        // ── Query with specification ─────────────────────────────────
        Task<IEnumerable<TEntity>> FindAsync(
            ISpecification<TEntity> specification,
            CancellationToken cancellationToken = default);

        Task<TEntity?> FirstOrDefaultAsync(
            ISpecification<TEntity> specification,
            CancellationToken cancellationToken = default);

        Task<int> CountAsync(
            ISpecification<TEntity> specification,
            CancellationToken cancellationToken = default);

        // ── Pagination ───────────────────────────────────────────────
        Task<(IReadOnlyList<TEntity> Items, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null,
            CancellationToken cancellationToken = default);

        // ── Create ───────────────────────────────────────────────────
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        // ── Update ───────────────────────────────────────────────────
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);

        // ── Delete ───────────────────────────────────────────────────
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
