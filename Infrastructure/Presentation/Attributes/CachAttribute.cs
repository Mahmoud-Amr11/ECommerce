using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    public class CacheAttribut(int Duration=120)   :  ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CacheKey = CreateCacheKey(context.HttpContext.Request);
            ICacheService cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var cacheValue =await cacheService.GetCacheAsync(CacheKey);

            if(cacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
            }
           var executedContext = await next.Invoke();

            if(executedContext.Result is ObjectResult objectResult)
            {
              await cacheService.SetCacheAsync(CacheKey, objectResult.Value, TimeSpan.FromSeconds(Duration));
            }
        }

        private string CreateCacheKey(HttpRequest request )
        {
            StringBuilder keyBuilder = new StringBuilder();
            keyBuilder.Append($"{request.Path}");
            keyBuilder.Append("?");
            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"{key}={value}&");
            }

            return keyBuilder.ToString();
        }
    }
}
