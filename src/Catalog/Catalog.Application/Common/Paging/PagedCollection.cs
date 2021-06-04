using System.Collections.Generic;

namespace Catalog.Application.Common.Paging
{
    public record PagedCollection<T>(IEnumerable<T> Result, int TotalRecords, int Page);
}
