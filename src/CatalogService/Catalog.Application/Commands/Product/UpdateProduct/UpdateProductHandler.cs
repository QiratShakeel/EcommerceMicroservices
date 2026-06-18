using BuildingBlocks.Shared.Exceptions;
using BuildingBlocks.Shared.Infrastructure;
using BuildingBlocks.Shared.Results;
using Ecommerce.Catalog.Application.Interfaces;
using Ecommerce.Catalog.Domain.Aggregates;
using Ecommerce.Catalog.Domain.ValueObjects;
using MediatR;
using System.Threading;

namespace Ecommerce.Catalog.Application.Commands
{
    public class UpdatebProductHandler : IRequestHandler<UpdateProductCommand, Result<Guid>>
    {
        private readonly IProductRepository _repository;
        private readonly IFileService _fileService;
        public UpdatebProductHandler(IProductRepository repository, IFileService fileService)
        {
            _repository = repository;
            //_mapper = mapper;
            _fileService= fileService;
        }
        public async Task<Result<Guid>> Handle(UpdateProductCommand cmd, CancellationToken ct)
        {
            try
            {
                var product = await _repository.GetByIdAsync(cmd.ProductId, ct);
                if (product == null)
                    throw new InvalidOperationException("Product Not Found");
                product.UpdateProduct(cmd.Name, new Money(cmd.Price), cmd.stock,cmd.Desc);
                // Add categories if provided
                if (cmd.CategoryIds != null)
                {
                    foreach (var catId in cmd.CategoryIds)
                        product.AddCategory(catId);
                }

                // Add images if provided
                if (cmd.Images != null)
                {
                    foreach (var file in cmd.Images)
                    {
                        var url = await _fileService.UploadAsync(file, "products");

                        product.AddImage(new ProductImage(url, file.FileName, Path.GetExtension(file.FileName)));
                    }
                }

                // Optionally, set inventory
                //product.SetInventory(new ProductInventory(0));
                await _repository.UpdateAsync(product);
                return Result<Guid>.Success(product.Id);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(ex.Message);               
            }
        }
    }
}