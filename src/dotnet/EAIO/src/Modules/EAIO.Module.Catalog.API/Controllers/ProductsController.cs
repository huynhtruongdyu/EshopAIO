using EAIO.Module.Catalog.Domain.Entities;
using EAIO.Module.Catalog.Domain.Specifications;
using EAIO.Module.Catalog.Infrastructure.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace EAIO.Module.Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductRepository repository) : ControllerBase
    {
        // GET api/products — all products ordered by name (uses AllProductsOrderedByNameSpec)
        [HttpGet]
        [ProducesResponseType<IEnumerable<Product>>(200)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var spec = new AllProductsOrderedByNameSpec();
            var products = await repository.FindAsync(spec, ct);
            return Ok(products);
        }

        // GET api/products/{id}
        [HttpGet("{id:guid}")]
        [ProducesResponseType<Product>(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var product = await repository.FindByIdAsync(id, ct);
            return product is null ? NotFound() : Ok(product);
        }

        // GET api/products/search?name=keyword (uses ProductsByNameSpec)
        [HttpGet("search")]
        [ProducesResponseType<IEnumerable<Product>>(200)]
        public async Task<IActionResult> SearchByName([FromQuery] string name, CancellationToken ct)
        {
            var spec = new ProductsByNameSpec(name);
            var products = await repository.FindAsync(spec, ct);
            return Ok(products);
        }

        // GET api/products/by-price?min=10&max=100 (uses ProductsByPriceRangeSpec)
        [HttpGet("by-price")]
        [ProducesResponseType<IEnumerable<Product>>(200)]
        public async Task<IActionResult> GetByPriceRange(
            [FromQuery] decimal min = 0,
            [FromQuery] decimal max = decimal.MaxValue,
            CancellationToken ct = default)
        {
            var spec = new ProductsByPriceRangeSpec(min, max);
            var products = await repository.FindAsync(spec, ct);
            return Ok(products);
        }

        // GET api/products/paged?pageNumber=1&pageSize=10&name=optional (uses ProductsPagedSpec)
        [HttpGet("paged")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? name = null,
            CancellationToken ct = default)
        {
            var spec = new ProductsPagedSpec(pageNumber, pageSize, name);
            var items = await repository.FindAsync(spec, ct);
            var totalCount = await repository.CountAsync(
                string.IsNullOrWhiteSpace(name) ? null : p => p.Name.Contains(name), ct);

            return Ok(new { items, totalCount, pageNumber, pageSize });
        }
    }
}
