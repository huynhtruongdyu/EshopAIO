using EAIO.Shared.Domain.Abstraction;
using EAIO.Shared.Infrastructure.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAIO.Shared.Infrastructure.Repositories
{
    public class GenericRepository<TEntity>(DbContext context) : IGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected DbSet<TEntity> dbSet = context.Set<TEntity>();

        public async Task<IEnumerable<TEntity>> FindAllAsync(CancellationToken cancellationToken = default)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbSet.FindAsync(id, cancellationToken);
        }
    }
}
