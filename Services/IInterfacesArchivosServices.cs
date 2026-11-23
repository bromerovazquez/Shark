using Drim.Interfaces.MVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drim.Interfaces.MVC.Services
{
    public interface IInterfacesArchivosServices
    {

        void ProcessInterfaces();

        string StepProcess(string nombreArchivo, Interfaces_Procesos_Steps Step, List<Interfaces_Procesos_Steps_Parametros> parametros);
        string Step_Archivo(Interfaces_Procesos_Steps Step, List<Interfaces_Procesos_Steps_Parametros> parametros);
        string Step_QuerySql(string nombreArchivo, Interfaces_Procesos_Steps Step, List<Interfaces_Procesos_Steps_Parametros> parametros);
        string Step_DataExchanged(string nombreArchivo, Interfaces_Procesos_Steps Step, List<Interfaces_Procesos_Steps_Parametros> parametros);
        string Step_LogEstado(string nombreArchivo, Interfaces_Procesos_Steps Step, List<Interfaces_Procesos_Steps_Parametros> parametros);
        string Step_Comparador(string nombreArchivo, Interfaces_Procesos_Steps Step, List<Interfaces_Procesos_Steps_Parametros> parametros);
        string Step_Traspasos(string nombreArchivo, Interfaces_Procesos_Steps Step, List<Interfaces_Procesos_Steps_Parametros> parametros);
        string Step_Inventarios(string nombreArchivo, Interfaces_Procesos_Steps Step, List<Interfaces_Procesos_Steps_Parametros> parametros);
        string Step_Albaran_Devoluciones(string nombreArchivo, Interfaces_Procesos_Steps Step, List<Interfaces_Procesos_Steps_Parametros> parametros);
        string Step_Albaran_TraspasosY(string nombreArchivo, Interfaces_Procesos_Steps Step, List<Interfaces_Procesos_Steps_Parametros> parametros);

        List<Interfaces_Steps_ResponseData> Global_interfaces_Steps_ResponsesDatas { get; set; }

        List<Interfaces_Procesos_Logs_Estados> Global_interfaces_Archivos_Logs_Estados { get; set; }

        Interfaces_Procesos Global_interfaces_Archivos { get; set; }


        Guid Global_IdProceso { get; set; }

        DateTime Global_FechaProceso { get; set; }

        List<Interfaces_Procesos> Global_archivosConfiguracion { get; set; }
        string ArchivosLoadFileNames(List<string> directorios);
        string AgregaDatosArchivosLoadData(string nombrearchivo, long IdArchivo, string Data, Interfaces_Procesos_Steps Step);
        string GetValueParametro(string nombreArchivo,long ArchivoId, Interfaces_Procesos_Steps_Parametros parametro, List<Interfaces_Procesos_Steps_Parametros> parametros, out List<object> resultObject);
    }
}
