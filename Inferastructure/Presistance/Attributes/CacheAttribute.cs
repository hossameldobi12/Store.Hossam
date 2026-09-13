using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Attributes
{

    public class CacheAttribute(int duration) : Attribute, IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServicesManger>().Cache;
            var CacheKey = GenerateCacheKKey(context.HttpContext.Request);
            var result = await cacheService.GetCacheValueAsync(CacheKey);
            if (!string.IsNullOrEmpty(result))
            {
                context.Result = new ContentResult()
                {
                    Content = result,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                return;
            }
            var contextResult = await next.Invoke();
            if(contextResult.Result is OkObjectResult okResult) {
                await cacheService.SetCacheValueAsync(CacheKey, okResult.Value, TimeSpan.FromSeconds(duration));
            }

        }

        private string GenerateCacheKKey(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append(request.Path);
            foreach (var item in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{item.Key}-{item.Value}");
            }
            return keyBuilder.ToString();
        }
  

    }
}
