using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace notes_service.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseVerifyHeader(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<VerifyHeaders>();
        } 
    }
}
