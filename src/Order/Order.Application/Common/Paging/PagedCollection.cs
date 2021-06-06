using System.Collections.Generic;

namespace Order.Application.Common.Paging
{
    public record PagedCollection<T>(IEnumerable<T> Result, int TotalRecords, int Page);
}
