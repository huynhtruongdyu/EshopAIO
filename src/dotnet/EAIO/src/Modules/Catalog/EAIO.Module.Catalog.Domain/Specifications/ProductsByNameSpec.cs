using EAIO.Module.Catalog.Domain.Entities;
using EAIO.Shared.Domain.Abstraction;

namespace EAIO.Module.Catalog.Domain.Specifications
{
    /// <summary>
    /// Searches products by name (case-insensitive contains), ordered by name.
    /// </summary>
    public class ProductsByNameSpec : BaseSpecification<Product>
    {
        public ProductsByNameSpec(string keyword)
            : base(p => p.Name.Contains(keyword))
        {
            ApplyOrderBy(p => p.Name);
        }
    }
}
