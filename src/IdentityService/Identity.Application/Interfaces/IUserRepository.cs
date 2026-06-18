using Ecommerce.Identity.Domain.Aggregates;

namespace Ecommerce.Identity.Application.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user, CancellationToken cancellationToken);
        Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<Role?> GetRoleByNameAsync(String Name, CancellationToken cancellationToken);
        Task<bool> EmailExistAsync(string email, CancellationToken cancellationToken);
        Task UpdateAsync(User user, CancellationToken cancellationToken);
        Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken);
    }
}