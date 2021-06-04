using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Common.interfaces
{
    public interface IPagingFilter
    {
        public int Page { get; set; }
        public int Size { get; set; }
        /// <summary>
        /// sort like : Title desc or Id asc
        /// </summary>
        public ICollection<string> Sorts { get; set; }
    }
}
