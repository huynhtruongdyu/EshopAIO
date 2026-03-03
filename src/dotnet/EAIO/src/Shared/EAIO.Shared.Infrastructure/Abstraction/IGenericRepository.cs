using EAIO.Shared.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAIO.Shared.Infrastructure.Abstraction
{
    public interface IGenericRepository<TEntity>
        where TEntity : IEntity
    {
        Task<TEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> FindAllAsync(CancellationToken cancellationToken = default);
    }
}
