using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Common.interfaces
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}
