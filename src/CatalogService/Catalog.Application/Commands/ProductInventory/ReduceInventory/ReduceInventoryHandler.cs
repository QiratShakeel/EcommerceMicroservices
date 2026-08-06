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
        private readonly IProductCommandRepository _repository;
        private readonly IProductQueries _queries;
        private readonly ILoggerService _logger;

        public ReduceInventoryHandler(IProductCommandRepository repository, ILoggerService logger, IProductQueries queries)
        {
            _repository = repository;
            _logger = logger;
            _queries = queries;
        }

        public async Task<Result> Handle(ReduceInventoryCommand cmd, CancellationToken ct)
        {
            _logger.LogInformation("Reduce Inventory Command Handler {orderitems}", cmd.items);
            var productIds = cmd.items.Select(x => x.ProductId).ToList();  
            var products = await _queries.GetByIdsAsync(productIds, ct);

            foreach (var item in cmd.items)
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                if (product == null)
                    return Result.Failure($"Product {item.ProductId} not found");

                product.ReduceInventory(item.Quantity); // domain logic inside entity
                //await _repository.UpdateAsync(product);
            }
            _logger.LogInformation("Reduce Inventory Command Handler {orderitems}", cmd.items);
            return Result.Success();
        }
    }
}
