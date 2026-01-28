using MediatR;

namespace BuildingBlocks.Shared.Infrastructure
{
    public interface ICommand<out TResponse> : IRequest<TResponse> { }
}