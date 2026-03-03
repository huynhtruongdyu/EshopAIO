using EAIO.Module.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAIO.Module.Catalog.Infrastructure.Configurations
{
    internal class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            //builder.HasData(
            //    Product.Create(Guid.NewGuid(), "Sample Product 1", 10000),
            //    Product.Create(Guid.NewGuid(), "Sample Product 2", 25000),
            //    Product.Create(Guid.NewGuid(), "Sample Product 3", 50000),
            //    Product.Create(Guid.NewGuid(), "Sample Product 4", 75000),
            //    Product.Create(Guid.NewGuid(), "Sample Product 5", 100000));
        }
    }
}
