using EAIO.Module.Catalog.Domain.Entities;
using EAIO.Module.Catalog.Infrastructure.Abstraction;
using EAIO.Module.Catalog.Infrastructure.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EAIO.Module.Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductRepository repository) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<IEnumerable<Product>>(200)]
        public async Task<IActionResult> Get()
        {
            var products = await repository.FindAllAsync();
            return Ok(products ?? []);
        }
    }
}
