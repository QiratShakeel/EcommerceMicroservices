namespace BuildingBlocks.Shared.Infrastructure
{
    public abstract class AuditableEntity : Entity
    {
        public DateTime CreatedAt { get; protected set; }
        public string? CreatedBy { get; protected set; }

        public DateTime? LastModifiedAt { get; protected set; }
        public string? LastModifiedBy { get; protected set; }

        protected void SetCreated(string? user)
        {
            CreatedAt = DateTime.UtcNow;
            CreatedBy = user;
        }

        protected void SetModified(string? user)
        {
            LastModifiedAt = DateTime.UtcNow;
            LastModifiedBy = user;
        }
    }
}