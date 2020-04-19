using System;
using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Infrastructure
{
    public static class ConsulExtension
    {
        public static void AddConsul(this IServiceCollection services, ServiceConfig serviceConfig)
        {
            var consulClient = new ConsulClient(opt =>
            {
                opt.Address = serviceConfig.ServiceRegistryUri;
            });

            services.AddSingleton(serviceConfig);
            services.AddSingleton<IConsulClient, ConsulClient>(p => consulClient);
        }

        public static ServiceConfig GetServiceConfig(this IConfiguration configuration)
        {
            var serviceRegistryUri = configuration.GetValue<string>("ServiceConfig:serviceRegistryUri");
            if (string.IsNullOrEmpty(serviceRegistryUri))
            {
                throw new InvalidOperationException("Missing consul service registry uri");
            }

            if (!Uri.TryCreate(serviceRegistryUri, UriKind.Absolute, out var uri))
            {
                throw new InvalidOperationException("Consul service registry uri is invalid");
            }

            return new ServiceConfig
            {
                ServiceName = configuration.GetValue<string>("ServiceConfig:serviceName"),
                ServiceRegistryUri = uri
            };
        }
    }
}