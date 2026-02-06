using BuildingBlocks.Shared.Behaviors.Logging;
using BuildingBlocks.Shared.Grpc.Catalog;
using Ecommerce.Catalog.Application.Queries;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Net.NetworkInformation;

namespace Ecommerce.Catalog.API.Grpc
{
    public class CatalogGrpcService : CatalogGrpc.CatalogGrpcBase
    {
        private readonly IMediator _mediator;
        private readonly ILoggerService _logger;
        public CatalogGrpcService(IMediator mediator, ILoggerService logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public override async Task<ProductResponse> GetProductById(GetProductByIdRequest request, ServerCallContext context)
        {
            _logger.LogInformation("GetProductById called with ProductId: {ProductId}", request.ProductId);
            
            if (!Guid.TryParse(request.ProductId, out var productId))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "ProductId is not a valid GUID"));
            }

            var query = new GetProductByIdQuery(productId);
            var product = await _mediator.Send(query);

            if (product == null)
            {
                _logger.LogWarning("Product not found for ProductId: {ProductId}", request.ProductId);
                throw new RpcException(new Status(StatusCode.NotFound, "Product not found"));
            }

            return new ProductResponse
            {
                ProductId = product.Id.ToString(),
                Name = product.Name,
                Price = (double)product.Price,
                IsAvailable = product.inventory?.IsAvailable ?? false
            };
        }
    }
}