﻿
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdRequest(Guid Id);

    public record GetProductByIdResponse(Product Product);

    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                var results = await sender.Send(new GetProductByIdQuery(id));
                var response = results.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductById")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product by Id")
            .WithDescription("Get Product By Id");
        }
    }
}
