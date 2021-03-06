using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain.entities
{
    public class ProductEntity :IHaveAudit
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public int Order { get; set; } 
        public bool Enabled { get; set; }
        public virtual CategoryEntity Category { get; set; }
        #region AuditProperties
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        #endregion

    }
}
