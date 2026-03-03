using EAIO.Module.Catalog.Domain;
using EAIO.Module.Catalog.Domain.Entities;
using EAIO.Module.Catalog.Domain.Specifications;
using EAIO.Module.Catalog.Infrastructure.Abstraction;
using EAIO.Shared.Domain.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace EAIO.Module.Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductRepository repository) : ControllerBase
    {
        // GET api/products — all products ordered by name
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
            var result = await GetProductByIdAsync(id, ct);
            return result.IsFailure
                ? NotFound(new { result.Error.Code, result.Error.Description })
                : Ok(result.Value);
        }

        // GET api/products/search?name=keyword
        [HttpGet("search")]
        [ProducesResponseType<IEnumerable<Product>>(200)]
        public async Task<IActionResult> SearchByName([FromQuery] string name, CancellationToken ct)
        {
            var spec = new ProductsByNameSpec(name);
            var products = await repository.FindAsync(spec, ct);
            return Ok(products);
        }

        // GET api/products/by-price?min=10&max=100
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

        // GET api/products/paged?pageNumber=1&pageSize=10&name=optional
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

        // POST api/products
        [HttpPost]
        [ProducesResponseType<Product>(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken ct)
        {
            var result = CreateProduct(request);
            if (result.IsFailure)
                return BadRequest(new { result.Error.Code, result.Error.Description });

            await repository.AddAsync(result.Value, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        // PUT api/products/{id}
        [HttpPut("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequest request, CancellationToken ct)
        {
            var findResult = await GetProductByIdAsync(id, ct);
            if (findResult.IsFailure)
                return NotFound(new { findResult.Error.Code, findResult.Error.Description });

            var validationResult = ValidateProductUpdate(request);
            if (validationResult.IsFailure)
                return BadRequest(new { validationResult.Error.Code, validationResult.Error.Description });

            findResult.Value.Name = request.Name;
            findResult.Value.BasePrice = request.BasePrice;
            repository.Update(findResult.Value);

            return NoContent();
        }

        // DELETE api/products/{id}
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await GetProductByIdAsync(id, ct);
            if (result.IsFailure)
                return NotFound(new { result.Error.Code, result.Error.Description });

            repository.Remove(result.Value);
            return NoContent();
        }

        // ── Private helpers using Result pattern ─────────────────────

        private async Task<Result<Product>> GetProductByIdAsync(Guid id, CancellationToken ct)
        {
            var product = await repository.FindByIdAsync(id, ct);
            return product is null
                ? Result.Failure<Product>(ProductErrors.NotFound(id))
                : Result.Success(product);
        }

        private static Result<Product> CreateProduct(CreateProductRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return Result.Failure<Product>(ProductErrors.NameRequired);

            if (request.BasePrice < 0)
                return Result.Failure<Product>(ProductErrors.InvalidPrice);

            return Result.Success(Product.Create(Guid.NewGuid(), request.Name, request.BasePrice));
        }

        private static Result ValidateProductUpdate(UpdateProductRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return Result.Failure(ProductErrors.NameRequired);

            if (request.BasePrice < 0)
                return Result.Failure(ProductErrors.InvalidPrice);

            return Result.Success();
        }
    }

    // ── Request DTOs ─────────────────────────────────────────────────

    public record CreateProductRequest(string Name, decimal BasePrice);
    public record UpdateProductRequest(string Name, decimal BasePrice);
}
