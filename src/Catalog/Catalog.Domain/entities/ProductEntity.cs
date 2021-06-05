using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain.entities
{
    public class ProductEntity
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
    }
}
