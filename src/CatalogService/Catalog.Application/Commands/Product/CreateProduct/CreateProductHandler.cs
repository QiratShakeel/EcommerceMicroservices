using Ecommerce.Catalog.Domain.Aggregates;
using Ecommerce.Catalog.Domain.ValueObjects;
using BuildingBlocks.Shared.Results;
using Ecommerce.Catalog.Application.Interfaces;
using MediatR;
using AutoMapper;
using BuildingBlocks.Shared.Exceptions;

namespace Ecommerce.Catalog.Application.Commands
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Result<Guid>>
    {
        private readonly IProductRepository _repository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        public CreateProductHandler(IProductRepository repository, IMapper mapper, IFileService fileService)
        {
            _repository = repository;
            _mapper= mapper;
            _fileService = fileService;
        }
        public async Task<Result<Guid>> Handle(CreateProductCommand cmd, CancellationToken ct)
        {
            try
            {

                if (!await _repository.IsSkuUniqueAsync(cmd.SKU, ct))
                    throw new InvalidOperationException("SKU must be unique");
                var product = _mapper.Map<Product>(cmd);    //automapper 

                if (cmd.CategoryIds != null)
                {
                    foreach (var catId in cmd.CategoryIds)
                        product.AddCategory(catId);
                }

                if (cmd.Images != null)
                {
                    foreach (var file in cmd.Images)
                    {
                        var url = await _fileService.UploadAsync(file, "products");

                        product.AddImage(new ProductImage(url,file.FileName,Path.GetExtension(file.FileName)));
                    }
                }
                //product.AddInventory(0);
                await _repository.AddAsync(product, ct);
                product.MarkAsCreated();
                return Result<Guid>.Success(product.Id);
            }
            catch (DomainException ex) {
                return Result<Guid>.Failure(ex.Message);
            }
        }
    }
}