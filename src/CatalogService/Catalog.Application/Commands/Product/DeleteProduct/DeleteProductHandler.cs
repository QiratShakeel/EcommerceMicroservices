using Ecommerce.Catalog.Application.Commands;
using Ecommerce.Catalog.Application.Interfaces;
using MediatR;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductQueries _queries;
    private readonly IProductCommandRepository _repository;

    public DeleteProductHandler(IProductQueries queries, IProductCommandRepository repository)
    {
        _queries = queries;
        _repository = repository;
    }

    public async Task<bool> Handle(
        DeleteProductCommand cmd,
        CancellationToken ct)
    {
        var product = await _queries.GetByIdAsync(cmd.prodId, ct);

        if (product == null)
            return false;

        product.MarkAsDeleted();

        await _repository.DeleteAsync(product);

        return true;
    }
}