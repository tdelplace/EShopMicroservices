using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByIdRequest(string category);

    public record GetProductByIdResponse(IEnumerable<Product> Products);

    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var results = await sender.Send(new GetProductsByCategoryQuery(category));
                var response = results.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductByCategory")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product by Category")
            .WithDescription("Get Product By Category");
        }
    }
}
