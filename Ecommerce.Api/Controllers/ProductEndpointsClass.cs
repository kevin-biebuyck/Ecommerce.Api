using Microsoft.EntityFrameworkCore;
using Ecommerce.Api.Data;
namespace Ecommerce.Api.Controllers;

public static class ProductEndpointsClass
{
    public static void MapProductEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Product", async (ECommerceContext db) =>
        {
            return await db.Products.ToListAsync();
        })
        .WithName("GetAllProducts")
        .Produces<List<Product>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Product/{id}", async (Guid Id, ECommerceContext db) =>
        {
            return await db.Products.FindAsync(Id)
                is Product model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetProductById")
        .Produces<Product>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Product/{id}", async (Guid Id, Product product, ECommerceContext db) =>
        {
            var foundModel = await db.Products.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            //update model properties here

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateProduct")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Product/", async (Product product, ECommerceContext db) =>
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return Results.Created($"/Products/{product.Id}", product);
        })
        .WithName("CreateProduct")
        .Produces<Product>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Product/{id}", async (Guid Id, ECommerceContext db) =>
        {
            if (await db.Products.FindAsync(Id) is Product product)
            {
                db.Products.Remove(product);
                await db.SaveChangesAsync();
                return Results.Ok(product);
            }

            return Results.NotFound();
        })
        .WithName("DeleteProduct")
        .Produces<Product>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
