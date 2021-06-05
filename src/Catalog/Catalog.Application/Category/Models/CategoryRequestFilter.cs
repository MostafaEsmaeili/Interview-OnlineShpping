using Catalog.Application.Common.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Category.Models
{
    public class CategoryRequestFilter : IPagingFilter
    {
        public string Name { get; set; }
        public bool? Enable { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public ICollection<string> Sorts { get; set; }
    }
}
