using Microsoft.AspNetCore.Mvc;
using MinimalAPI.EndpointFilters;
using MinimalAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace MinimalAPI.RouteGroups
{
    public static class ProductsMapGroup
    {
        private static List<Product> products = new List<Product>()
        {
            new Product()
            {
                Id = 1,
                ProductName="Smart Phone"
            },
            new Product()
            {
                Id = 2,
                ProductName="Smart TV"
            }
        };

        public static RouteGroupBuilder ProductsAPI(this RouteGroupBuilder group)
        {
            //GET /products
            group.MapGet("/", (HttpContext context) =>
            {
                //1, xxxxxx
                //2, xxxxxx

                return Results.Json(products);
            });

            //GET /products/{id}
            group.MapGet("/{id:int}", (HttpContext context, int id) =>
            {
                Product? product = products.FirstOrDefault(temp => temp.Id == id);
                if (product == null)
                {
                    return Results.BadRequest(new { error = "Incorrect Product ID" });
                }

                return Results.Json(product);
            });

            //POST /products
            group.MapPost("/", (HttpContext context, Product product) =>
            {
                products.Add(product);
                return Results.Ok(new { message = "Product Added" });
            })
                .AddEndpointFilter<CustomEndpointFilter>();

            //PUT /products/{id}
            group.MapPut("/{id}", (HttpContext context, int id, [FromBody] Product product) =>
            {
                Product? productFromCollection = products.FirstOrDefault(temp => temp.Id == id);
                if (productFromCollection == null)
                {
                    return Results.BadRequest(new { error = "Incorrect Product ID" });
                }

                productFromCollection.ProductName = product.ProductName;

                return Results.Ok(new { message = "Product Updated" });
            });

            //DELETE /products/{id}
            group.MapDelete("/{id}", (HttpContext context, int id) =>
            {
                Product? productFromCollection = products.FirstOrDefault(temp => temp.Id == id);
                if (productFromCollection == null)
                {
                    return Results.ValidationProblem(new Dictionary<string, string[]> {
                        { "id", new string[] { "Incorrect Product ID" }
                        } });
                }

                products.Remove(productFromCollection);

                return Results.Ok(new { message = "Product Deleted" });
            });

            return group;
        }
    }
}
