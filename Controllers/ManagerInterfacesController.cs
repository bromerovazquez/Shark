using Drim.Interfaces.MVC.Connection;
using Drim.Interfaces.MVC.Repositorys;
using Drim.Interfaces.MVC.Entities;
using Drim.Interfaces.MVC.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using Drim.Interfaces.MVC.ViewModels;
using Newtonsoft.Json;

namespace Drim.Interfaces.MVC.Controllers
{
    [AllowAnonymous]
    public class ManagerInterfacesController : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private UsuarioDrim usDrim = null;
        private readonly IDrimRepository _DrimRepository;
        private readonly IManagerRepository _ManagerRepository;




        public ManagerInterfacesController(IDrimRepository DrimRepository, IHttpContextAccessor accessor, IManagerRepository ManagerRepository)
        {
            _httpContextAccessor = accessor;
            usDrim = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<UsuarioDrim>("usDrim");
            _DrimRepository = DrimRepository;
            _ManagerRepository = ManagerRepository;

        }



        public IActionResult Index()
        {

            var archivos = _DrimRepository.GetInterfaces_Archivos(null, null);

            return View(archivos);
        }


        public IActionResult LogsInterfaces() 
        {


            return View();
        }

        public IActionResult GetArchivos()
        {
            var archivos = _DrimRepository.GetInterfaces_Archivos(null, null);
            return PartialView("ManagerInterfaces/ProcesosGrid", archivos);
        }


        public IActionResult GetContentArchivo(long id, long focusStepId = -1)
        {



            List<Interfaces_Procesos_Steps_Parametros> parametros = new List<Interfaces_Procesos_Steps_Parametros>();

            var archivo = _DrimRepository.GetInterfaces_Archivos(id, null).FirstOrDefault();

            if (archivo == null)
            {
                var orden = _DrimRepository.GetUltimoOrdenArchivo();
                archivo = new Interfaces_Procesos() { Orden = orden };
            }

            ViewData["focusStepId"] = focusStepId;

            ViewData["allParametrosArchivoSteps"] = archivo.Steps.SelectMany(s => s.Parametros).ToList();

            var maps = _ManagerRepository.GetMapsDataExchage().Select(s => new { value = s.IDMAP, text = s.IDMAP + "-" + s.NOMBRE });

            ViewData["IdsMapDataExchanged"] = new SelectList(maps, "value", "text");


            ViewData["ultimoOrdenStep"] = _DrimRepository.GetOrdenStepByArchivo(id);


            return PartialView("ManagerInterfaces/ProcesoContent", archivo);
        }

        [HttpPost]
        public IActionResult UpdateArchivo(string jsonArchivo)
        {
            string result = string.Empty;
            Interfaces_Procesos archivo = new Interfaces_Procesos();

            try
            {
                archivo = JsonConvert.DeserializeObject<Interfaces_Procesos>(jsonArchivo);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            result = _DrimRepository.InsertInterfaces_Archivos(ref archivo);

            return GetContentArchivo(archivo.ArchivoId);
        }


        [HttpPost]
        public IActionResult UpdateStep(string jsonStep, string jsonParametrosOutput, string jsonParametrosInput)
        {
            string result = string.Empty;
            List<Interfaces_Procesos_Steps_Parametros> parametrosInput = null;
            List<Interfaces_Procesos_Steps_Parametros> parametroOutput = null;
            Interfaces_Procesos_Steps step = null;

            try
            {
                if (!string.IsNullOrEmpty(jsonParametrosOutput))
                {
                    parametrosInput = JsonConvert.DeserializeObject<List<Interfaces_Procesos_Steps_Parametros>>(jsonParametrosInput);
                }

                if (!string.IsNullOrEmpty(jsonParametrosOutput))
                {

                    parametroOutput = JsonConvert.DeserializeObject<List<Interfaces_Procesos_Steps_Parametros>>(jsonParametrosOutput);
                }

                step = JsonConvert.DeserializeObject<Interfaces_Procesos_Steps>(jsonStep);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            result = _DrimRepository.InsertInterfaces_ArchivosSteps(ref step);

            if (!string.IsNullOrEmpty(result))
                return Json(new { result = "NOK", mensaje = result });

            if (parametroOutput != null && parametroOutput.Any())
                result = _DrimRepository.InsertInterfaces_ArchivosAccionParametrosRangeByStepId(step.StepId, "out", ref parametroOutput);

            if (!string.IsNullOrEmpty(result))
                return Json(new { result = "NOK", mensaje = result });

            if (parametrosInput != null && parametrosInput.Any())
                result = _DrimRepository.InsertInterfaces_ArchivosAccionParametrosRangeByStepId(step.StepId, "in", ref parametrosInput);

            if (!string.IsNullOrEmpty(result))
                return Json(new { result = "NOK", mensaje = result });

            return GetContentArchivo(step.ArchivoId.Value, step.StepId);

        }

        public IActionResult UpdateValueParametro(long idparametro, string valueparametro)
        {


            if (!DataHelper.ValidaCadena(valueparametro))
                return Json(new { result = "NOK", mensaje = "El valor del parametro no es valido" });



            var parametro = _DrimRepository.GetInterfaces_Accion_Parametros(idparametro, null).First();
            parametro.ValueParametro = valueparametro;

            if (!string.IsNullOrEmpty(_DrimRepository.InsertInterfaces_ArchivosAccionParametros(parametro)))
                return Json(new { result = "NOK", mensaje = "Error al actualizar parametro" });



            return GetContentArchivo(parametro.Step.ArchivoId.Value, parametro.Step.StepId);

        }


        public IActionResult GetSetValueParametroStep(long idparametro)
        {
            var parametro = _DrimRepository.GetInterfaces_Accion_Parametros(idparametro, null).First();
            var archivo = _DrimRepository.GetInterfaces_Archivos(parametro.Step.ArchivoId, null).FirstOrDefault();

            parametro.ValueParametroDescripcion = String.IsNullOrEmpty(parametro.ValueParametro) ? String.Empty : parametro.ValueParametro.GetDescripcionValueParametroStep(archivo.Steps.SelectMany(s => s.Parametros).ToList());

            ViewData["parametro"] = parametro;
            return PartialView("ManagerInterfaces/SetValueParametroStep", archivo);
        }



        public IActionResult GetParametrosQueryEntrada(long ArchivoId, long StepId, string Descripcion, string Direccion, short IdTipoAccion, string TipoQuerySQL, string QuerySQL, string DBSQL)
        {

            List<Interfaces_Procesos_Steps_Parametros> parametros = new List<Interfaces_Procesos_Steps_Parametros>();


            if (StepId == 0)
            {
                short? _orden = _DrimRepository.GetOrdenStepByArchivo(ArchivoId);

                Interfaces_Procesos_Steps stepNew = new Interfaces_Procesos_Steps()
                {
                    ArchivoId = ArchivoId,
                    DateCreate = DateTime.Now,
                    Descripcion = Descripcion,
                    Direccion = char.Parse(Direccion),
                    IdTipoAccion = IdTipoAccion,
                    UserId = usDrim != null ? usDrim.Id : 0,
                    Orden = _orden,
                    TipoQuerySQL = Convert.ToChar(TipoQuerySQL),
                    QuerySQL = QuerySQL,
                    DBSQL = DBSQL,


                };

                _DrimRepository.InsertInterfaces_ArchivosSteps(ref stepNew);
                StepId = stepNew.StepId;
            }
            else
            {

                var steps = _DrimRepository.GetInterfaces_Archivo_Steps(StepId, null);
                if (steps != null && steps.Any())
                {
                    var stepUpdate = steps.First();
                    stepUpdate.DBSQL = DBSQL;
                    stepUpdate.QuerySQL = QuerySQL;
                    stepUpdate.TipoQuerySQL = Convert.ToChar(TipoQuerySQL);
                    stepUpdate.UserId = usDrim != null ? usDrim.Id : 0;

                    _DrimRepository.InsertInterfaces_ArchivosAccion(stepUpdate);

                }

            }

            short? index = 0;
            if (TipoQuerySQL == "s")
            {
                var parametro = DataHelper.GetStoredProcedureParameters(DBSQL.GetDB().GetConectionString(), QuerySQL);

                foreach (DataRow campo in parametro.Rows)
                {

                    parametros.Add(new Interfaces_Procesos_Steps_Parametros()
                    {
                        NombreParametro = campo["PARAMETER_NAME"].ToString(),
                        TypeDataParametro = campo["DATA_TYPE"].ToString(),
                        Value_IndexParametros = index,
                        StepId = StepId,
                        TipoParametros = "in",
                        LineaArchivo = 1,
                        IsKey = true

                    });
                    index++;
                }

            }
            else
            {
                var variables = DataHelper.GetColumnsWithVariables(QuerySQL, DBSQL);


                foreach (var variable in variables)
                {

                    parametros.Add(new Interfaces_Procesos_Steps_Parametros()
                    {
                        NombreParametro = variable.Variable,
                        TypeDataParametro = variable.TipoDato.NormalizeTypeData(),
                        Value_IndexParametros = index,
                        StepId = StepId,
                        TipoParametros = "in",
                        LineaArchivo = 1,
                        IsKey = variable.IsKey

                    });
                    index++;
                }


            }

            //ViewData["tipoParametro"] = "in";
            //ViewData["IdStep"] = StepId;

            _DrimRepository.InsertInterfaces_ArchivosAccionParametrosRangeByStepId(StepId, "in", ref parametros);


            //return PartialView("ManagerInterfaces/StepParametros", parametros);
            return GetContentArchivo(ArchivoId, StepId);
        }



        public IActionResult GetParametrosQuerySalida(long ArchivoId, long StepId, string Descripcion, string Direccion, short IdTipoAccion, string TipoQuerySQL, string QuerySQL, string DBSQL)
        {

            List<Interfaces_Procesos_Steps_Parametros> parametrosOut = new List<Interfaces_Procesos_Steps_Parametros>();
            string result = string.Empty;

            var parametrosIn = _DrimRepository.GetInterfaces_Accion_Parametros(null, StepId).Where(x => x.TipoParametros == "in").ToList();


            if (parametrosIn != null && parametrosIn.Any())
            {
                parametrosIn = parametrosIn.Where(x => x.TipoParametros == "in").ToList();
            }

            result = DataHelper.GetParametrosQuerySqlOut(QuerySQL, TipoQuerySQL == "s" ? CommandType.StoredProcedure : CommandType.Text, DBSQL, StepId, parametrosIn, out parametrosOut);

            if (!string.IsNullOrEmpty(result))
            {
                return Json(new { result = "NOK", mensaje = result });
            }

            //ViewData["tipoParametro"] = "out";

            result = _DrimRepository.InsertInterfaces_ArchivosAccionParametrosRangeByStepId(StepId, "out", ref parametrosOut);


            if (string.IsNullOrEmpty(result))
            {
                //return PartialView("ManagerInterfaces/StepParametros", parametrosOut);
                return GetContentArchivo(ArchivoId, StepId);
            }
            else
            {
                return Json(new { result = "NOK", mensaje = result });
            }
        }


        public IActionResult GetParametrosDataExchangedEntrada(int IdMap, long ArchivoId, long StepId, string Descripcion, string Direccion, short orden)
        {

            List<Interfaces_Procesos_Steps_Parametros> parametrosIn = new List<Interfaces_Procesos_Steps_Parametros>();
            string result = string.Empty;


            if (StepId == 0)
            {
                short? _orden = _DrimRepository.GetOrdenStepByArchivo(ArchivoId);

                Interfaces_Procesos_Steps stepNew = new Interfaces_Procesos_Steps()
                {
                    ArchivoId = ArchivoId,
                    DateCreate = DateTime.Now,
                    Descripcion = Descripcion,
                    Direccion = char.Parse(Direccion),
                    IdTipoAccion = 3,
                    UserId = usDrim != null ? usDrim.Id : 0,
                    Orden = _orden,
                    IdMapDataExchanged = IdMap
                };

                _DrimRepository.InsertInterfaces_ArchivosSteps(ref stepNew);
                StepId = stepNew.StepId;
            }
            else
            {

                var steps = _DrimRepository.GetInterfaces_Archivo_Steps(StepId, null);
                if (steps != null && steps.Any())
                {
                    var stepUpdate = steps.First();
                    stepUpdate.IdMapDataExchanged = IdMap;
                    stepUpdate.UserId = usDrim != null ? usDrim.Id : 0;
                    _DrimRepository.InsertInterfaces_ArchivosAccion(stepUpdate);
                }
            }


            var parametros = _ManagerRepository.GetParametrosMapsDataExchage(IdMap);

            var configLineas = _ManagerRepository.GetParametrosLineaFilterMapsDataExchage(IdMap);

            foreach (var parametroLinea in parametros.GroupBy(x => x.IDFILE).OrderBy(x => x.Key))
            {
                int index = 0;

                var configLinea = configLineas.Where(x => x.IDFILE == parametroLinea.Key).FirstOrDefault();


                foreach (var parametro in parametroLinea.OrderBy(x => x.POSICION))
                {

                    parametrosIn.Add(new Interfaces_Procesos_Steps_Parametros()
                    {
                        LineaArchivo = configLinea != null && !string.IsNullOrEmpty(configLinea.FILTROLINEAS) ? configLinea.FILTROLINEAS.IndexOf("CAB") != -1 ? (short?)1 : (short?)2 : Convert.ToInt16(parametro.IDFILE),
                        NombreParametro = parametro.FIELDNAME,
                        Value_IndexParametros = Convert.ToInt16(index),
                        TipoParametros = "in",
                        TypeDataParametro = "nvarchar",
                        StepId = StepId,
                        ValueParametro = configLinea != null && !string.IsNullOrEmpty(configLinea.FILTROLINEAS) && configLinea.FILTROLINEAS.Split("=").Count() > 1 && configLinea.FILTROLINEAS.Split("=")[0].Replace("\t", "") == parametro.FIELDNAME && configLinea.IDFILE == parametro.IDFILE ? "<" + configLinea.FILTROLINEAS.Split("=")[1].Replace("\t", "") + ">" : string.Empty
                    });

                    index++;
                }
            }

            //ViewData["tipoParametro"] = "in";
            //ViewData["IdStep"] = StepId;

            _DrimRepository.InsertInterfaces_ArchivosAccionParametrosRangeByStepId(StepId, "in", ref parametrosIn);


            if (string.IsNullOrEmpty(result))
            {
                //return PartialView("ManagerInterfaces/StepParametros", parametrosIn);
                return GetContentArchivo(ArchivoId, StepId);
            }
            else
            {
                return Json(new { result = "NOK", mensaje = result });
            }
        }



        [HttpPost]
        public IActionResult LoadDataFile()
        {

            long idArchivo = Request.Form.Where(x => x.Key == "idArchivo").Any() ? Convert.ToInt64(Request.Form.Where(x => x.Key == "idArchivo").FirstOrDefault().Value.ToString()) : 0;

            long IdStep = Request.Form.Where(x => x.Key == "IdStep").Any() ? Convert.ToInt64(Request.Form.Where(x => x.Key == "IdStep").FirstOrDefault().Value.ToString()) : 0;

            short IdTipoAccion = Request.Form.Where(x => x.Key == "IdTipoAccion").Any() ? Convert.ToInt16(Request.Form.Where(x => x.Key == "IdTipoAccion").FirstOrDefault().Value.ToString()) : (short)0;

            string Descripcion = Request.Form.Where(x => x.Key == "Descripcion").Any() ? Request.Form.Where(x => x.Key == "Descripcion").FirstOrDefault().Value.ToString() : string.Empty;

            string Direccion = Request.Form.Where(x => x.Key == "Direccion").Any() ? Request.Form.Where(x => x.Key == "Direccion").FirstOrDefault().Value.ToString() : string.Empty;

            bool IsParametroNameFile = Request.Form.Where(x => x.Key == "IsParametroNameFile").Any() ? Convert.ToBoolean(Request.Form.Where(x => x.Key == "IsParametroNameFile").FirstOrDefault().Value.ToString()) : false;

            string SeparadorParametrosNameFile = Request.Form.Where(x => x.Key == "SeparadorParametrosNameFile").Any() ? Request.Form.Where(x => x.Key == "SeparadorParametrosNameFile").FirstOrDefault().Value.ToString() : string.Empty;

            bool IsCABParametros = Request.Form.Where(x => x.Key == "IsCABParametros").Any() ? Convert.ToBoolean(Request.Form.Where(x => x.Key == "IsCABParametros").FirstOrDefault().Value.ToString()) : false;




            IFormFile _file = null;
            List<Interfaces_Procesos_Steps_Parametros> parametroConfig = new List<Interfaces_Procesos_Steps_Parametros>();
            var files = Request.Form.Files;


            if (IdStep == 0)
            {
                short? _orden = _DrimRepository.GetOrdenStepByArchivo(idArchivo);


                Interfaces_Procesos_Steps stepNew = new Interfaces_Procesos_Steps()
                {
                    ArchivoId = idArchivo,
                    DateCreate = DateTime.Now,
                    Descripcion = Descripcion,
                    Direccion = char.Parse(Direccion),
                    IdTipoAccion = IdTipoAccion,
                    UserId = usDrim != null ? usDrim.Id : 0,
                    Orden = _orden,
                    IsParametroNameFile = IsParametroNameFile,
                    SeparadorParametrosNameFile = SeparadorParametrosNameFile

                };


                _DrimRepository.InsertInterfaces_ArchivosSteps(ref stepNew);
                IdStep = stepNew.StepId;
            }






            if (files != null && files.Any())
            {
                _file = files[0];

                short indexParametro = 0;
                short? _Linea = 0;
                int _lineasTop = 2;
                int _lineas = 0;
                string tipo = string.Empty;


                if (IsParametroNameFile)
                {
                    var parametrosNameFile = _file.FileName.IndexOf(".") != -1 ? _file.FileName.Substring(_file.FileName.IndexOf(".") + 1) : _file.FileName;

                    foreach (var parametroName in parametrosNameFile.Split(SeparadorParametrosNameFile))
                    {
                        parametroConfig.Add(new Interfaces_Procesos_Steps_Parametros()
                        {
                            IndexParametroNameFile = indexParametro,
                            Value_IndexParametros = indexParametro,
                            NombreParametro = "NameFile_Index_" + indexParametro,
                            IsKey = true,
                            IsParametroNameFile = true,
                            LineaArchivo = 1,
                            StepId = IdStep,
                            TipoParametros = "out",
                            ValueParametro = parametroName,
                            TypeDataParametro = parametroName.GetTipoData(),

                        });

                        indexParametro++;
                    }
                }



                string data = string.Empty;

                using (var reader = new StreamReader(_file.OpenReadStream()))
                {
                    data = reader.ReadToEnd();
                }



                int row = 0;

                foreach (var linea in data.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None))
                {
                    indexParametro = 0;
                    _Linea = 1;

                    if (_lineas == _lineasTop)
                        break;

                    foreach (var parametro in linea.Split(new string[] { ",", "|", ";" }, StringSplitOptions.None))
                    {
                        if (_lineasTop == 0 && IsCABParametros && indexParametro == 0)
                        {
                            _Linea = 1;
                            parametroConfig.Add(new Interfaces_Procesos_Steps_Parametros()
                            {
                                LineaArchivo = 1,
                                NombreParametro = "CABECERA",
                                TipoParametros = "out",
                                TypeDataParametro = "string",
                                ValueParametro = parametro,
                                Value_IndexParametros = indexParametro,
                                StepId = IdStep

                            });
                            indexParametro++;
                            continue;

                        }
                        else if (_lineasTop == 1 && IsCABParametros && indexParametro == 0)
                        {
                            _Linea = 2;

                            parametroConfig.Add(new Interfaces_Procesos_Steps_Parametros()
                            {
                                LineaArchivo = 2,
                                NombreParametro = "LINEA",
                                TipoParametros = "out",
                                TypeDataParametro = "string",
                                ValueParametro = parametro,
                                Value_IndexParametros = indexParametro,
                                StepId = IdStep
                            });
                            indexParametro++;
                            continue;
                        }


                        parametroConfig.Add(new Interfaces_Procesos_Steps_Parametros()
                        {
                            LineaArchivo = _Linea,
                            NombreParametro = "index_" + indexParametro,
                            TipoParametros = "out",
                            TypeDataParametro = parametro.GetTipoData(),
                            ValueParametro = parametro,
                            Value_IndexParametros = indexParametro,
                            StepId = IdStep
                        });


                        indexParametro++;
                    }

                    _lineas++;
                }

                _DrimRepository.InsertInterfaces_ArchivosAccionParametrosRangeByStepId(IdStep, "out", ref parametroConfig);
            }

            //ViewData["IdStep"] = IdStep;
            //ViewData["tipoParametro"] = "out";

            //return PartialView("ManagerInterfaces/StepParametros", parametroConfig);
            return GetContentArchivo(idArchivo, IdStep);
        }



        public IActionResult GetStepParametros(long idStep, string tipoParametro)
        {

            var parametrosStep = _DrimRepository.GetInterfaces_Accion_Parametros(null, idStep);

            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };


            var parametrosStepJson = JsonConvert.SerializeObject(parametrosStep, settings);

            return Json(parametrosStepJson);

        }



        [HttpGet]
        public async Task<ActionResult> GetColumnsQuerySql(string query, CommandType type, int top = 5)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            int rowsCount = 0;

            using (DAL dal = new DAL(ConnectionCatalogDB.G001))
            {
                using (var rdr = dal.DataReader(query, type))
                {
                    while (rdr.Read())
                    {

                        if (top == rowsCount)
                            break;

                        Dictionary<string, object> newRow = new Dictionary<string, object>();

                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            if (!rdr.IsDBNull(i))
                            {
                                string fieldName = rdr.GetName(i);
                                newRow.Add(fieldName, rdr.GetValue(i));
                            }
                        }
                        result.Add(newRow);
                        rowsCount++;
                    }
                }
            }
            return PartialView("ManagerInterfaces/SelectColumnSqlMap", result);
        }


        public async Task<ActionResult> GetLogDetalle(long id) 
        {
            var logDetalle = await  _DrimRepository.GetInterfaces_LogDetalle(id);
            return PartialView("ManagerInterfaces/LogDetalle", logDetalle);
        }

    }
}
