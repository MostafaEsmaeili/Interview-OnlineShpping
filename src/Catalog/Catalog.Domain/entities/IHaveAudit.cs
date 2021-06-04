using System;
namespace Catalog.Domain.entities
{
    public interface IHaveAudit
    {
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
