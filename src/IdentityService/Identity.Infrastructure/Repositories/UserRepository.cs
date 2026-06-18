using Ecommerce.Identity.Application.Interfaces;
using Ecommerce.Identity.Domain.Aggregates;
using Ecommerce.Identity.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Identity.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext _context;
        public UserRepository(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);            
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Include(u => u.Roles) // Load roles
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }
        public async Task<Role?> GetRoleByNameAsync(String Name, CancellationToken cancellationToken)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(u => u.Name == Name, cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<bool> EmailExistAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(u => u.Email == email, cancellationToken);
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Users
                .Include(u => u.Roles)
                .ToListAsync(cancellationToken);
        }
    }
}
