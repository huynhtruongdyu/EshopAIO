using EAIO.Shared.Domain.Abstraction;

namespace EAIO.Shared.Domain.Base
{
    public class AuditableEntity : Entity, IEntity, IAuditable
    {
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
