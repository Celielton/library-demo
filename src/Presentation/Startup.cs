using library_api.Infrastructure;
using library_api.Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Shared.API;
using Shared.Application.ApiVersion;
using Shared.Application.Services;
using Shared.Application.Swagger;
using Shared.Core.Application;
using Shared.Core.Infrastructure.Repository;
using Shared.Core.Infrastructure.UnitOfWork;
using Shared.Infrastructure.Repository;
using Shared.Infrastructure.UnitOfWork;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace library_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.RegisterAPIVersion();


            var connectionString = Configuration.GetConnectionString("LibraryDBContext");
            services.AddDbContext<DbContext, LibraryDBContext>(options =>
                        options.UseNpgsql(connectionString).EnableSensitiveDataLogging(true));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IApplicationService<>), typeof(ApplicationService<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();

            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddSwaggerGen(c =>
            {
               // c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library Api", Version = "v1" });
                c.OperationFilter<ApiVersionFilter>();
            });
            services.ConfigureOptions<ConfigureSwaggerOptions>();

            services.AddHealthChecks()
                        .AddNpgSql(connectionString);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options => 
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                    }
                });
            }

            app.UseHttpsRedirection();

            app.RegisterExceptionMiddleware();

            app.UseRouting();

            app.UseAuthorization();

            app.UseHealthChecks("/health");

            app.UseHealthChecks("/version", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions() {
                ResponseWriter = async (context, report) => {
                    await context.Response.WriteAsync("v" + provider.ApiVersionDescriptions.First().ApiVersion.MajorVersion.ToString());
                }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public class ConfigureSwaggerOptionsDeprecated
           : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptionsDeprecated(
            IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            // add swagger document for every API version discovered
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    CreateVersionInfo(description));
            }
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        private OpenApiInfo CreateVersionInfo(
                ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "Library API",
                Version = description.ApiVersion.ToString()
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}
