using System.Linq.Expressions;

namespace EAIO.Shared.Domain.Abstraction
{
    public abstract class BaseSpecification<TEntity> : ISpecification<TEntity>
        where TEntity : IEntity
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; } = new();
        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }
        public int? Skip { get; private set; }
        public int? Take { get; private set; }

        protected BaseSpecification() { }

        protected BaseSpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        protected void AddCriteria(Expression<Func<TEntity, bool>> criteria) => Criteria = criteria;

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
            => Includes.Add(includeExpression);

        protected void ApplyOrderBy(Expression<Func<TEntity, object>> orderByExpression)
            => OrderBy = orderByExpression;

        protected void ApplyOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression)
            => OrderByDescending = orderByDescExpression;

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }
    }
}
