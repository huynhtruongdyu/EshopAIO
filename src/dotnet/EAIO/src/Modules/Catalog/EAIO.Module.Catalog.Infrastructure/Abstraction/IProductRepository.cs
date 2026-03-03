using EAIO.Module.Catalog.Domain.Entities;
using EAIO.Shared.Infrastructure.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAIO.Module.Catalog.Infrastructure.Abstraction
{
    public interface IProductRepository:IGenericRepository<Product>
    {
    }
}
