using Microsoft.Extensions.DependencyInjection;

namespace Shared.Application.ApiVersion
{
    public static class SharedApiVersion
    {
        public static void RegisterAPIVersion(this IServiceCollection services, string groupFormat = "'v'VVV")
        {
            services.AddApiVersioning(p =>
            {
                p.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                p.ReportApiVersions = true;
                p.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = groupFormat;
                p.SubstituteApiVersionInUrl = true;
            });
        }
    }
}
