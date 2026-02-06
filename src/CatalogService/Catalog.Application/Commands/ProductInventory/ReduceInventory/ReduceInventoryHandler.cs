using BuildingBlocks.Shared.Behaviors.Logging;
using BuildingBlocks.Shared.Results;
using Ecommerce.Catalog.Application.Interfaces;
using Ecommerce.Catalog.Domain.Aggregates;
using MediatR;
using Microsoft.Extensions.Logging;
namespace Ecommerce.Catalog.Application.Commands
{
    public class ReduceInventoryHandler : IRequestHandler<ReduceInventoryCommand, Result>
    {
        private readonly IProductRepository _repository;
        private readonly ILoggerService _logger;

        public ReduceInventoryHandler(IProductRepository repository, ILoggerService logger)
        {
            _repository = repository;
            _logger = logger; 
        }

        public async Task<Result> Handle(ReduceInventoryCommand cmd, CancellationToken ct)
        {
            _logger.LogInformation("Reduce Inventory Command Handler {orderitems}", cmd.items);
            var productIds = cmd.items.Select(x => x.ProductId).ToList();  
            var products = await _repository.GetByIdsAsync(productIds, ct);

            foreach (var item in cmd.items)
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                if (product == null)
                    return Result.Failure($"Product {item.ProductId} not found");

                product.ReduceInventory(item.Quantity); // domain logic inside entity
                await _repository.UpdateAsync(product, CancellationToken.None);
            }
            _logger.LogInformation("Reduce Inventory Command Handler {orderitems}", cmd.items);
            return Result.Success();
        }
    }
}
