using EAIO.Module.Catalog.Domain.Entities;
using EAIO.Module.Catalog.Infrastructure;
using EAIO.Module.Catalog.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCatalogInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //using var scope = app.Services.CreateScope();
    //var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    //var penddingMigrations = await context.Database.GetPendingMigrationsAsync();
    //if (penddingMigrations != null && penddingMigrations.Count() > 0)
    //{
    //    await context.Database.MigrateAsync();
    //}
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    List<Product> products = [Product.Create(Guid.NewGuid(), "Sample Product 1", 10000),
                Product.Create(Guid.NewGuid(), "Sample Product 2", 25000),
                Product.Create(Guid.NewGuid(), "Sample Product 3", 50000),
                Product.Create(Guid.NewGuid(), "Sample Product 4", 75000),
                Product.Create(Guid.NewGuid(), "Sample Product 5", 100000)];
    context.Products.AddRange(products);
    await context.SaveChangesAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
