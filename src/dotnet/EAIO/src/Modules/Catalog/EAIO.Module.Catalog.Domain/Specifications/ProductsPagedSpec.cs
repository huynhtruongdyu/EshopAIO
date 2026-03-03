using EAIO.Module.Catalog.Domain.Entities;
using EAIO.Shared.Domain.Abstraction;

namespace EAIO.Module.Catalog.Domain.Specifications
{
    /// <summary>
    /// Returns a paged list of products with optional name filter, ordered by name.
    /// </summary>
    public class ProductsPagedSpec : BaseSpecification<Product>
    {
        public ProductsPagedSpec(int pageNumber, int pageSize, string? nameFilter = null)
        {
            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                AddCriteria(p => p.Name.Contains(nameFilter));
            }

            ApplyOrderBy(p => p.Name);
            ApplyPaging(skip: (pageNumber - 1) * pageSize, take: pageSize);
        }
    }
}
