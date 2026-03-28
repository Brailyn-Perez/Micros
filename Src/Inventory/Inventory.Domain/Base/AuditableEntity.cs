namespace Inventory.Domain.Base
{
    public class AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
