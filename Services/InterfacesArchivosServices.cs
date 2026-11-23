using Drim.Interfaces.MVC.Connection;
using Drim.Interfaces.MVC.Entities;
using Drim.Interfaces.MVC.Helpers;
using Drim.Interfaces.MVC.Repositorys;
using MathNet.Symbolics;
using NCrontab;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drim.Interfaces.MVC.Services
{
    public class InterfacesArchivosServices : IInterfacesArchivosServices
    {
        private readonly ILogger<InterfacesArchivosServices> _logger;

        public readonly IDrimRepository _DrimRepository;

        public readonly IManagerRepository _ManagerRepository;

        public List<Interfaces_Steps_ResponseData> Global_interfaces_Steps_ResponsesDatas { get; set; }
        public Interfaces_Procesos Global_interfaces_Archivos { get; set; }
        public List<Interfaces_Procesos_Logs_Estados> Global_interfaces_Archivos_Logs_Estados { get; set; }
        public Dictionary<string, List<string>> directoriosArchivos { get; set; }
        public Guid Global_IdProceso { get; set; }
        public DateTime Global_FechaProceso { get; set; }


        public List<Interfaces_Procesos> Global_archivosConfiguracion { get; set; }

        public InterfacesArchivosServices(IDrimRepository drimRepository, ILogger<InterfacesArchivosServices> logger, IManagerRepository managerRepository)
        {
            _logger = logger;

            _DrimRepository = drimRepository;
            _ManagerRepository = managerRepository;



        }

        //public void ProcessInterfaces()
        //{

        //    string result = string.Empty;

        //    //0.-Carga todas las Configuracion del Archivo
        //    _logger.LogInformation("0.-Carga todas las Configuracion del Archivo");
        //    Global_archivosConfiguracion = _DrimRepository.GetInterfaces_Archivos(null, null);

        //    if (!Global_archivosConfiguracion.Any())
        //    {
        //        _logger.LogWarning("No se encontro ningun archivo configurado.");
        //        return;
        //    }

        //    _logger.LogInformation("1.-Carga Directorios y Datos de Arvhicos en Logs");
        //    List<string> directorios = new List<string>();
        //    //directorios.AddRange(Global_archivosConfiguracion.Where(x => !string.IsNullOrEmpty(x.Path_Destino_Adaia)).Select(s => "ftp|" + Global_interfaces_Servidores.Where(x => x.Id == s.IdServidorFtp).Select(ser => ser.Ip).FirstOrDefault() + "|" + s.Path_Destino_Adaia + "|" + Global_interfaces_Servidores.Where(x => x.Id == s.IdServidorFtp).Select(ser => ser.usuario + "|" + ser.password).FirstOrDefault()).Distinct().ToList());
        //    //directorios.AddRange(Global_archivosConfiguracion.Where(x => !string.IsNullOrEmpty(x.Path_Destino_Manager)).Select(s => "local|" + Global_interfaces_Servidores.Where(x => x.Id == s.IdServidorLocal).Select(ser => ser.Ip).FirstOrDefault() + "|" + s.Path_Destino_Manager + "|" + Global_interfaces_Servidores.Where(x => x.Id == s.IdServidorLocal).Select(ser => ser.usuario + "|" + ser.password).FirstOrDefault()).Distinct().ToList());
        //    //directorios.AddRange(Global_archivosConfiguracion.Where(x => !string.IsNullOrEmpty(x.Path_Origen_Adaia)).Select(s => "ftp|" + Global_interfaces_Servidores.Where(x => x.Id == s.IdServidorFtp).Select(ser => ser.Ip).FirstOrDefault() + "|" + s.Path_Origen_Adaia + "|" + Global_interfaces_Servidores.Where(x => x.Id == s.IdServidorFtp).Select(ser => ser.usuario + "|" + ser.password)).Distinct().ToList());
        //    //directorios.AddRange(Global_archivosConfiguracion.Where(x => !string.IsNullOrEmpty(x.Path_Origen_Manager)).Select(s => "local|" + Global_interfaces_Servidores.Where(x => x.Id == s.IdServidorLocal).Select(ser => ser.Ip).FirstOrDefault() + "|" + s.Path_Origen_Manager + "|" + Global_interfaces_Servidores.Where(x => x.Id == s.IdServidorLocal).Select(ser => ser.usuario + "|" + ser.password)).Distinct().ToList());
        //    //result = ArchivosLoadFileNames(directorios);

        //    if (!string.IsNullOrEmpty(result))
        //    {
        //        return;
        //    }

        //    if (!Global_interfaces_Archivos_Logs_Estados.Any())
        //    {
        //        _logger.LogInformation("Sin arhivos que procesar.");
        //        return;
        //    }

        //    foreach (var archivoLogData in Global_interfaces_Archivos_Logs_Estados)
        //    {
        //        result = string.Empty;
        //        Global_interfaces_Archivos = null;
        //        Global_interfaces_Steps_ResponsesDatas = null;
        //        _interfaces_Servidor_Ftp = null;
        //        _interfaces_Servidor_Local = null;

        //        _logger.LogInformation("2.-Procesando Archivo " + archivoLogData.NombreArchivo);

        //        Global_interfaces_Archivos = Global_archivosConfiguracion.Where(x => x.ArchivoId == archivoLogData.IdArchivo).FirstOrDefault();

        //        if (Global_interfaces_Archivos == null)
        //        {
        //            _logger.LogError("No se encontro la configuracion del arhivho:" + archivoLogData.NombreArchivo);
        //            _DrimRepository.UpdateInterfaces_Archivos_Logs_Estados(archivoLogData, "ConfiguracionArchivoError: No se encontro la configuracion del archivo:" + archivoLogData.NombreArchivo);
        //            continue;
        //        }

        //        _logger.LogInformation("3.-Carga Datos de Configuracion del Archivo" + archivoLogData.NombreArchivo);

        //        result = AgregaDatosArchivosLoadData(archivoLogData.NombreArchivo, archivoLogData.IdArchivo.Value, archivoLogData.Data, Global_interfaces_Archivos.Steps.Where(x => x.Orden == 0).FirstOrDefault());

        //        if (!string.IsNullOrEmpty(result))
        //        {
        //            _logger.LogError(result);
        //            _DrimRepository.UpdateInterfaces_Archivos_Logs_Estados(archivoLogData, "LoadDataArchivoError: " + result + ". " + archivoLogData.NombreArchivo);
        //            continue;
        //        }

        //        _logger.LogInformation("4.-Carga Datos de Arhivo Logs" + archivoLogData.NombreArchivo);

        //        _interfaces_Servidor_Ftp = Global_interfaces_Servidores.Where(x => x.Id == Global_interfaces_Archivos.IdServidorFtp).FirstOrDefault();
        //        _interfaces_Servidor_Local = Global_interfaces_Servidores.Where(x => x.Id == Global_interfaces_Archivos.IdServidorLocal).FirstOrDefault();



        //        foreach (var step in Global_interfaces_Archivos.Steps.OrderBy(o => o.Orden).ThenBy(i => i.Direccion))
        //        {
        //            try
        //            {
        //                _logger.LogInformation("5.-Procesando Step " + step.StepId + ". TipoStep:" + step.IdTipoAccion.Value.GetTipoAccion().ToString() + ". nombreFile:" + archivoLogData.NombreArchivo);

        //                _DrimRepository.UpdateInterfaces_Archivos_Logs_Estados(archivoLogData, "StepProcess:" + step.StepId + ". TipoStep:" + step.IdTipoAccion.Value.GetTipoAccion().ToString() + ". nombreFile:" + archivoLogData.NombreArchivo + ". " + result);

        //                result = StepProcess(archivoLogData.NombreArchivo, step, Global_interfaces_Archivos.Steps.SelectMany(s => s.Parametros).ToList());

        //                if (!string.IsNullOrEmpty(result))
        //                {
        //                    _logger.LogCritical("Error al procesar el Step:" + step.StepId + ". TipoStep:" + step.IdTipoAccion.Value.GetTipoAccion().ToString() + ". nombreFile:" + archivoLogData.NombreArchivo + ". " + result);
        //                    _DrimRepository.UpdateInterfaces_Archivos_Logs_Estados(archivoLogData, "StepError:" + step.StepId + ". TipoStep:" + step.IdTipoAccion.Value.GetTipoAccion().ToString() + ". nombreFile:" + archivoLogData.NombreArchivo + ". " + result);
        //                    break;
        //                }
        //                else
        //                {
        //                    _DrimRepository.UpdateInterfaces_Archivos_Logs_Estados(archivoLogData, "StepSuccess:" + step.StepId + ". TipoStep:" + step.IdTipoAccion.Value.GetTipoAccion().ToString() + ". nombreFile:" + archivoLogData.NombreArchivo + ". " + result);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                _logger.LogError("Error al procesar el Step:" + step.StepId + ". " + ex.Message, ex);
        //            }
        //        }

        //    }
        //}



        public void ProcessInterfaces()
        {

            Global_FechaProceso = DateTime.Now;
            Global_IdProceso = Guid.NewGuid();
            Global_archivosConfiguracion = new List<Interfaces_Procesos>();
            Global_interfaces_Steps_ResponsesDatas = new List<Interfaces_Steps_ResponseData>();
            Global_interfaces_Archivos_Logs_Estados = new List<Interfaces_Procesos_Logs_Estados>();
            directoriosArchivos = new Dictionary<string, List<string>>();


            var procesos = _DrimRepository.GetInterfaces_Archivos(null, null);

            foreach (var proceso in procesos.Where(x => x.IsActivo).OrderBy(x => x.Orden))
            {

                if (!string.IsNullOrEmpty(proceso.ExpresionCron) && ValidateCronExpression(proceso.ExpresionCron, proceso.LastUpdate.HasValue? proceso.LastUpdate.Value: proceso.DateCreate.Value))
                {
                    _logger.LogInformation("Procesando : " + proceso.NombreProceso);

                    foreach (var step in proceso.Steps.OrderBy(o => o.Direccion).ThenBy(o => o.Orden))
                    {
                        _logger.LogInformation("Step : " + step.Orden + "|" + step.Descripcion);

                        StepProcess(null, step, proceso.Steps.SelectMany(s => s.Parametros).ToList());
                    }

                    proceso.LastUpdate = DateTime.Now;
                    _DrimRepository.UpdateInterfaces_Archivos(proceso);

                  

                }
            }
        }



        public string StepProcess(string nombreArchivo, Interfaces_Procesos_Steps Step, List<Interfaces_Procesos_Steps_Parametros> parametros)
        {
            string result = string.Empty;

            switch (Step.IdTipoAccion.Value.GetTipoAccion())
            {
                case TipoAccion.Archivo:
                    result = Step_Archivo(Step, parametros);
                    break;
                case TipoAccion.QuerySql:
                    result = Step_QuerySql(nombreArchivo, Step, parametros);
                    break;
                case TipoAccion.DataExchanged:
                    result = Step_DataExchanged(nombreArchivo, Step, parametros);
                    break;
                case TipoAccion.LogEstado:
                    result = Step_LogEstado(nombreArchivo, Step, parametros);
                    break;
                case TipoAccion.Comparador:
                    result = Step_Comparador(nombreArchivo, Step, parametros);
                    break;
                case TipoAccion.Traspaso:
                    result = Step_Traspasos(nombreArchivo, Step, parametros);
                    break;
                case TipoAccion.Inventarios:
                    result = Step_Inventarios(nombreArchivo, Step, parametros);
                    break;
                case TipoAccion.Albaran_Devoluciones:
                    result = Step_Albaran_Devoluciones(nombreArchivo, Step, parametros);
                    break;
                case TipoAccion.Albaran_TraspasosY:
                    result = Step_Albaran_TraspasosY(nombreArchivo, Step, parametros);
                    break;
                case TipoAccion.Undefined:
                    break;
            }

            return result;
        }

        public string Step_Archivo(Interfaces_Procesos_Steps Step, List<Interfaces_Procesos_Steps_Parametros> parametros)
        {
            string result = string.Empty;
            List<string> files = new List<string>();
            string filesProcess = string.Empty;
            string dataReader = string.Empty;

            try
            {
                string parametroIn = Step.Direccion == 'V' ? Step.Path_Origen_Adaia + "/" + filesProcess : Step.Path_Origen_Manager + "\\" + filesProcess;

                Interfaces_Procesos_Logs_Estados logEstado = new Interfaces_Procesos_Logs_Estados()
                {
                    DateCreate = DateTime.Now,
                    Estado = "Start",
                    ProcesoId = Step.ArchivoId,
                    StepId = Step.StepId,
                    NombreArchivo = filesProcess,
                    IdProcess = Global_IdProceso,
                    DataIn = parametroIn,
                };

                result = _DrimRepository.UpdateInterfaces_Archivos_Logs_Estados(ref logEstado, "LoadData", null);

                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }


                if (Step.Direccion == 'v')
                {
                    files = FTPManager.GetFilesFtp(Step.Path_Origen_Adaia, EntornoHelper.Path_Origen_Adaia.usuario, EntornoHelper.Path_Origen_Adaia.pass);
                }
                else
                {
                    files = FTPManager.GetFilesLocal(Step.Path_Origen_Manager);
                }


                if (!files.Any())
                {
                    return "No se encontro ningun archivo configurado.";

                }

                filesProcess = files.Where(x => x.Split('.')[0].ToUpper().Trim() == Step.NombreArchivo.ToUpper().Trim()).FirstOrDefault();

                if (string.IsNullOrEmpty(filesProcess))
                {
                    return "No se encontro ningun archivo configurado.";
                }



                if (Step.Direccion == 'v')
                {

                    result = FTPManager.ReaderFileFtp(Step.Path_Origen_Adaia + "/" + filesProcess, EntornoHelper.Path_Origen_Adaia.usuario, EntornoHelper.Path_Origen_Adaia.pass, out dataReader);
                }
                else
                {

                    result = FTPManager.ReaderFileLocal(Step.Path_Origen_Manager + "\\" + filesProcess, out dataReader);
                }


                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }




                result = AgregaDatosArchivosLoadData(filesProcess, Step.ArchivoId.Value, dataReader, Step);

                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }


                if (Step.Direccion == 'v')
                {
                    result = FTPManager.DownloadFileFTP(Step.Path_Origen_Adaia + "/" + filesProcess, Step.Path_Destino_Manager + "\\" + filesProcess, EntornoHelper.Path_Origen_Adaia.usuario, EntornoHelper.Path_Origen_Adaia.pass);
                }
                else
                {
                    result = FTPManager.UploadFileFTP(Step.Path_Origen_Manager + "\\" + filesProcess, Step.Path_Destino_Adaia + "/" + filesProcess, EntornoHelper.Path_Origen_Adaia.usuario, EntornoHelper.Path_Origen_Adaia.pass);
                }


                logEstado.Estado = "LoadData";
                logEstado.DateUpdate = DateTime.Now;
                logEstado.DataOut = dataReader;

                _DrimRepository.UpdateInterfaces_Archivos_Logs_Estados(ref logEstado, "LoadData", null);


            }
            catch (Exception ex)
            {
                result = ex.Message;
                Interfaces_Procesos_Logs_Estados logEstadoError = new Interfaces_Procesos_Logs_Estados()
                {
                    DateCreate = DateTime.Now,
                    Estado = "Error",
                    ProcesoId = Step.ArchivoId,
                    StepId = Step.StepId,
                    NombreArchivo = filesProcess,
                    IdProcess = Global_IdProceso
                };

                _DrimRepository.UpdateInterfaces_Archivos_Logs_Estados(ref logEstadoError, "Error", result);

            }
            return result;
        }
        public string Step_QuerySql(string nombreArchivo, Interfaces_Procesos_Steps Step, List<Interfaces_Procesos_Steps_Parametros> parametros)
        {
            string result = string.Empty;
            var paramtreosStepQuerySQl = parametros.Where(x => x.StepId == Step.StepId).ToList();
            try
            {

                Interfaces_Procesos_Logs_Estados logEstado = new Interfaces_Procesos_Logs_Estados()
                {
                    DateCreate = DateTime.Now,
                    Estado = "Start",
                    ProcesoId = Step.ArchivoId,
                    StepId = Step.StepId,
                    NombreArchivo = nombreArchivo,
                    IdProcess = Global_IdProceso
                };

                result = _DrimRepository.UpdateInterfaces_Archivos_Logs_Estados(ref logEstado, "Start", result);

                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }


                using (DAL dal = new DAL(Step.DBSQL.GetDB().GetConectionString()))
                {
                    foreach (var parametro in paramtreosStepQuerySQl.Where(x => x.TipoParametros == "in"))
                    {
                        List<object> _valueParametro = null;

                        result = GetValueParametro(nombreArchivo, Step.ArchivoId.Value, parametro, parametros, out _valueParametro);

                        if (!string.IsNullOrEmpty(result))
                        {
                            return result;
                        }

                        if (!_valueParametro.Any())
                        {
                            return "Error: No se encontro el Valor del Parametro:" + parametro.NombreParametro;
                        }

                        dal.AddParameters("@" + parametro.NombreParametro, _valueParametro[0], parametro.TypeDataParametro.GetSqlDbType(), 100);
                    }

                    //                    _DrimRepository.UpdateInterfaces_Archivos_Logs_Estados()

                    if (dal.parameters.Any())
                    {
                        logEstado.DataIn = JsonConvert.SerializeObject(dal.parameters);

                        result = _DrimRepository.UpdateInterfaces_Archivos_Logs_Estados(ref logEstado, "LoadData", null);

                        if (!string.IsNullOrEmpty(result))
                        {
                            return result;
                        }
                    }

                    var parametrosResponseConfiguracion = paramtreosStepQuerySQl.Where(x => x.TipoParametros == "out").ToList();

                    List<Interfaces_Steps_ResponseData> _dataOut = new List<Interfaces_Steps_ResponseData>();

                    if (parametrosResponseConfiguracion.Any())
                    {
                        using (var rdr = dal.DataReader(Step.QuerySQL, Step.TipoQuerySQL.Value.GetCommandType()))
                        {
                            int row = 0;

                            while (rdr.Read())
                            {
                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    if (!rdr.IsDBNull(i))
                                    {
                                        string fieldName = rdr.GetName(i);

                                        var configParametro = parametrosResponseConfiguracion.Where(x => x.NombreParametro == fieldName).FirstOrDefault();

                                        if (configParametro == null)
                                        {
                                            return "No se encontro la configuracion del parametro out :" + fieldName + ". nombreArchivo:" + (!string.IsNullOrEmpty(nombreArchivo) ? nombreArchivo : "") + ". IdStep:" + Step.StepId;
                                        }

                                        var dataResponse = new Interfaces_Steps_ResponseData()
                                        {
                                            IdArchivo = Step.ArchivoId.Value,
                                            IdStep = Step.StepId,
                                            IndexValue = i,
                                            Value = rdr.GetValue(i) != null ? Convert.ToString(rdr.GetValue(i)) : string.Empty,
                                            IsKey = configParametro.IsKey.HasValue && configParametro.IsKey.Value,
                                            LineaValue = configParametro.LineaArchivo,
                                            NombreArchivo = nombreArchivo,
                                            TipoValueData = configParametro.TypeDataParametro,
                                            Row = row,
                                            ParametroId = configParametro.ParametroId
                                        };


                                        _dataOut.Add(dataResponse);


                                    }
                                }

                                row++;
                            }

                            if (_dataOut.Any())
                            {
                                Global_interfaces_Steps_ResponsesDatas.AddRange(_dataOut);
                                logEstado.DataOut = JsonConvert.SerializeObject(_dataOut);
                                result = _DrimRepository.UpdateInterfaces_Archivos_Logs_Estados(ref logEstado, "LoadData", null);
                            }
                        }
                    }
                    else
                    {
                        dal.ExecuteQueries(Step.QuerySQL, Step.TipoQuerySQL.Value.GetCommandType());
                        result = _DrimRepository.UpdateInterfaces_Archivos_Logs_Estados(ref logEstado, "Ejecutado", null);
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;

                Interfaces_Procesos_Logs_Estados logError = new Interfaces_Procesos_Logs_Estados()
                {
                    DateCreate = DateTime.Now,
                    Estado = "Error",
                    ProcesoId = Step.ArchivoId,
                    StepId = Step.StepId,
                    NombreArchivo = nombreArchivo,
                    IdProcess = Global_IdProceso

                };

                _DrimRepository.UpdateInterfaces_Archivos_Logs_Estados(ref logError, "Error", result);
            }

            return result;
        }
        public string Step_DataExchanged(string nombreArchivo, Interfaces_Procesos_Steps Step, List<Interfaces_Procesos_Steps_Parametros> parametros)
        {
            string result = string.Empty;
            string responseWs = string.Empty;
            string responseErrorWs = string.Empty;
            List<string> data = new List<string>();
            string cabezera = string.Empty;

            Interfaces_Procesos_Logs_Estados logEstado = new Interfaces_Procesos_Logs_Estados()
            {
                DateCreate = DateTime.Now,
                Estado = "Start",
                ProcesoId = Step.ArchivoId,
                StepId = Step.StepId,
                NombreArchivo = nombreArchivo,
                IdProcess = Global_IdProceso
            };

            result = _DrimRepository.UpdateInterfaces_Archivos_Logs_Estados(ref logEstado, "Start", result);

            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }


            var configuracionParametros = parametros.Where(x => x.StepId == Step.StepId).ToList();

            var configuracionLineas = _ManagerRepository.GetParametrosLineaFilterMapsDataExchage(Step.IdMapDataExchanged.Value);



            if (configuracionLineas.Where(x => !string.IsNullOrEmpty(x.FILTROLINEAS) && x.FILTROLINEAS.IndexOf("CAB") != -1).Any())
            {
                foreach (var parametrosCABIn in configuracionParametros.Where(x => x.LineaArchivo == 1).OrderBy(x => x.Value_IndexParametros))
                {
                    List<object> _parametroCab = null;

                    result = GetValueParametro(nombreArchivo, Step.ArchivoId.Value, parametrosCABIn, parametros, out _parametroCab);

                    if (!string.IsNullOrEmpty(result))
                    {
                        return result;
                    }
                    if (!_parametroCab.Any())
                    {
                        return "Error: No se encontro el Valor del Parametro:" + parametrosCABIn.NombreParametro;
                    }

                    cabezera = (string.IsNullOrEmpty(cabezera) ? "" : cabezera + "|") + _parametroCab[0].ToString().Replace(",", ".");
                }

                if (!string.IsNullOrEmpty(cabezera))
                {
                    cabezera = DataHelper.LimpiarCaracteresEspeciales(cabezera);

                    data.Add(cabezera);
                }
            }

            List<string> lineasData = new List<string>();

            //var constanteLine = configuracionLineas.Where(x => !string.IsNullOrEmpty(x.FILTROLINEAS) && x.FILTROLINEAS.IndexOf("LIN") != -1).Select(s => s.FILTROLINEAS.Split("=")[1]).FirstOrDefault();
            int noRegistros = 0;
            List<string> constante = new List<string>();

            foreach (var parametrosLinIn in configuracionParametros.Where(x => x.LineaArchivo == 2 && !string.IsNullOrEmpty(x.ValueParametro)).OrderBy(x => x.Value_IndexParametros))
            {
                List<object> _parametroLin = null;
                result = GetValueParametro(nombreArchivo, Step.ArchivoId.Value, parametrosLinIn, parametros, out _parametroLin);

                noRegistros = noRegistros < _parametroLin.Count() ? _parametroLin.Count() : noRegistros;

                if (_parametroLin.Count() == 1 && parametrosLinIn.ValueParametro.IndexOf("<") != -1)
                {
                    constante.Add(_parametroLin[0].ToString().Replace(",", "."));
                    continue;
                }

                for (int i = 0; i < noRegistros; i++)
                {

                    var _valueParametroConvert = _parametroLin.Count() - 1 >= i ? !String.IsNullOrEmpty(_parametroLin[i].ToString()) ? _parametroLin[i].ToString().Replace(",", ".") : String.Empty : String.Empty;


                    if (lineasData.Count() - 1 >= i)
                    {
                        if (constante.Any())
                        {
                            lineasData[i] += "|" + string.Join("|", constante.ToArray()) + (constante.Count() == 1 ? "|" : "") + _valueParametroConvert;
                        }
                        else
                        {
                            lineasData[i] += "|" + _valueParametroConvert;
                        }
                    }
                    else
                    {
                        if (constante.Any())
                        {
                            lineasData.Add(string.Join("|", constante.ToArray()) + (constante.Count() == 1 ? "|" : "") + _valueParametroConvert);
                        }
                        else
                        {
                            lineasData.Add(_valueParametroConvert);
                        }
                    }
                }

                constante.Clear();

            }

            if (lineasData.Any())
            {
                foreach (var linea in lineasData)
                {
                    data.Add(DataHelper.LimpiarCaracteresEspeciales(linea));
                }
            }

            if (data.Any())
            {
                logEstado.DataIn = String.Join(System.Environment.NewLine, data.ToArray());

                result = _DrimRepository.UpdateInterfaces_Archivos_Logs_Estados(ref logEstado, "LoadData", null);

                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }
            }



            try
            {
                using (wsDataExchange.ExecuteMappingsSoapClient wsCliente = new wsDataExchange.ExecuteMappingsSoapClient(wsDataExchange.ExecuteMappingsSoapClient.EndpointConfiguration.ExecuteMappingsSoap))
                {
                    wsDataExchange.ArrayOfString _data = new wsDataExchange.ArrayOfString();
                    _data.AddRange(data);

                    var respose = wsCliente.EjecutarMapping(Step.IdMapDataExchanged.Value.ToString(), _data, "1", true, out responseWs, out responseErrorWs);

                    if (!respose)
                    {
                        _logger.LogError(responseErrorWs);
                        result = _DrimRepository.UpdateInterfaces_Archivos_Logs_Estados(ref logEstado, "Error", responseErrorWs);
                    }
                    else
                    {
                        result = _DrimRepository.UpdateInterfaces_Archivos_Logs_Estados(ref logEstado, "Ejecutado", null);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("StepId:" + Step.StepId + ". Error:" + ex.Message);

                Interfaces_Procesos_Logs_Estados logError = new Interfaces_Procesos_Logs_Estados()
                {
                    DateCreate = DateTime.Now,
                    Estado = "Error",
                    ProcesoId = Step.ArchivoId,
                    StepId = Step.StepId,
                    NombreArchivo = nombreArchivo,
                    IdProcess = Global_IdProceso

                };

                _DrimRepository.UpdateInterfaces_Archivos_Logs_Estados(ref logError, "Error", ex.Message);

            }

            return result;
        }
        public string Step_LogEstado(string nombreArchivo, Interfaces_Procesos_Steps Step, List<Interfaces_Procesos_Steps_Parametros> parametros)
        {
            throw new NotImplementedException();
        }
        public string Step_Comparador(string nombreArchivo, Interfaces_Procesos_Steps Step, List<Interfaces_Procesos_Steps_Parametros> parametros)
        {
            throw new NotImplementedException();
        }
        public string Step_Traspasos(string nombreArchivo, Interfaces_Procesos_Steps Step, List<Interfaces_Procesos_Steps_Parametros> parametros)
        {
            throw new NotImplementedException();
        }
        public string Step_Inventarios(string nombreArchivo, Interfaces_Procesos_Steps Step, List<Interfaces_Procesos_Steps_Parametros> parametros)
        {
            throw new NotImplementedException();
        }
        public string Step_Albaran_Devoluciones(string nombreArchivo, Interfaces_Procesos_Steps Step, List<Interfaces_Procesos_Steps_Parametros> parametros)
        {
            throw new NotImplementedException();
        }
        public string Step_Albaran_TraspasosY(string nombreArchivo, Interfaces_Procesos_Steps Step, List<Interfaces_Procesos_Steps_Parametros> parametros)
        {
            throw new NotImplementedException();
        }



        public string ArchivosLoadFileNames(List<string> directorios)
        {
            string resul = string.Empty;
            List<string> files = new List<string>();

            foreach (var directorio in directorios)
            {
                List<Interfaces_Procesos_Logs_Estados> _interfaces_Archivos_Logs_Estados = new List<Interfaces_Procesos_Logs_Estados>();

                List<string> directorioData = directorio.Split("|").ToList();

                if (directorioData[0].Contains("ftp"))
                {
                    files = FTPManager.GetFilesFtp("ftp://" + directorioData[1] + directorioData[2], directorioData[3], directorioData[4]);
                }
                else
                {
                    files = FTPManager.GetFilesLocal(@"\\" + directorioData[1] + directorioData[2]);
                }


                files = files.Distinct().ToList();

                foreach (var file in files)
                {

                    string _estado = "LoadData";

                    var archivo = Global_archivosConfiguracion.Where(x => file.Split(".")[0].Contains(x.NombreProceso.Trim().ToUpper())).FirstOrDefault();

                    if (archivo == null)
                    {
                        _logger.LogError("Error no se encontro la configuracion del arhcivo:{file}", file);
                        _estado = string.Format("Error no se encontro la configuracion del arhcivo:{file}", file);
                    }

                    string dataFile = string.Empty;
                    string resultFileReader = string.Empty;

                    if (directorioData[0].Contains("ftp"))
                    {
                        resultFileReader = FTPManager.ReaderFileFtp("ftp://" + directorioData[1] + directorioData[2], directorioData[3], directorioData[4], out dataFile);
                    }
                    else
                    {
                        resultFileReader = FTPManager.ReaderFileLocal(@"\\" + directorioData[1] + directorioData[2], out dataFile);
                    }
                    if (!string.IsNullOrWhiteSpace(resultFileReader))
                    {
                        _logger.LogError("Error de lectura de datos archivo:{file}.Error:{Error}.", file, resultFileReader);
                        _estado = string.Format("Error de lectura de datos archivo:{file}.Error:{Error}.", file, resultFileReader);
                    }

                    if (string.IsNullOrWhiteSpace(dataFile))
                    {
                        _logger.LogError("Error el archivo no contiene datos:{file}.Error:{Error}.", file, resultFileReader);
                        _estado = string.Format("Error el archivo no contiene datos:{file}.Error:{Error}.", file, resultFileReader);
                    }

                    _interfaces_Archivos_Logs_Estados.Add(new Interfaces_Procesos_Logs_Estados()
                    {
                        DateCreate = Global_FechaProceso,
                        Estado = _estado,
                        ProcesoId = archivo != null ? archivo.ArchivoId : null,
                        NombreArchivo = file,
                        IdProcess = Global_IdProceso,
                        DataOut = dataFile,
                        StepId = 0
                    });
                }


                resul = _DrimRepository.InsertInterfaces_Archivos_Logs_Estados(_interfaces_Archivos_Logs_Estados);

                if (!string.IsNullOrEmpty(resul))
                {
                    _logger.LogCritical("Error no se guardaron datos de los archivos log estados:{direcotiro}, {result}", directorio, resul);
                    break;
                }

                Global_interfaces_Archivos_Logs_Estados.AddRange(_interfaces_Archivos_Logs_Estados.Where(x => x.Estado == "LoadData"));


            }

            return resul;
        }
        public string GetValueParametro(string nombreArchivo, long ArchivoId, Interfaces_Procesos_Steps_Parametros parametro, List<Interfaces_Procesos_Steps_Parametros> parametros, out List<object> resultObject)
        {
            resultObject = new List<object>();
            string exprecionNueva = "";
            int IdParametros = 0;
            int index = 0;
            string tipoparametro = "";
            var variables = new Dictionary<string, FloatingPoint>();
            string result = string.Empty;
            object valorParametroMap = null;
            var ValueParametro = parametro.ValueParametro;
            var ValueDataType = parametro.TypeDataParametro;
            bool IsOperacionMath = ValueParametro.ToCharArray().ToList().Any(x => (new List<char> { '+', '-', '*', '/', '%' }).Contains(x));
            var caracteres = ValueParametro.ToCharArray();
            Dictionary<string, List<object>> valuePairs = new Dictionary<string, List<object>>();
            try
            {
                for (int i = 0; i < caracteres.Length; i++)
                {

                    var caracter = caracteres[i];

                    valorParametroMap = null;
                    IdParametros = 0;

                    if (caracter == '[')
                    {
                        tipoparametro = "accion";
                    }
                    else if (caracter == '<')
                    {
                        tipoparametro = "constante";
                    }
                    else if (caracter == '>' || caracter == ']')
                    {
                        continue;
                    }
                    else if ((new List<string> { "+", "-", "*", "/", "%" }).Contains(caracter.ToString()))
                    {
                        exprecionNueva += caracter.ToString();
                    }
                    else
                    {
                        if (tipoparametro == "accion")
                        {
                            if (int.TryParse(caracter.ToString(), out IdParametros))
                            {
                                var dataValueParametro = Global_interfaces_Steps_ResponsesDatas.Where(x => x.ParametroId == IdParametros).ToList();

                                if (!dataValueParametro.Any())
                                {
                                    _logger.LogError("No se encontro el valor del parametro :" + IdParametros);
                                    return "Error: No se encontro el valor del parametro :" + IdParametros;
                                }

                                object itemObj = null;
                                List<object> listObj = new List<object>();

                                foreach (var value in dataValueParametro)
                                {
                                    result += value.Value.ConvertValueByTypeData(value.TipoValueData, string.Empty, out itemObj);
                                    listObj.Add(itemObj);
                                }

                                if (!string.IsNullOrEmpty(result))
                                {
                                    _logger.LogError("Error: al Convertir el Valor del parametro :" + IdParametros + ". " + result);
                                    return "Error: al Convertir el Valor del parametro :" + IdParametros + ". " + result;
                                }

                                valuePairs.Add("[" + IdParametros + "]", listObj);
                            }
                        }
                        else
                        {
                            string strValorParametroMap = ValueParametro.Substring(i, (ValueParametro.Length) - i);

                            i = i + strValorParametroMap.IndexOf('>');

                            strValorParametroMap = strValorParametroMap.Substring(0, strValorParametroMap.IndexOf('>'));

                            object itemObjConstatnte = null;
                            result = strValorParametroMap.ConvertValueByTypeData(ValueDataType, parametro.FormatDateTime, out itemObjConstatnte);

                            if (!string.IsNullOrEmpty(result))
                            {
                                _logger.LogError("Error: al Convertir el Valor del parametro :" + parametro.ParametroId + ". Value:" + strValorParametroMap + ". " + result);
                                return "Error: al Convertir el Valor del parametro :" + parametro.ParametroId + ". Value:" + strValorParametroMap + ". " + result;
                            }

                            valuePairs.Add("<" + strValorParametroMap + ">", new List<object>() { itemObjConstatnte });


                        }
                    }
                }

                if (!IsOperacionMath)
                {
                    resultObject.AddRange(valuePairs[valuePairs.Keys.First()]);
                    return result;
                }

                List<string> formatsData = new List<string>();
                formatsData.Add("yyyyMMdd");
                formatsData.Add("yyyyMMddHHmmss");
                formatsData.Add("yyyy-MM-dd HH:mm:ss");
                formatsData.Add("yyyy/MM/dd HH:mm:ss");
                formatsData.Add("ddMMyyyy");
                formatsData.Add("ddMMyyyyHHmmss");
                formatsData.Add("dd-MM-yyyy HH:mm:ss");
                formatsData.Add("dd/MM/yyyy");
                formatsData.Add("dd/MM/yyyy HH:mm:ss");


                List<string> formatsTime = new List<string>();
                formatsData.Add("HHmmss");
                formatsData.Add("HH:mm:ss");

                if (formatsData.Contains(valuePairs.Keys.First().Replace("<","").Replace(">", "")))
                {
                    var datetimeNow = DateTime.ParseExact(valuePairs[valuePairs.Keys.First()][0].ToString(), valuePairs.Keys.First().Replace("<", "").Replace(">", ""), System.Globalization.CultureInfo.InvariantCulture);

                    var tipoAgregado = valuePairs[valuePairs.Keys.Last()][0].ToString();
                    int minutos = 0;

                    if (tipoAgregado.IndexOf("d") != -1)
                    {
                        minutos = 1440 * int.Parse(tipoAgregado.Split("d")[0]) ;
                    }
                    else if (tipoAgregado.IndexOf("M") != -1)
                    {
                        minutos = (1440 * 31) * int.Parse(tipoAgregado.Split("M")[0]); 
                    }
                    else if (tipoAgregado.IndexOf("H") != -1)
                    {
                        minutos = 60 * int.Parse(tipoAgregado.Split("H")[0]); 
                    }
                    else if (tipoAgregado.IndexOf("m") != -1)
                    {
                        minutos = 1 * int.Parse(tipoAgregado.Split("m")[0]); ;
                    }

                    if (ValueParametro.IndexOf("-") != -1) 
                    {
                        datetimeNow = datetimeNow.AddMinutes(minutos*-1);
                    }
                    else if (ValueParametro.IndexOf("+") != -1)
                    {
                        datetimeNow = datetimeNow.AddMinutes(minutos);
                    }

                    var valueObjetDateTime = datetimeNow.ToString(valuePairs.Keys.First().Replace("<", "").Replace(">", ""));

                    resultObject.Add(valueObjetDateTime);

                    return result;

                }
                else if (formatsTime.Contains(ValueParametro))
                {


                }
                else
                {

                    int indexKeys = 0;
                    string formulaOperacion = ValueParametro;

                    foreach (var _variable in ValueParametro.Split(new char[] { '+', '-', '*', '/', '%' }))
                    {
                        formulaOperacion = formulaOperacion.Replace(_variable, indexKeys.GetVariableByIndex());
                        indexKeys++;
                    }

                    int rows = 0;
                    bool IsMatrixZiseOk = false;

                    foreach (var _parametro in valuePairs.Keys)
                    {
                        rows = rows < valuePairs[_parametro].Count() ? valuePairs[_parametro].Count() : rows;
                    }

                    for (int i = 0; i <= rows; i++)
                    {
                        indexKeys = 0;
                        Dictionary<string, FloatingPoint> valoresFormula = new Dictionary<string, FloatingPoint>();

                        foreach (var _parametro in valuePairs.Keys)
                        {
                            if (_parametro.IndexOf("<") == 0 && valuePairs[_parametro].Count() > 1 && valuePairs[_parametro].Count() < i)
                            {
                                _logger.LogError("Error: el tamaño de las matrices de los valores del parametro son distintas:" + ValueParametro);
                                return "Error: el tamaño de las matrices de los valores del parametro son distintas:" + ValueParametro + ". _parametro:" + _parametro;
                            }

                            var valorParametro = _parametro.IndexOf("<") != -1 || valuePairs[_parametro].Count() == 1 ? valuePairs[_parametro][0] : valuePairs[_parametro][i];
                            valoresFormula.Add(indexKeys.GetVariableByIndex(), Convert.ToSingle(valorParametro));
                            indexKeys++;
                        }

                        var valorResultadoFormula = CalcularOperacion(formulaOperacion, valoresFormula);

                        resultObject.Add(valorResultadoFormula);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al intentar el valor del Parametro." + parametro.ParametroId + ":" + ex.Message);

                Interfaces_Procesos_Logs_Estados logError = new Interfaces_Procesos_Logs_Estados()
                {
                    ProcesoId = ArchivoId,
                    DateCreate = DateTime.Now,
                    NombreArchivo = nombreArchivo,
                    IdProcess = Global_IdProceso,
                    StepId = parametro.StepId
                };

                _DrimRepository.UpdateInterfaces_Archivos_Logs_Estados(ref logError, "Error", "Error al intentar el valor del Parametro." + ex.Message);
            }
            return result;
        }
        public double CalcularOperacion(string operacion, Dictionary<string, FloatingPoint> arg)
        {

            double resultado = 0;

            try
            {
                var expresion = Infix.ParseOrThrow(operacion);
                resultado = Evaluate.Evaluate(arg, expresion).RealValue;
            }
            catch (Exception)
            {
                Console.WriteLine("Error en la operación.");
            }

            return resultado;
        }
        public string AgregaDatosArchivosLoadData(string nombreArchivo, long IdArchivo, string Data, Interfaces_Procesos_Steps Step)
        {

            Global_interfaces_Steps_ResponsesDatas = new List<Interfaces_Steps_ResponseData>();

            string result = string.Empty;

            int indexParametro = 0;
            int _Linea = 0;
            int row = 0;

            foreach (var linea in Data.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None))
            {
                indexParametro = 0;
                _Linea = 0;

                foreach (var parametro in linea.Split(new string[] { ",", "|", ";" }, StringSplitOptions.None))
                {
                    if (parametro.IndexOf("CA") != -1)
                    {
                        _Linea = 1;
                        continue;
                    }
                    else if (parametro.IndexOf("LI") != -1)
                    {
                        _Linea = 2;
                        continue;
                    }


                    var configuracionParametro = Step.Parametros.Where(x => x.LineaArchivo == _Linea && x.Value_IndexParametros == indexParametro).FirstOrDefault();

                    if (configuracionParametro == null)
                    {
                        return "AgregaDatosArchivosLoadAData: No se encontro la configuracion del Parametro Archivo:" + IdArchivo + ". Step:" + Step.StepId + " Linea:" + linea + ". indexParametro:" + indexParametro;
                    }

                    Global_interfaces_Steps_ResponsesDatas.Add(new Interfaces_Steps_ResponseData()
                    {
                        IdArchivo = IdArchivo,
                        IdStep = Step.StepId,
                        IndexValue = indexParametro,
                        IsKey = configuracionParametro.IsKey.HasValue && configuracionParametro.IsKey.Value,
                        LineaValue = Convert.ToInt16(_Linea),
                        Value = parametro,
                        NombreArchivo = nombreArchivo,
                        Row = row,
                        ParametroId = configuracionParametro.ParametroId
                    });


                    indexParametro++;
                }

                row++;
            }

            return result;
        }



        public static bool ValidateCronExpression(string cronExpression, DateTime targetDateTime)
        {
            try
            {

                DateTime _targetDateTime = new DateTime(targetDateTime.Year, targetDateTime.Month, targetDateTime.Day, targetDateTime.Hour, targetDateTime.Minute, 0);


                DateTime _datetime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);

                // Parsea la expresión cron
                var schedule = CrontabSchedule.Parse(cronExpression);

                var netxOcurrence = schedule.GetNextOccurrence(_targetDateTime);

                // Determina si la expresión cron se activa en la fecha y hora específicas
                return netxOcurrence <= _datetime;
            }
            catch (Exception ex)
            {
                // Si hay un error al analizar la expresión cron, puedes manejarlo aquí.
                Console.WriteLine($"Error al validar la expresión cron: {ex.Message}");
                return false;
            }
        }

    }
}
