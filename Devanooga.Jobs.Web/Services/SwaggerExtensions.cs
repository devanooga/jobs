namespace Devanooga.Jobs.Web.Services
{
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services) => services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v0", new OpenApiInfo
                {
                    Title = "Interface API",
                    Version = "v0",
                    Description =
                        "API designed for internal use only, will change and WILL break backwards compability as needed for our GUI"
                });

                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    apiDesc.TryGetMethodInfo(out var methodInfo);
                    var versions = methodInfo?.DeclaringType?.GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);
                    return versions?.Any(v => $"v{v}" == docName) ?? false;
                });
            });

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app) => app
                .UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v0/swagger.json", "Interface API v0");
                });
    }
}