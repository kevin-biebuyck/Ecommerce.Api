﻿using Microsoft.EntityFrameworkCore;
using Ecommerce.Api.Data;
namespace Ecommerce.Api.Controllers;

public static class ProductEndpointsClass
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/products", async (string? search, ECommerceContext db) =>
            {
                var query = db.Products.AsQueryable();

                if (search != null)
                    query = query.Where(p => p.Name.Contains(search));

                return await query.ToListAsync();
            })
        .WithName("GetAllProducts")
        .Produces<List<Product>>(StatusCodes.Status200OK);

        routes.MapGet("/api/products/{id}", async (Guid id, ECommerceContext db) =>
        {
            return await db.Products.FindAsync(id)
                is Product model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetProductById")
        .Produces<Product>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/products/{id}", async (Guid id, Product product, ECommerceContext db) =>
            {
                if (product.Price < 0)
                    return Results.BadRequest("The price cannot be under 0");

                var foundModel = await db.Products.FindAsync(id);

                if (foundModel is null)
                {
                    return Results.NotFound();
                }

                //update model properties here
                foundModel.Description = product.Description;
                foundModel.Name = product.Name;
                foundModel.Price = product.Price;

                await db.SaveChangesAsync();

                return Results.NoContent();
            })
        .WithName("UpdateProduct")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest);

        routes.MapPost("/api/products/", async (Product product, ECommerceContext db) =>
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return Results.Created($"/Products/{product.Id}", product);
        })
        .WithName("CreateProduct")
        .Produces<Product>(StatusCodes.Status201Created);

        routes.MapDelete("/api/products/{id}", async (Guid id, ECommerceContext db) =>
        {
            if (await db.Products.FindAsync(id) is Product product)
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
