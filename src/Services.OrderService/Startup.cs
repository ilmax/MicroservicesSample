using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Infrastructure;

namespace Services.OrderService
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
            services.AddControllers();
            services.AddConsul(Configuration.GetServiceConfig());
            services.AddHostedService<ServiceDiscoveryHostedService>();
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
