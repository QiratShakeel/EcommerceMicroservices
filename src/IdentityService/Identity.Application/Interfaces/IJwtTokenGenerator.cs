using Ecommerce.Identity.Domain.Aggregates;

namespace Ecommerce.Identity.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}