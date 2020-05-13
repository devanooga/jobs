namespace Devanooga.Jobs.Web
{
    using Devanooga.Jobs.Data.Entity;
    using Devanooga.Jobs.Web.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<JobContext>(options =>
                    options.UseNpgsql(
                        Configuration.GetConnectionString("Default"),
                        o => o.MigrationsAssembly("Devanooga.Jobs.Data.Entity")
                    ))
                .AddSwagger()
                .AddMvc()
                .Services
                .AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, JobContext jobContext)
        {
            jobContext.Database.EnsureCreated();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseForwardedHeaders()
                .UseHealthChecks("/health")
                .UseHttpsRedirection()
                .UseAuthentication()
                .UseCustomSwagger()
                .UseDefaultFiles()
                .UseStaticFiles()
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

        }
    }
}
