using EAIO.Shared.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAIO.Module.Catalog.Domain.Entities
{
    public class Product : AuditableEntity
    {
        public string Name { get; set; } = default!;
        public decimal BasePrice { get; set; }

        public static Product Create(Guid id, string name, decimal basePrice)
        {
            var product = new Product()
            {
                Id = id,
                Name = name,
                BasePrice = basePrice
            };
            return product;
        }
    }
}
