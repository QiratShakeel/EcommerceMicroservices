using Ecommerce.Catalog.Domain.Aggregates;
using Ecommerce.Catalog.Domain.ValueObjects;
using Ecommerce.Catalog.Domain.Enums;
using Ecommerce.Catalog.Application.Interfaces;
using MediatR;
using System.Threading;

namespace Ecommerce.Catalog.Application.Commands
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _repository;
        public DeleteProductHandler(IProductRepository repository)
        {
            _repository = repository;
            //_mapper = mapper;
        }
        public async Task<bool> Handle(DeleteProductCommand cmd, CancellationToken ct)
        {
            var product = await _repository.GetByIdAsync(cmd.prodId, ct);
            if (product == null)
                return false;

            await _repository.DeleteAsync(product);
            return true;
        }
    }
}