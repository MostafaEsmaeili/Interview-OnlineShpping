using Microsoft.AspNetCore.Mvc;

namespace Order.Infrastructure.Extensions
{
    public static class ResultExtensions
    {
        public static JsonResult ToJsonResult<T>(this T data)
        {
            return new JsonResult(data);
        }
    }
}
