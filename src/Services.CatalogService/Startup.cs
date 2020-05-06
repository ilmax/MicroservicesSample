using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.CatalogService.Hypermedia;
using Services.Hateoas.Infrastructure;
using Services.Infrastructure;

namespace Services.CatalogService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddHateoas(opt => opt.RemoveSystemJsonFormatter = true);
            services.AddConsul(Configuration.GetServiceConfig());
            services.AddHostedService<ServiceDiscoveryHostedService>();
            services.AddScoped<IEnricher, CatalogEnricher>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseHttpsRedirection(); TODO

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
