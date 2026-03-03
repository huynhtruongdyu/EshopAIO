using EAIO.Module.Catalog.Domain.Entities;
using EAIO.Module.Catalog.Infrastructure.Abstraction;
using EAIO.Module.Catalog.Infrastructure.Contexts;
using EAIO.Shared.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAIO.Module.Catalog.Infrastructure.Repositories
{
    public class ProductRepository(CatalogDbContext context) : GenericRepository<Product>(context), IProductRepository
    {
    }
}
