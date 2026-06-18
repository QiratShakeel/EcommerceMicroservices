using Ecommerce.Catalog.Application.Commands;
using Ecommerce.Catalog.Application.Interfaces;
using MediatR;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _repository;

    public DeleteProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        DeleteProductCommand cmd,
        CancellationToken ct)
    {
        var product = await _repository.GetByIdAsync(cmd.prodId, ct);

        if (product == null)
            return false;

        product.MarkAsDeleted();

        await _repository.DeleteAsync(product);

        return true;
    }
}