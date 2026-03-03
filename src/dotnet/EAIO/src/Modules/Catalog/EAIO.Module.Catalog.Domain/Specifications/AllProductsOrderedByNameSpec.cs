using EAIO.Module.Catalog.Domain.Entities;
using EAIO.Shared.Domain.Abstraction;

namespace EAIO.Module.Catalog.Domain.Specifications
{
    /// <summary>
    /// Returns all products ordered by name.
    /// </summary>
    public class AllProductsOrderedByNameSpec : BaseSpecification<Product>
    {
        public AllProductsOrderedByNameSpec()
        {
            ApplyOrderBy(p => p.Name);
        }
    }
}
