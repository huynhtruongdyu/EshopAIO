using System.Linq.Expressions;

namespace EAIO.Shared.Domain.Abstraction
{
    public interface ISpecification<TEntity> where TEntity : IEntity
    {
        Expression<Func<TEntity, bool>>? Criteria { get; }
        List<Expression<Func<TEntity, object>>> Includes { get; }
        Expression<Func<TEntity, object>>? OrderBy { get; }
        Expression<Func<TEntity, object>>? OrderByDescending { get; }
        int? Skip { get; }
        int? Take { get; }
    }
}
