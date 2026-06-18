using Microsoft.AspNetCore.Identity;
using Ecommerce.Identity.Application.Interfaces;
namespace Ecommerce.Identity.Infrastructure.Repositories
{

    public class PasswordHasherService : IPasswordHasher
    {
        private readonly PasswordHasher<object> _hasher = new();

        public string Hash(string password)
        {
            return _hasher.HashPassword(null!, password);
        }

        public bool Verify(string password, string passwordHash)
        {
            var result = _hasher.VerifyHashedPassword(
                null!,
                passwordHash,
                password
            );

            return result == PasswordVerificationResult.Success;
        }
    }

}
