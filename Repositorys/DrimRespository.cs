using Drim.Interfaces.MVC.Connection;
using Drim.Interfaces.MVC.Data;
using Drim.Interfaces.MVC.Entities;
using Microsoft.EntityFrameworkCore;

namespace Drim.Interfaces.MVC.Repositorys
{
    public class DrimRespository : IDrimRepository
    {

        //public readonly IDbContextFactory<DRIMDbContext> _FDRIMDbContext;


        public readonly DRIMDbContext _DRIMDbContext;


        public DrimRespository(IDbContextFactory<DRIMDbContext> FDRIMDbContext)
        {
            _DRIMDbContext = FDRIMDbContext.CreateDbContext();
        }


        string IDrimRepository.InsertLogsGestionTraspasos(string codAlmacenOrigen, string codAlmacenDestino, string codAlmacenDestinoNuevo, DateTime FechaInicio, DateTime FechaFin, bool? IsTraspasados, int NoArticulos, int userId, bool IsEjecutado, int? codArticulo, int? codProveedor, string tipo, bool? IsSeparadoPorTienda)
        {
            string result = string.Empty;

            DateTime _FechaInicio = !codArticulo.HasValue ? new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0) : FechaInicio;
            DateTime _FechaFin = !codArticulo.HasValue ? new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 59) : FechaFin;


            try
            {
                var updateLog = _DRIMDbContext.LogGestionesTraspasos.Where(x =>
                x.codAlmacenOrigen == codAlmacenOrigen &&
                x.codAlmacenDestino == codAlmacenDestino &&
                _FechaInicio >= x.FechaInicio && x.FechaFin >= _FechaFin &&
                x.IsTraspasado == IsTraspasados &&
                x.codProveedor == codProveedor &&
                x.codArticulo == codArticulo &&
                x.Tipo == tipo
                ).FirstOrDefault();


                if (updateLog == null)
                    _DRIMDbContext.LogGestionesTraspasos.Add(new LogGestionTraspasosAlbaranes()
                    {
                        codAlmacenOrigen = codAlmacenOrigen,
                        codAlmacenDestino = codAlmacenDestino,
                        codAlmacenDestinoNuevo = codAlmacenDestinoNuevo,
                        DateCreate = DateTime.Now,
                        FechaInicio = _FechaInicio,
                        FechaFin = _FechaFin,
                        IsTraspasado = IsTraspasados,
                        NoArticulos = NoArticulos,
                        UserId = userId,
                        IsEjecutado = IsEjecutado,
                        codArticulo = codArticulo,
                        codProveedor = codProveedor,
                        Tipo = tipo,
                        IsSeparadoPorTienda = IsSeparadoPorTienda


                    });
                else
                {
                    updateLog.DateCreate = DateTime.Now;
                    updateLog.codAlmacenDestinoNuevo = codAlmacenDestinoNuevo;
                    updateLog.NoArticulos = NoArticulos;
                    updateLog.IsEjecutado = IsEjecutado;
                }

                _DRIMDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        List<LogGestionTraspasosAlbaranes> IDrimRepository.GetLogsGestionTraspasos(string codAlmacenOrigen, string codAlmacenDestino, string codAlmacenDestinoNuevo, DateTime FechaInicio, DateTime FechaFin, DateTime? DateCreate, bool? IsTraspasados, int? codArticulo, int? codProveedor, string tipo)
        {


            List<LogGestionTraspasosAlbaranes> logs = new List<LogGestionTraspasosAlbaranes>();


            DateTime _FechaInicio = !codArticulo.HasValue ? new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0) : FechaInicio;
            DateTime _FechaFin = !codArticulo.HasValue ? new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 59) : FechaFin;

            if (codProveedor.HasValue)
            {

                logs = _DRIMDbContext.LogGestionesTraspasos.Where(x => x.IsTraspasado == IsTraspasados && x.Tipo == tipo &&
               x.codProveedor == codProveedor &&
                _FechaInicio >= x.FechaInicio && x.FechaFin >= _FechaFin &&
               string.IsNullOrEmpty(x.codAlmacenOrigen) &&
               !x.codArticulo.HasValue
                ).ToList();


                if (logs.Any())
                    return logs;
            }


            logs = _DRIMDbContext.LogGestionesTraspasos.Where(x => x.IsTraspasado == IsTraspasados && x.Tipo == tipo &&
              (x.codProveedor == codProveedor || !codProveedor.HasValue) &&
               _FechaInicio >= x.FechaInicio && x.FechaFin >= _FechaFin &&
              x.codAlmacenOrigen == codAlmacenOrigen &&
              x.codAlmacenDestino == codAlmacenDestino &&
              !x.codArticulo.HasValue
               ).ToList();


            if (logs.Any())
                return logs;



            logs = _DRIMDbContext.LogGestionesTraspasos.Where(x => x.IsTraspasado == IsTraspasados && x.Tipo == tipo &&
         (x.codProveedor == codProveedor || !codProveedor.HasValue) &&
          _FechaInicio >= x.FechaInicio && x.FechaFin >= _FechaFin &&
         x.codAlmacenOrigen == codAlmacenOrigen &&
         x.codAlmacenDestino == codAlmacenDestino &&
         x.codArticulo == codArticulo
          ).ToList();


            if (logs.Any())
                return logs;



            return logs;
        }

        List<Traspaso> IDrimRepository.GetTraspasos(string codAlmacenOrigen, string codAlmacenDestino, DateTime FechaInicio, DateTime FechaFin, bool? IsTraspaso, int? codArticulo)
        {
            List<Traspaso> traspasos = new List<Traspaso>();

            traspasos = _DRIMDbContext.Traspasos.Where(x =>
            x.TipoTraspaso == "TRASPASO" &&
           (x.codAlmacenOrigen == codAlmacenOrigen || string.IsNullOrEmpty(codAlmacenOrigen)) &&
           (x.codAlmacenDestino == codAlmacenDestino || string.IsNullOrEmpty(codAlmacenDestino)) &&
           (x.Traspasado == IsTraspaso || !IsTraspaso.HasValue) &&
           (x.CodArticulo == codArticulo || !codArticulo.HasValue) &&
           x.FechaCreacion >= FechaInicio && x.FechaCreacion <= FechaFin).ToList();

            return traspasos;
        }


        Traspaso IDrimRepository.GetTraspaso(int Id)
        {

            return _DRIMDbContext.Traspasos.Where(x => x.IDTraspaso == Id).FirstOrDefault();

        }

        string IDrimRepository.InsertTraspasos(List<Traspaso> traspasos)
        {
            string result = string.Empty;
            try
            {
                _DRIMDbContext.Traspasos.AddRange(traspasos);
                _DRIMDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }


        string IDrimRepository.InsertTraspaso(Traspaso traspaso)
        {
            string result = string.Empty;

            DateTime _fecha = new DateTime(traspaso.FechaCreacion.Year, traspaso.FechaCreacion.Month, traspaso.FechaCreacion.Day, 0, 0, 0);

            try
            {

                var _traspaso = _DRIMDbContext.Traspasos.Where(x => x.codAlmacenOrigen == traspaso.codAlmacenOrigen && x.codAlmacenDestino == traspaso.codAlmacenDestino && x.CodArticulo == traspaso.CodArticulo && x.FechaCreacion == _fecha && x.Cantidad == traspaso.Cantidad && x.TipoTraspaso == traspaso.TipoTraspaso).ToList();

                if (_traspaso.Where(x => x.FechaCreacion.Hour == traspaso.FechaCreacion.Hour && x.FechaCreacion.Minute == traspaso.FechaCreacion.Minute).Any())
                {
                    return "El traspaso ya existe";
                }

                _DRIMDbContext.Traspasos.Add(traspaso);
                _DRIMDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }


        string IDrimRepository.RemoveTraspasos(List<Traspaso> traspasos)
        {
            string result = string.Empty;
            try
            {
                _DRIMDbContext.Traspasos.RemoveRange(_DRIMDbContext.Traspasos.Where(x => traspasos.Select(s => s.IDTraspaso).Contains(x.IDTraspaso)));
                _DRIMDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        string IDrimRepository.TraspasoCambioDestino(List<Traspaso> traspasosInversa, List<Traspaso> traspasosNuevo, bool IsTraspasado)
        {
            string result = string.Empty;

            using (var transaction = _DRIMDbContext.Database.BeginTransaction())
            {
                try
                {
                    if (!IsTraspasado)
                    {
                        _DRIMDbContext.Traspasos.RemoveRange(_DRIMDbContext.Traspasos.Where(x => traspasosInversa.Select(s => s.IDTraspaso).Contains(x.IDTraspaso)));
                    }
                    else
                    {
                        _DRIMDbContext.Traspasos.AddRange(traspasosInversa);
                    }

                    _DRIMDbContext.SaveChanges();


                    _DRIMDbContext.Traspasos.AddRange(traspasosNuevo);
                    _DRIMDbContext.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    result = ex.Message;
                }
            }

            return result;
        }



        List<MatchTraspasosYTicketDevolucion> IDrimRepository.GetMatchTraspasosYTicketDevolucions(string codAlmacenOrigen, DateTime FechaInicio, DateTime FechaFin, int? codArticulo)
        {

            return _DRIMDbContext.MatchTraspasosYTicketDevoluciones.Where(x => (x.codAlmacenOrigen == codAlmacenOrigen || string.IsNullOrEmpty(codAlmacenOrigen)) && x.FechaTraspaso >= FechaInicio && x.FechaTraspaso <= FechaFin && (x.codArticulo == codArticulo || !codArticulo.HasValue)).ToList();

        }


        List<Interfaces_Procesos> IDrimRepository.GetInterfaces_Archivos(long? id, string nombreProceso)
        {
            List<Interfaces_Procesos> response = new List<Interfaces_Procesos>();
            using (DRIMDbContext dRIMDbContext = new DRIMDbContext())
            {
                response = dRIMDbContext.Interfaces_Procesos.Include(i => i.Steps).ThenInclude(i => i.Parametros).Where(x => (x.ArchivoId == id || !id.HasValue) && (x.NombreProceso == nombreProceso || string.IsNullOrEmpty(nombreProceso))).ToList();
            }
            return response;
        }


        short? IDrimRepository.GetUltimoOrdenArchivo()
        {
            short? orden = Convert.ToInt16(0);
            short? suma = 1;
            using (DRIMDbContext dRIMDbContext = new DRIMDbContext())
            {
                orden = dRIMDbContext.Interfaces_Procesos.OrderByDescending(x => x.Orden).Select(s => s.Orden).FirstOrDefault();
                orden = orden.HasValue ? (short)(orden + suma) : Convert.ToInt16(1);
            }
            return orden;
        }




        string IDrimRepository.InsertInterfaces_Archivos(ref Interfaces_Procesos interfaces_Archivos)
        {

            string result = string.Empty;
            try
            {
                using (DRIMDbContext dRIMDbContext = new DRIMDbContext())
                {

                    long idUpdate = interfaces_Archivos != null ? interfaces_Archivos.ArchivoId : 0;

                    if (!dRIMDbContext.Interfaces_Procesos.Where(x => x.ArchivoId == idUpdate).Any())
                    {
                        interfaces_Archivos.DateCreate = DateTime.Now;
                        dRIMDbContext.Interfaces_Procesos.Add(interfaces_Archivos);
                    }
                    else
                    {
                        dRIMDbContext.Interfaces_Procesos.Update(interfaces_Archivos);
                    }
                    dRIMDbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        string IDrimRepository.UpdateInterfaces_Archivos(Interfaces_Procesos interfaces_Archivos)
        {

            string result = string.Empty;
            try
            {
                using (DRIMDbContext dRIMDbContext = new DRIMDbContext())
                {

                    long idUpdate = interfaces_Archivos != null ? interfaces_Archivos.ArchivoId : 0;

                    if (!dRIMDbContext.Interfaces_Procesos.Where(x => x.ArchivoId == idUpdate).Any())
                    {
                        interfaces_Archivos.DateCreate = DateTime.Now;
                        dRIMDbContext.Interfaces_Procesos.Add(interfaces_Archivos);
                    }
                    else
                    {
                        dRIMDbContext.Interfaces_Procesos.Update(interfaces_Archivos);
                    }
                    dRIMDbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }




        string IDrimRepository.InsertInterfaces_ArchivosAccion(Interfaces_Procesos_Steps interfaces_Accion)
        {
            string result = string.Empty;
            try
            {
                using (DRIMDbContext dRIMDbContext = new DRIMDbContext())
                {
                    if (interfaces_Accion.StepId > 0)
                    {
                        dRIMDbContext.Interfaces_Procesos_Steps.Update(interfaces_Accion);
                    }
                    else
                    {
                        dRIMDbContext.Interfaces_Procesos_Steps.Add(interfaces_Accion);
                    }

                    dRIMDbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }


        List<Interfaces_Procesos_Steps_Parametros> IDrimRepository.GetInterfaces_Accion_Parametros(long? id, long? IdAccion)
        {
            List<Interfaces_Procesos_Steps_Parametros> response = new List<Interfaces_Procesos_Steps_Parametros>();
            using (DRIMDbContext dRIMDbContext = new DRIMDbContext())
            {
                response = dRIMDbContext.Interfaces_Procesos_Steps_Parametros.Include(i => i.Step).ThenInclude(i => i.Parametros).Where(x => (x.ParametroId == id || !id.HasValue) && (x.StepId == IdAccion || !IdAccion.HasValue)).ToList();
            }
            return response;
        }


        string IDrimRepository.InsertInterfaces_ArchivosAccionParametrosRangeByStepId(long StepId, string TipoParametros, ref List<Interfaces_Procesos_Steps_Parametros> interfaces_Acciones_Parametros)
        {

            string result = string.Empty;
            try
            {
                using (DRIMDbContext dRIMDbContext = new DRIMDbContext())
                {


                    var parametrosActaules = dRIMDbContext.Interfaces_Procesos_Steps_Parametros.Where(x => x.StepId == StepId && x.TipoParametros == TipoParametros).ToList();


                    //Dictionary<string, string> parametrosReeplazar = new Dictionary<string, string>();




                    var parametrosEliminar = (from actuales in parametrosActaules
                                              join nuevos in interfaces_Acciones_Parametros on new { actuales.LineaArchivo, actuales.Value_IndexParametros } equals new { nuevos.LineaArchivo, nuevos.Value_IndexParametros } into diff
                                              //join nuevos in interfaces_Acciones_Parametros on actuales.NombreParametro equals nuevos.NombreParametro into diff
                                              from faltan in diff.DefaultIfEmpty()
                                              where faltan == null
                                              select actuales);

                    if (parametrosEliminar.Any())
                    {


                        var stepActual = dRIMDbContext.Interfaces_Procesos_Steps.Where(x => x.StepId == StepId).FirstOrDefault();

                        var parametrosStepsAfters = dRIMDbContext.Interfaces_Procesos_Steps.Include(i => i.Parametros).Where(x => x.ArchivoId == stepActual.ArchivoId && x.Orden > stepActual.Orden).SelectMany(s => s.Parametros).ToList();


                        foreach (var paramAfter in parametrosStepsAfters)
                        {

                            if (!string.IsNullOrEmpty(paramAfter.ValueParametro) && paramAfter.ValueParametro.IndexOf("[") != -1)
                            {

                                var parametrosSetValues = paramAfter.ValueParametro.Split(new char[] { '+', '-', '*', '/' }).ToList();

                                foreach (var operacion in parametrosSetValues)
                                {

                                    if (operacion.IndexOf("[") != -1)
                                    {

                                        var parametroIdActual = int.Parse(operacion.Substring(1, operacion.IndexOf("]") - 1));
                                        var parametroActual = parametrosEliminar.Where(x => x.ParametroId == parametroIdActual).FirstOrDefault();
                                        if (parametroActual != null)
                                        {
                                            paramAfter.ValueParametro = String.Empty;
                                        }
                                    }

                                }

                            }

                        }

                        dRIMDbContext.Interfaces_Procesos_Steps_Parametros.RemoveRange(parametrosEliminar);

                    }

                    foreach (var parametroNuevo in interfaces_Acciones_Parametros)
                    {

                        var paramUpdate = parametrosActaules.Where(x => x.LineaArchivo == parametroNuevo.LineaArchivo && x.Value_IndexParametros == parametroNuevo.Value_IndexParametros).FirstOrDefault();

                        if (paramUpdate != null)
                        {
                            paramUpdate.NombreParametro = parametroNuevo.NombreParametro;
                            paramUpdate.TypeDataParametro = parametroNuevo.TypeDataParametro;
                            paramUpdate.ValueParametro = parametroNuevo.ValueParametro;
                            paramUpdate.FormatDateTime = parametroNuevo.FormatDateTime;
                            paramUpdate.IsKey = parametroNuevo.IsKey;
                            parametroNuevo.ParametroId = paramUpdate.ParametroId;
                            dRIMDbContext.Interfaces_Procesos_Steps_Parametros.Update(paramUpdate);
                        }
                        else
                        {
                            dRIMDbContext.Interfaces_Procesos_Steps_Parametros.Add(parametroNuevo);
                        }
                    }

                    dRIMDbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }


            return result;
        }



        string IDrimRepository.InsertInterfaces_ArchivosAccionParametros(Interfaces_Procesos_Steps_Parametros interfaces_Accion_Parametros)
        {
            string result = string.Empty;
            try
            {
                using (DRIMDbContext dRIMDbContext = new DRIMDbContext())
                {
                    if (interfaces_Accion_Parametros.ParametroId > 0)
                    {
                        dRIMDbContext.Interfaces_Procesos_Steps_Parametros.Update(interfaces_Accion_Parametros);
                    }
                    else
                    {
                        dRIMDbContext.Interfaces_Procesos_Steps_Parametros.Add(interfaces_Accion_Parametros);
                    }

                    dRIMDbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        List<Interfaces_Procesos_Steps> IDrimRepository.GetInterfaces_Archivo_Steps(long? id, long? IdArchivo)
        {
            List<Interfaces_Procesos_Steps> response = new List<Interfaces_Procesos_Steps>();
            using (DRIMDbContext dRIMDbContext = new DRIMDbContext())
            {
                response = dRIMDbContext.Interfaces_Procesos_Steps.Include(i => i.Parametros).Where(x => (x.StepId == id || !id.HasValue) && (x.ArchivoId == IdArchivo || !IdArchivo.HasValue)).ToList();
            }
            return response;
        }

        string IDrimRepository.InsertInterfaces_ArchivosSteps(ref Interfaces_Procesos_Steps interfaces_Archivo_Steps)
        {
            string result = string.Empty;
            try
            {
                using (DRIMDbContext dRIMDbContext = new DRIMDbContext())
                {
                    if (interfaces_Archivo_Steps.StepId > 0)
                    {
                        dRIMDbContext.Interfaces_Procesos_Steps.Update(interfaces_Archivo_Steps);
                    }
                    else
                    {
                        dRIMDbContext.Interfaces_Procesos_Steps.Add(interfaces_Archivo_Steps);

                    }
                    dRIMDbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }


        string IDrimRepository.InsertInterfaces_Archivos_Logs_Estados(List<Interfaces_Procesos_Logs_Estados> interfaces_Archivos_Steps)
        {

            string result = string.Empty;

            try
            {
                using (DRIMDbContext dRIMDbContext = new DRIMDbContext())
                {
                    dRIMDbContext.Interfaces_Procesos_Logs_Estados.AddRange(interfaces_Archivos_Steps);
                    dRIMDbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;

        }

        string IDrimRepository.UpdateInterfaces_Archivos_Logs_Estados(ref Interfaces_Procesos_Logs_Estados interfaces_Archivos_Steps, string estado, string error)
        {

            string result = string.Empty;

            try
            {
                using (DRIMDbContext dRIMDbContext = new DRIMDbContext())
                {

                    if (interfaces_Archivos_Steps.Id > 0)
                    {
                        interfaces_Archivos_Steps.Estado = estado;
                        interfaces_Archivos_Steps.Error = error;
                        interfaces_Archivos_Steps.DateUpdate = DateTime.Now;
                        dRIMDbContext.Interfaces_Procesos_Logs_Estados.Update(interfaces_Archivos_Steps);
                    }
                    else
                    {
                        dRIMDbContext.Interfaces_Procesos_Logs_Estados.Add(interfaces_Archivos_Steps);
                    }

                    dRIMDbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;

        }



        short? IDrimRepository.GetOrdenStepByArchivo(long IdArchivo)
        {
            short? orden = 0;
            short? suma = 1;
            using (DRIMDbContext dRIMDbContext = new DRIMDbContext())
            {

                orden = dRIMDbContext.Interfaces_Procesos_Steps.Where(x => x.ArchivoId == IdArchivo).OrderByDescending(o => o.Orden).Select(s => s.Orden).FirstOrDefault();
                orden = orden.HasValue ? (short?)(orden + suma) : suma;
            }

            return orden;
        }


        async Task<IEnumerable<Interfaces_Logs_Resumen>> IDrimRepository.GetInterfaces_Logs_Resumens()
        {
            List<Interfaces_Logs_Resumen> resumens = new List<Interfaces_Logs_Resumen>();

            using (DAL dal = new DAL(ConnectionCatalogDB.DRIM))
            {
                using (var dtr = await dal.DataReaderAsyn("GetInterfacesLogsResumen", System.Data.CommandType.StoredProcedure))
                {
                    while (dtr.Read())
                    {
                        resumens.Add(new Interfaces_Logs_Resumen()
                        {
                            ProcesoId = !dtr.IsDBNull(0) ? dtr.GetInt64(0) : 0,
                            NombreProceso = !dtr.IsDBNull(0) ? dtr.GetString(1) : String.Empty,
                            IsActivo = !dtr.IsDBNull(2) ? dtr.GetBoolean(2) : false,
                            UltimoEstado = !dtr.IsDBNull(3) ? dtr.GetString(3) : String.Empty,
                            UltimaActualziacion = !dtr.IsDBNull(4) ? (DateTime?)dtr.GetDateTime(4) : null,
                            Error = !dtr.IsDBNull(5) ? dtr.GetString(5) : String.Empty,
                            NoSucces = !dtr.IsDBNull(6) ? dtr.GetInt32(6) : 0,
                            NoErrores = !dtr.IsDBNull(7) ? dtr.GetInt32(7) : 0
                        });
                    }
                }
            }

            return resumens;
        }


        async Task<vLogInterfaces> IDrimRepository.GetInterfaces_LogDetalle(long id) 
        {
            vLogInterfaces response = null;

            using (DRIMDbContext dRIMDbContext = new DRIMDbContext())
            {
                response = await dRIMDbContext.vLogsInterfaces.Where(x => x.Id == id).FirstOrDefaultAsync();
            }

            return response;
        }


        List<TraspasoIncidencias> IDrimRepository.GetTraspasosIncidencias(string codAlmacenOrigen, string codAlmacenDestino, DateTime? FechaInicio, DateTime? FechaFin, string tipo, int? codArticulo) 
        {
            List<TraspasoIncidencias> response;

            using (DRIMDbContext dRIMDbContext = new DRIMDbContext())
            {
                response =  dRIMDbContext.TraspasosIncidencias.Where(x => 

                (x.AlmacenOrigen == codAlmacenOrigen || string.IsNullOrEmpty(codAlmacenOrigen))
                &&
                (x.AlmacenDestino == codAlmacenDestino || string.IsNullOrEmpty(codAlmacenDestino))
                &&
                (((x.FechaCreacion >= FechaInicio && x.FechaCreacion <= FechaFin) || !FechaInicio.HasValue)
                ||
                ((x.FechaLectura >= FechaInicio && x.FechaLectura <= FechaFin) || !FechaInicio.HasValue))
                &&
                (x.TipoInidencia == tipo || string.IsNullOrEmpty(tipo))
                &&
                (x.CodArticulo == codArticulo || !codArticulo.HasValue)
                ).ToList();
            }

            return response;
        }

    }
}
