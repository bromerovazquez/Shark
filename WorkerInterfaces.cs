

using Drim.Interfaces.MVC.Data;
using Drim.Interfaces.MVC.Entities;
using Drim.Interfaces.MVC.Helpers;
using Drim.Interfaces.MVC.HUB;
using Drim.Interfaces.MVC.Services;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace Drim.Interfaces.MVC.WorkerServices
{
    public class WorkerInterfaces : BackgroundService
    {
        private readonly ILogger<WorkerInterfaces> _logger;
        
        private readonly IInterfacesArchivosServices _interfacesArchivosServices;

        

        public WorkerInterfaces(ILogger<WorkerInterfaces> logger, IInterfacesArchivosServices interfacesArchivosServices)
        {
            _logger = logger;
            _interfacesArchivosServices = interfacesArchivosServices;
        }

       

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("{time}", DateTimeOffset.Now);

               // _interfacesArchivosServices.ProcessInterfaces();

                await Task.Delay(EntornoHelper.SegundosCheckProcessInterfaces, stoppingToken);
            }
        }

    }
}