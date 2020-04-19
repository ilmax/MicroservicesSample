using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Hosting;

namespace Services.Infrastructure
{
    public class ServiceDiscoveryHostedService : IHostedService
    {
        private readonly IConsulClient _client;
        private readonly ServiceConfig _config;
        private readonly IServer _server;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly HashSet<string> _registrationsId = new HashSet<string>();

        public ServiceDiscoveryHostedService(IConsulClient client, ServiceConfig config, IServer server, IHostApplicationLifetime hostApplicationLifetime)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _server = server;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _hostApplicationLifetime.ApplicationStarted.Register(OnStarted);

            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            foreach (var address in _server.Features.Get<IServerAddressesFeature>().Addresses)
            {
                var addressUri = new Uri(address);
                var host = string.IsNullOrEmpty(addressUri.Host) ? addressUri.Host : _config.ServiceName;
                var registrationId = $"{_config.ServiceName}-{host}:{addressUri.Port}";

                if (_registrationsId.Add(registrationId))
                {
                    var registration = new AgentServiceRegistration
                    {
                        ID = registrationId,
                        Name = _config.ServiceName,
                        Address = host,
                        Port = addressUri.Port
                    };

                    _client.Agent.ServiceDeregister(registration.ID).GetAwaiter().GetResult();
                    _client.Agent.ServiceRegister(registration).GetAwaiter().GetResult();
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var registrationId in _registrationsId)
            {
                await _client.Agent.ServiceDeregister(registrationId, cancellationToken);
            }
        }
    }

    public class ServiceConfig
    {
        public Uri ServiceRegistryUri { get; set; }
        public string ServiceName { get; set; }
    }
}