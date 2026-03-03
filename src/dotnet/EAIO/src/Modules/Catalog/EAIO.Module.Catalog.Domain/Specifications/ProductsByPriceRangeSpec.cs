using EAIO.Module.Catalog.Domain.Entities;
using EAIO.Shared.Domain.Abstraction;

namespace EAIO.Module.Catalog.Domain.Specifications
{
    /// <summary>
    /// Filters products within a price range, ordered by price ascending.
    /// </summary>
    public class ProductsByPriceRangeSpec : BaseSpecification<Product>
    {
        public ProductsByPriceRangeSpec(decimal minPrice, decimal maxPrice)
            : base(p => p.BasePrice >= minPrice && p.BasePrice <= maxPrice)
        {
            ApplyOrderBy(p => p.BasePrice);
        }
    }
}
