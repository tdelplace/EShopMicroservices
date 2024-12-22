﻿using Catalog.API.Products.CreateProduct;
using FluentValidation;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }

    internal class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        private readonly IDocumentSession _session = session;
        private readonly ILogger<DeleteProductCommandHandler> _logger = logger;

        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("DeleteProductCommandHandler.Handle called with {@Command}", command);

            _session.Delete<Product>(command.Id);
            await _session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}
