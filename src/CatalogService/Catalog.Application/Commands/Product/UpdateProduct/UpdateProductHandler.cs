using Ecommerce.Catalog.Domain.Aggregates;
using BuildingBlocks.Shared.Infrastructure;
using Ecommerce.Catalog.Application.Interfaces;
using Ecommerce.Catalog.Domain.ValueObjects;
using MediatR;
using System.Threading;
using BuildingBlocks.Shared.Results;

namespace Ecommerce.Catalog.Application.Commands
{
    public class UpdatebProductHandler : IRequestHandler<UpdateProductCommand, Result<Guid>>
    {
        private readonly IProductRepository _repository;
        public UpdatebProductHandler(IProductRepository repository)
        {
            _repository = repository;
            //_mapper = mapper;
        }
        public async Task<Result<Guid>> Handle(UpdateProductCommand cmd, CancellationToken ct)
        {
            var product = await _repository.GetByIdAsync(cmd.ProductId, ct);
            if (product == null)
                throw new InvalidOperationException("Product Not Found");
            product.UpdateProduct(cmd.Name, new Money(cmd.Price), cmd.Desc);
            // Add categories if provided
            if (cmd.CategoryIds != null)
            {
                foreach (var catId in cmd.CategoryIds)
                    product.AddCategory(catId);
            }

            // Add images if provided
            if (cmd.Images != null)
            {
                foreach (var imgDto in cmd.Images)
                    product.AddImage(new ProductImage(imgDto.Url, imgDto.AltText, imgDto.FileType));
            }

            // Optionally, set inventory
            //product.SetInventory(new ProductInventory(0));
            await _repository.UpdateAsync(product, ct);
            return Result<Guid>.Success(product.Id);
        }
    }
}