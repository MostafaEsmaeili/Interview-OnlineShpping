using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Common.Extensions
{
    public static class EnumExtensions
    {
        public static bool IsInEnum<T>(this T value) where T : struct, Enum => Enum.IsDefined(value);
        public static bool IsInEnum<T>(this T? value) where T : struct, Enum
        {
            if (value is null)
            {
                return false;
            }
            return value.Value.IsInEnum();
        }
    }
}
