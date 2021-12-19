using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using PucMinas.TCC.Domain.Services;
using PucMinas.TCC.Utility.Constants;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace PucMinas.TCC.RegistrationInformation.HostedServices
{
    public class IntegrationLegacyHostedService : MessageQueueConsumeService, IHostedService, IDisposable
    {
        public IntegrationLegacyHostedService(IConfiguration configuration) :
            base(configuration, QueueNameConstant.IntegrationChargeByLegacy)
        {
        }

        public void Dispose()
        {
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }

        protected override Task DoWork(string message)
        {
            Debug.WriteLine(message);
            return Task.CompletedTask;
        }
    }
}
