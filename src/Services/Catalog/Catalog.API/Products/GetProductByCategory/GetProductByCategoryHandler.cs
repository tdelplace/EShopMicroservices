using Catalog.API.Products.GetProductById;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductsByCategoryQuery(string category) : IQuery<GetProductsByCategoryResult>;

    public record GetProductsByCategoryResult(IEnumerable<Product> Products);

    internal class GetProductsByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        private readonly IDocumentSession _session = session;
        private readonly ILogger<GetProductByIdQueryHandler> _logger = logger;

        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetProductByCategoryQueryHandler.Handle called with {@Query}", query);

            var products = await _session.Query<Product>().Where(p => p.Category.Contains(query.category)).ToListAsync(cancellationToken);

            return new GetProductsByCategoryResult(products);
        }
    }
}
