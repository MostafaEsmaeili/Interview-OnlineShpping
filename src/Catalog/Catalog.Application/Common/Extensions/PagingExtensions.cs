using Catalog.Application.Common.interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Common.Extensions
{
    public static class PagingExtensions
    {
        public static IQueryable<T> SetOrdering<T>(this IQueryable<T> query, IPagingFilter filter)
        {
            if (filter.Sorts is null)
            {
                return query;
            }
            foreach (var item in filter.Sorts)
            {
                if (query.IsOrdered())
                {
                    query = ((IOrderedQueryable<T>)query).ThenBy(item);

                }
                else
                {
                    query = query.OrderBy(item);

                }
            }
            return query;
        }
        public static IQueryable<T> SetPaging<T>(this IQueryable<T> query, IPagingFilter filter)
        {
            if (filter.Page < 1 || filter?.Page == null)
            {
                filter.Page = 0;
            }
            else
            {
                filter.Page--;
            }
            if (filter.Size < 1 || filter?.Size == null)
            {
                filter.Page = 100;
            }
            return query.Skip(filter.Page * filter.Size).Take(filter.Size);
        }
        public static IQueryable<T> SetOrderingAndPaging<T>(this IQueryable<T> query, IPagingFilter filter)
        {
            return query.SetOrdering(filter).SetPaging(filter);
        }

        public static bool IsOrdered<T>(this IQueryable<T> queryable)
        {
            if (queryable == null)
            {
                throw new ArgumentNullException("queryable");
            }

            return queryable.Expression.Type == typeof(IOrderedQueryable<T>);
        }
    }
}
