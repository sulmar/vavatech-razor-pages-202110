using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Middlewares
{
    public static class PageMemoryCacheMiddlewareMiddlewareExtensions
    {
        public static IServiceCollection AddPageMemoryCache(this IServiceCollection services)
        {
            services.AddMemoryCache();

            return services;
        }

        public static IApplicationBuilder UsePageMemoryCache(this IApplicationBuilder app)
        {
            app.UseMiddleware<PageMemoryCacheMiddleware>();

            return app;

        }
    }

    public class PageMemoryCacheMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IMemoryCache memoryCache;
        private readonly ILogger<PageMemoryCacheMiddleware> logger;


        public PageMemoryCacheMiddleware(RequestDelegate next, IMemoryCache memoryCache, ILogger<PageMemoryCacheMiddleware> logger)
        {
            this.next = next;
            this.memoryCache = memoryCache;
            this.logger = logger;
        }



        public async Task InvokeAsync(HttpContext context)
        {
            if (HttpMethods.IsGet(context.Request.Method))
            {
                string key = $"{context.Request.Path}";

                if (context.Request.QueryString.HasValue)
                {
                    key += context.Request.QueryString.ToString();
                }

                

                if (memoryCache.TryGetValue(key, out string body))
                {
                    logger.LogInformation($"Strona {context.Request.Path} pobrana z cache");

                    await context.Response.WriteAsync(body);
                }
                else
                {
                    // Pobieranie odpowiedzi na podst.
                    // https://exceptionnotfound.net/using-middleware-to-log-requests-and-responses-in-asp-net-core/

                    var originalBodyStream = context.Response.Body;

                    using (var responseBody = new MemoryStream())
                    {

                        //...and use that for the temporary response body
                        context.Response.Body = responseBody;

                        //  //Continue down the Middleware pipeline, eventually returning to this class
                        await next(context);

                        //Format the response from the server
                        var response = await FormatResponse(context.Response);

                        // Save log cache
                        memoryCache.Set(key, response);

                        //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
                        await responseBody.CopyToAsync(originalBodyStream);
                    }
                }

            }

            else
            {

                await next(context);
            }
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            string text = await new StreamReader(response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);

            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            return text;
        }
    }
}
