using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Category.Models
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string ParentName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
    }
}
