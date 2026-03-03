using EAIO.Shared.Domain.Abstraction;
using EAIO.Shared.Infrastructure.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EAIO.Shared.Infrastructure.Repositories
{
    public class GenericRepository<TEntity>(DbContext context) : IGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected DbSet<TEntity> dbSet = context.Set<TEntity>();

        // ── Query by ID ──────────────────────────────────────────────

        public async Task<TEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbSet.FindAsync([id], cancellationToken);
        }

        // ── Query all ────────────────────────────────────────────────

        public async Task<IEnumerable<TEntity>> FindAllAsync(CancellationToken cancellationToken = default)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        // ── Query with predicate ─────────────────────────────────────

        public async Task<IEnumerable<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return await dbSet.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return await dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return await dbSet.AnyAsync(predicate, cancellationToken);
        }

        public async Task<int> CountAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            CancellationToken cancellationToken = default)
        {
            return predicate is null
                ? await dbSet.CountAsync(cancellationToken)
                : await dbSet.CountAsync(predicate, cancellationToken);
        }

        // ── Query with specification ─────────────────────────────────

        public async Task<IEnumerable<TEntity>> FindAsync(
            ISpecification<TEntity> specification,
            CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification).ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> FirstOrDefaultAsync(
            ISpecification<TEntity> specification,
            CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> CountAsync(
            ISpecification<TEntity> specification,
            CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification).CountAsync(cancellationToken);
        }

        // ── Pagination ───────────────────────────────────────────────

        public async Task<(IReadOnlyList<TEntity> Items, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = dbSet;

            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items.AsReadOnly(), totalCount);
        }

        // ── Create ───────────────────────────────────────────────────

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await dbSet.AddAsync(entity, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await dbSet.AddRangeAsync(entities, cancellationToken);
        }

        // ── Update ───────────────────────────────────────────────────

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
        }

        // ── Delete ───────────────────────────────────────────────────

        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }

        // ── Private helpers ──────────────────────────────────────────

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(dbSet.AsQueryable(), specification);
        }
    }
}
