using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain.entities
{
    public class CategoryEntity : IHaveAudit
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public virtual CategoryEntity Parent { get; set; }
        public ICollection<ProductEntity> Products { get; set; } = new HashSet<ProductEntity>();
        public ICollection<CategoryEntity> SubCategories { get; set; } = new HashSet<CategoryEntity>();

        #region AuditProperties
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        #endregion
    }
}
