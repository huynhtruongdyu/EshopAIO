using EAIO.Shared.Domain.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace EAIO.Shared.Infrastructure.Repositories
{
    public static class SpecificationEvaluator<TEntity> where TEntity : class, IEntity
    {
        public static IQueryable<TEntity> GetQuery(
            IQueryable<TEntity> inputQuery,
            ISpecification<TEntity> specification)
        {
            var query = inputQuery;

            if (specification.Criteria is not null)
            {
                query = query.Where(specification.Criteria);
            }

            foreach (var include in specification.Includes)
            {
                query = query.Include(include);
            }

            if (specification.OrderBy is not null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.Skip.HasValue)
            {
                query = query.Skip(specification.Skip.Value);
            }

            if (specification.Take.HasValue)
            {
                query = query.Take(specification.Take.Value);
            }

            return query;
        }
    }
}
