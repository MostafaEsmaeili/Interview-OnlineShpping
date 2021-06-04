using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string item)
        {
            return string.IsNullOrEmpty(item);
        }
    }
}
