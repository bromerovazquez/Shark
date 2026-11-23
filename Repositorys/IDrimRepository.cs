using Drim.Interfaces.MVC.Entities;

namespace Drim.Interfaces.MVC.Repositorys
{
    public interface IDrimRepository
    {

        string InsertLogsGestionTraspasos(string codAlmacenOrigen, string codAlmacenDestino, string codAlmacenDestinoNuevo, DateTime FechaInicio, DateTime FechaFin,  bool? IsTraspasados, int NoArticulos,int userId,bool IsEjecutado, int? codArticulo, int? codProveedor, string tipo,bool? IsSeparadoPorTienda);
        List<LogGestionTraspasosAlbaranes> GetLogsGestionTraspasos(string codAlmacenOrigen, string codAlmacenDestino,string codAlmacenDestinoNuevo, DateTime FechaInicio, DateTime FechaFin,DateTime? DateCreate, bool? IsTraspasados, int? codArticulo, int? codProveedor, string tipo);

        List<Traspaso> GetTraspasos(string codAlmacenOrigen, string codAlmacenDestino, DateTime FechaInicio, DateTime FechaFin, bool? IsTraspasados,int? codArticulo);


        Traspaso GetTraspaso(int Id);


        string InsertTraspasos(List<Traspaso> traspasos);


        string InsertTraspaso(Traspaso traspaso);

        string RemoveTraspasos(List<Traspaso> traspasos);

        string TraspasoCambioDestino(List<Traspaso> traspasosInversa, List<Traspaso> traspasosNuevo, bool IsTraspasado);


        List<MatchTraspasosYTicketDevolucion> GetMatchTraspasosYTicketDevolucions(string codAlmacenOrigen,  DateTime FechaInicio, DateTime FechaFin,  int? codArticulo);



        #region Interfaces Archivos

        List<Interfaces_Procesos> GetInterfaces_Archivos(long? id,string nombreProceso);


        short? GetUltimoOrdenArchivo();

        string InsertInterfaces_Archivos(ref Interfaces_Procesos interfaces_Archivos);

        string UpdateInterfaces_Archivos(Interfaces_Procesos interfaces_Archivos);


        string InsertInterfaces_ArchivosAccion(Interfaces_Procesos_Steps interfaces_Accion);

        List<Interfaces_Procesos_Steps_Parametros> GetInterfaces_Accion_Parametros(long? id,  long? IdAccion);

        string InsertInterfaces_ArchivosAccionParametros(Interfaces_Procesos_Steps_Parametros interfaces_Accion_Parametros);


        string InsertInterfaces_ArchivosAccionParametrosRangeByStepId(long StepId, string TipoParametros,ref List<Interfaces_Procesos_Steps_Parametros> interfaces_Acciones_Parametros);

        List<Interfaces_Procesos_Steps> GetInterfaces_Archivo_Steps(long? id, long? IdArchivo);

        string InsertInterfaces_ArchivosSteps(ref Interfaces_Procesos_Steps interfaces_Archivo_Steps);

        string InsertInterfaces_Archivos_Logs_Estados(List<Interfaces_Procesos_Logs_Estados> interfaces_Archivos_Steps);

        string UpdateInterfaces_Archivos_Logs_Estados(ref Interfaces_Procesos_Logs_Estados interfaces_Archivos_Steps, string estado,string error);


        short? GetOrdenStepByArchivo(long IdArchivo);




        Task<IEnumerable<Interfaces_Logs_Resumen>> GetInterfaces_Logs_Resumens();


        Task<vLogInterfaces> GetInterfaces_LogDetalle(long id);



        List<TraspasoIncidencias> GetTraspasosIncidencias(string codAlmacenOrigen, string codAlmacenDestino, DateTime? FechaInicio, DateTime? FechaFin, string tipo, int? codArticulo);


        #endregion
    }
}
