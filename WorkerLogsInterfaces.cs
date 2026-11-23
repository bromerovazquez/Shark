

using Drim.Interfaces.MVC.Data;
using Drim.Interfaces.MVC.Entities;
using Drim.Interfaces.MVC.Helpers;
using Drim.Interfaces.MVC.HUB;
using Drim.Interfaces.MVC.Repositorys;
using Drim.Interfaces.MVC.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Drim.Interfaces.MVC.WorkerServices
{
    public class WorkerLogsInterfaces : BackgroundService
    {
        private readonly ILogger<WorkerLogsInterfaces> _logger;
        private readonly IHubContext<LogInterfacesHub> _hubLogInterfaces;
        private readonly IDrimRepository _drimRepository;

        public WorkerLogsInterfaces(ILogger<WorkerLogsInterfaces> logger, IDrimRepository drimRepository, IHubContext<LogInterfacesHub> hubLogInterfaces)
        {
            _hubLogInterfaces = hubLogInterfaces;
            _logger = logger;
            _drimRepository = drimRepository;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                await SendLogInterfaces();

                await Task.Delay(EntornoHelper.SegundosCheckProcessLogInterfaces, stoppingToken);
            }
        }

        private async Task SendLogInterfaces()
        {
            //object logsinterfaces;

            //using (DRIMDbContext contex = new DRIMDbContext())
            //{
            //    logsinterfaces = await contex.vLogsInterfaces.Select(s=> new { s.Id, s.NombreProceso, s.StepDescripcion,s.DateCreate,s.Estado,s.Error }).OrderByDescending(x => x.DateCreate).ToListAsync();
            //}

            //IEnumerable<Interfaces_Logs_Resumen> logsResumen = await _drimRepository.GetInterfaces_Logs_Resumens();



            //if (logsResumen.Where(x => x.NoErrores > 2).Any())
            //{
            //    foreach (var logErrores in logsResumen.Where(x => x.NoErrores > 2 && x.IsActivo))
            //    {
            //        var archivo = _drimRepository.GetInterfaces_Archivos(logErrores.ProcesoId, null).FirstOrDefault();

            //        if (archivo != null)
            //        {
            //            archivo.IsActivo = false;
            //            _drimRepository.InsertInterfaces_Archivos(ref archivo);

            //            string htmlBody = "<table>";
            //            htmlBody += "<tr>";
            //            htmlBody += "<td>Nombre Proceso:</td>";
            //            htmlBody += "<td>" + logErrores.NombreProceso + "</td>";
            //            htmlBody += "</tr>";
            //            htmlBody += "<tr>";
            //            htmlBody += "<td>Error:</td>";
            //            htmlBody += "<td>" + logErrores.Error + "</td>";
            //            htmlBody += "</tr>";
            //            htmlBody += "<tr>";
            //            htmlBody += "<td>Fecha:</td>";
            //            htmlBody += "<td>" + (logErrores.UltimaActualziacion.HasValue ? logErrores.UltimaActualziacion.Value.ToString("dd/MM/yyy HH:mm:ss") : "") + "</td>";
            //            htmlBody += "</tr>";
            //            htmlBody += "</table>";
            //            ManagerMail.EnviarMail(RemitenteEmail.informes, "ERROR - PROCESOS - INTERFACES - " + archivo.NombreProceso, htmlBody, new List<string>() { EntornoHelper.EmailsTEST });
            //        }
            //    }
            //}


            //_hubLogInterfaces.Clients.All.SendAsync("logresumen", logsResumen);



            //_hubLogInterfaces.Clients.All.SendAsync("logsinterfaces", logsinterfaces);
        }

    }
}