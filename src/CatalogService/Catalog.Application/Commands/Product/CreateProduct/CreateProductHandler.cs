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
        private readonly IProductCommandRepository _repository;
        private readonly IProductQueries _queries;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        public CreateProductHandler(IProductCommandRepository repository, IMapper mapper, IFileService fileService, IProductQueries queries)
        {
            _repository = repository;
            _mapper= mapper;
            _fileService = fileService;
            _queries = queries;
        }
        public async Task<Result<Guid>> Handle(CreateProductCommand cmd, CancellationToken ct)
        {
            try
            {

                if (!await _queries.IsSkuUniqueAsync(cmd.SKU, ct))
                    return Result<Guid>.Failure("SKU must be unique");
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

                        product.AddImage(new ProductImage(url, file.FileName, Path.GetExtension(file.FileName)));
                    }
                }
                //product.AddInventory(0);
                await _repository.AddAsync(product, ct);
                product.MarkAsCreated();
                return Result<Guid>.Success(product.Id);
            }
            catch (DomainException ex)
            {
                return Result<Guid>.Failure(ex.Message);
            }
        }
    }
}