using library_api.Infrastructure;
using library_api.Infrastructure.Repository;
using library_api.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
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

            services.AddApiVersioning(p =>
            {
                p.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                p.ReportApiVersions = true;
                p.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });

            var connectionString = Configuration.GetConnectionString("LibraryDBContext");
            services.AddDbContext<LibraryDBContext>(options =>
                        options.UseNpgsql(connectionString).EnableSensitiveDataLogging(true));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library Api", Version = "v1" });
                c.OperationFilter<RemoveQueryApiVersionParamOperationFilter>();
            });

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
}
