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
        //private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateProductHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper= mapper;
            //_unitOfWork = unitOfWork;
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
                    foreach (var img in cmd.Images)
                        product.AddImage(new ProductImage(img.Url, img.AltText, img.FileType));
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