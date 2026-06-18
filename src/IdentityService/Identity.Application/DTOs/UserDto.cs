using Ecommerce.Identity.Domain.Aggregates;

namespace Ecommerce.Identity.Application.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Role> Roles { get; set; } = new();
    }
}
