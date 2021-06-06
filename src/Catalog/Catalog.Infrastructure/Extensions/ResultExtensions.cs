using Microsoft.AspNetCore.Mvc;

namespace Catalog.Infrastructure.Extensions
{
    public static class ResultExtensions
    {
        public static JsonResult ToJsonResult<T>(this T data)
        {
            return new JsonResult(data);
        }
    }
}
