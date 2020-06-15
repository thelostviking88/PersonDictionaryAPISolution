using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace API.MiddleWare
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next.Invoke(context);
        }
    }

    public static class CultureExtensions
    {
        public static IApplicationBuilder UseCulture(this IApplicationBuilder builder)
        {
          
            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("ka-GE")
            };
            builder.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ka-GE"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
            return builder.UseMiddleware<CultureMiddleware>();
        }
    }
}
