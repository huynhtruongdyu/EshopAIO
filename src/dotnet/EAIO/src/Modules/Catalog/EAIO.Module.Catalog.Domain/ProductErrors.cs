using EAIO.Shared.Domain.Abstraction;

namespace EAIO.Module.Catalog.Domain
{
    /// <summary>
    /// Domain-specific errors for the Catalog module.
    /// </summary>
    public static class ProductErrors
    {
        public static Error NotFound(Guid productId) =>
            new("Product.NotFound", $"Product with ID '{productId}' was not found.");

        public static readonly Error NameRequired =
            new("Product.NameRequired", "Product name is required.");

        public static readonly Error InvalidPrice =
            new("Product.InvalidPrice", "Product price must be greater than or equal to zero.");
    }
}
