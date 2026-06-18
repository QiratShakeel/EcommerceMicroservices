using BuildingBlocks.Shared.Exceptions;
using BuildingBlocks.Shared.Infrastructure;

namespace Ecommerce.Identity.Domain.Aggregates
{
    public class Role : Entity
    {
        public string Name { get; private set; }

        private Role() { }

        public Role(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Role name required");

            Name = name.Trim();
        }
    }
}
