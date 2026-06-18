using System.Security.Claims;

namespace Ecommerce.Orders.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            if (user?.Identity?.IsAuthenticated != true)
                throw new UnauthorizedAccessException("User not authenticated");

            var claim = user.FindFirst("sub")?.Value;

            if (claim == null || !Guid.TryParse(claim, out var userId))
                throw new UnauthorizedAccessException("Invalid user id");

            return userId;
        }
    }
}