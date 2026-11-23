using Drim.Interfaces.MVC.Entities;
using Drim.Interfaces.MVC.Models;
using Drim.Interfaces.MVC.Repositorys;
using Drim.Interfaces.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Drim.Interfaces.MVC.Controllers
{
    public class AlbaranesController : Controller
    {

        private readonly IManagerRepository _ManagerRepository;
        private readonly ITraspasosModel _TraspasosModel;
        private readonly IAlbaranesModel _AlbaranesModel;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private UsuarioDrim usDrim = null;

        private List<MenuRol> MenuRolList = null;

        private List<string> rolDrim = null;


        private List<int> codproveedores { get; set; }

        private List<SelectListItem> almacenes { get; set; }

        public AlbaranesController(IAlbaranesModel albaranesModel, IManagerRepository managerRepository, ITraspasosModel TraspasosModel, IHttpContextAccessor accessor)
        {
            _AlbaranesModel = albaranesModel;
            _httpContextAccessor = accessor;
            usDrim = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<UsuarioDrim>("usDrim");
            rolDrim = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<List<string>>("rolDrim");
            MenuRolList = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<List<MenuRol>>("menuUserRol");
            _ManagerRepository = managerRepository;
            _TraspasosModel = TraspasosModel;
            codproveedores = new List<int>() { 189, 369, 246, 3069, 3361 };

            almacenes = _ManagerRepository.GetAlmacenesSelect(false);
        }

        public IActionResult Index()
        {

            ViewData["almacenes"] = almacenes;

            DateTime fechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            DateTime fechaFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            List<SelectListItem> proveedores = new List<SelectListItem>();
            proveedores.Add(new SelectListItem() { Text = "--Todos--", Value = "0" });
            proveedores.AddRange(_ManagerRepository.GetProveedores(null, null).Select(s => new SelectListItem() { Value = s.CODPROVEEDOR.ToString(), Text = s.NOMPROVEEDOR }).ToList());
            ViewData["proveedores"] = proveedores;

            return View();
        }


        public async Task<IActionResult> GetAlbaranes(string codAlmacenOrigen, string codAlmacenDestino, DateTime fechaInicio, DateTime fechaFin, string estado, int? codArticulo, int? codProveedor)
        {

            if (fechaInicio.Hour == 0)
            {
                fechaInicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 0, 0, 0);
                fechaFin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, 23, 59, 59);
            }


            return PartialView("Albaranes/AlbaranesOrigenDestinoProveedorGrid");
        }



        #region Devoluciones Traspasos (Y) (YY)

        public IActionResult Devoluciones()
        {

            ViewData["almacenes"] = almacenes;


            List<SelectListItem> proveedores = new List<SelectListItem>();
            
            proveedores.Add(new SelectListItem() { Text = "--Todos--", Value = "0" });

            proveedores.AddRange(_ManagerRepository.GetProveedores(null, null).Where(x => codproveedores.Contains(x.CODPROVEEDOR)).Select(s => new 
            SelectListItem() { Value = s.CODPROVEEDOR.ToString(), Text = s.NOMPROVEEDOR }).ToList());
            
            
            ViewData["proveedores"] = proveedores;

            //DateTime fechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            //DateTime fechaFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            //var traspasoDevoluciones = _TraspasosModel.GetTraspasosProveedorOrigenDestinoDia(codproveedores, null, "Y", fechaInicio, fechaFin, null, null, null);

            //ViewData["traspasos"] = traspasoDevoluciones;

            return View();
        }

        public IActionResult GetTraspasosYDevolucionesProveedor(int codProveedor, string codAlmacenOrigen, string codAlmacenDestino, DateTime fechaInicio, DateTime fechaFin, int? codArticulo, string estado)
        {

            if (codAlmacenDestino == "YY") 
            {
                codAlmacenOrigen = "Y";
                codproveedores = null;
            }

            if (codProveedor != 0)
            {
                codproveedores = new List<int>() { codProveedor };
            }


            var traspasoDevoluciones = _TraspasosModel.GetTraspasosProveedorOrigenDestinoDia(codproveedores,codAlmacenOrigen, codAlmacenDestino,  fechaInicio, fechaFin, estado, codArticulo, null);


            return PartialView("Albaranes/AlbaranesAlmacenProveedorGrid", traspasoDevoluciones);
        }

        public async Task<IActionResult> GetTicketDetalle(string codAlmacenOrigen, string fecha, int? codArticulo, long? idMatch)
        {


            DateTime _fechaInicio = DateTime.MinValue;
            DateTime _fechaFin = DateTime.MinValue;


            if (!string.IsNullOrEmpty(fecha) && !string.IsNullOrEmpty(fecha))
            {
                int añoi = DateTime.Now.Year;
                int mesi = DateTime.Now.Month;
                int diai = DateTime.Now.Day;
                int horai = DateTime.Now.Hour;
                int minutoi = DateTime.Now.Minute;




                int.TryParse(fecha.Substring(0, 2), out diai);
                int.TryParse(fecha.Substring(2, 2), out mesi);
                int.TryParse(fecha.Substring(4, 4), out añoi);
                int.TryParse(fecha.Substring(8, 2), out horai);
                int.TryParse(fecha.Substring(10, 2), out minutoi);




                _fechaFin = new DateTime(añoi, mesi, diai, 23, 59, 59);


            }

            _fechaInicio = _fechaFin.AddMonths(-1);



            _fechaInicio = new DateTime(_fechaInicio.Year, _fechaInicio.Month, _fechaInicio.Day,0,0,0);

            var tickets = _ManagerRepository.GetTiketsDevoluciones(codAlmacenOrigen, _fechaInicio, _fechaFin, codArticulo, idMatch);

            ViewData["idMatch"] = idMatch;

            return PartialView("Albaranes/TicketsDevoluciones", tickets);
        }




        public async Task<IActionResult> GetTraspasosYModificacion(int codProveedor, string codAlmacenOrigen, string codAlmacenDestino, string fechaInicio, string fechaFin, int? codArticulo, bool? IsSeparadoPorTienda)
        {

            DateTime? _fechaInicio = null;
            DateTime? _fechaFin = null;


            if (!string.IsNullOrEmpty(fechaInicio) && !string.IsNullOrEmpty(fechaFin))
            {

                if (!IsSeparadoPorTienda.HasValue)
                {

                    int añoi = DateTime.Now.Year;
                    int mesi = DateTime.Now.Month;
                    int diai = DateTime.Now.Day;
                    int horai = 0;
                    int minutoi = 0;

                    int añof = DateTime.Now.Year;
                    int mesf = DateTime.Now.Month;
                    int diaf = DateTime.Now.Day;
                    int horaf = 23;
                    int minutof = 59;


                    int.TryParse(fechaInicio.Substring(0, 2), out diai);
                    int.TryParse(fechaInicio.Substring(2, 2), out mesi);
                    int.TryParse(fechaInicio.Substring(4, 4), out añoi);
                    if (fechaInicio.Length > 8)
                    {
                        int.TryParse(fechaInicio.Substring(8, 2), out horai);
                        int.TryParse(fechaInicio.Substring(10, 2), out minutoi);
                    }

                    int.TryParse(fechaFin.Substring(0, 2), out diaf);
                    int.TryParse(fechaFin.Substring(2, 2), out mesf);
                    int.TryParse(fechaFin.Substring(4, 4), out añof);

                    if (fechaFin.Length > 8)
                    {
                        int.TryParse(fechaFin.Substring(8, 2), out horaf);
                        int.TryParse(fechaFin.Substring(10, 2), out minutof);
                    }


                    _fechaInicio = new DateTime(añoi, mesi, diai, horai, minutoi, 0);
                    _fechaFin = new DateTime(añof, mesf, diaf, horaf == 0 ? 23 : horaf, minutof == 0 ? 59 : minutof, 59);
                }
                else
                {
                    DateTime FechaInicio = DateTime.MinValue;
                    DateTime FechaFin = DateTime.MinValue;
                    if (DateTime.TryParse(fechaInicio, out FechaInicio) && DateTime.TryParse(fechaFin, out FechaFin))
                    {
                        _fechaInicio = FechaInicio;
                        _fechaFin = FechaFin;
                    }


                }


                if (_fechaInicio.HasValue && _fechaInicio.Value != DateTime.MinValue && _fechaFin.HasValue && _fechaFin.Value != DateTime.MinValue)
                {

                    codArticulo = codArticulo == 0 ? null : codArticulo;


                    ViewData["codProveedor"] = codProveedor;
                    ViewData["codAlmacenOrigen"] = codAlmacenOrigen;
                    ViewData["codAlmacenDestino"] = codAlmacenDestino;
                    
                    ViewData["codArticulo"] = codArticulo;
                    ViewData["fechaInicio"] = _fechaInicio;
                    ViewData["fechaFin"] = _fechaFin;
                    ViewData["IsSeparadoPorTienda"] = IsSeparadoPorTienda;




                    if (codAlmacenDestino == "YY") 
                    {
                        codAlmacenOrigen = "Y";
                        codproveedores = null;
                    }

                    if (codProveedor != 0)
                    {
                        codproveedores = new List<int>() { codProveedor };
                    }



                    var registroLogAlbaran = _TraspasosModel.GetLogsGestionTraspasos(codAlmacenOrigen, codAlmacenDestino, null, _fechaInicio.Value, _fechaFin.Value, null, null, codArticulo, codProveedor, "ALBARAN_DEV");

                    if (registroLogAlbaran.Where(x => x.IsEjecutado.HasValue && x.IsEjecutado.Value).Any())
                    {
                        return PartialView("Traspasos/LogGestionTraspasos", registroLogAlbaran);
                    }
                    else
                    {

                        var traspasoDevoluciones = _TraspasosModel.GetTraspasosProveedorOrigenDestinoDia(codproveedores, codAlmacenOrigen, codAlmacenDestino, _fechaInicio.Value, _fechaFin.Value, "Sin Gestionar", codArticulo, null);

                        _TraspasosModel.InsertLogsGestionTraspasos(codAlmacenOrigen, codAlmacenDestino, null, _fechaInicio.Value, _fechaFin.Value, null, traspasoDevoluciones.SelectMany(s => s.traspasos).GroupBy(x => x.CodArticulo).Count(), usDrim != null ? usDrim.Id : 0, false, codArticulo, codProveedor, "ALBARAN_DEV", IsSeparadoPorTienda);


                        return PartialView("Albaranes/GenerarAlbaranTraspasosY", traspasoDevoluciones);
                    }
                }
                else
                {
                    return Json(new { result = "NOK", mensaje = "Error al recuperar las fechas." });
                }
            }
            else
            {
                return Json(new { result = "NOK", mensaje = "Error al recuperar las fechas." });
            }
        }


        [HttpPost]
        public async Task<IActionResult> GeneraAlbaranesDevolucionTraspasosY(int codProveedor, string codAlmacenOrigen, string codAlmacenDestino, string fechaInicio, string fechaFin, int? codArticulo,bool? IsSeparadoPorTienda)
        {
            string result = string.Empty;

            DateTime? _fechaInicio = null;
            DateTime? _fechaFin = null;


            if (!string.IsNullOrEmpty(fechaInicio) && !string.IsNullOrEmpty(fechaFin))
            {
                if (fechaInicio.IndexOf("T")==-1)
                {
                    int añoi = DateTime.Now.Year;
                    int mesi = DateTime.Now.Month;
                    int diai = DateTime.Now.Day;
                    int horai = 0;
                    int minutoi = 0;

                    int añof = DateTime.Now.Year;
                    int mesf = DateTime.Now.Month;
                    int diaf = DateTime.Now.Day;
                    int horaf = 23;
                    int minutof = 59;


                    int.TryParse(fechaInicio.Substring(0, 2), out diai);
                    int.TryParse(fechaInicio.Substring(2, 2), out mesi);
                    int.TryParse(fechaInicio.Substring(4, 4), out añoi);
                    if (fechaInicio.Length > 8)
                    {
                        int.TryParse(fechaInicio.Substring(8, 2), out horai);
                        int.TryParse(fechaInicio.Substring(10, 2), out minutoi);
                    }

                    int.TryParse(fechaFin.Substring(0, 2), out diaf);
                    int.TryParse(fechaFin.Substring(2, 2), out mesf);
                    int.TryParse(fechaFin.Substring(4, 4), out añof);

                    if (fechaFin.Length > 8)
                    {
                        int.TryParse(fechaFin.Substring(8, 2), out horaf);
                        int.TryParse(fechaFin.Substring(10, 2), out minutof);
                    }


                    _fechaInicio = new DateTime(añoi, mesi, diai, horai, minutoi, 0);
                    _fechaFin = new DateTime(añof, mesf, diaf, horaf == 0 ? 23 : horaf, minutof == 0 ? 59 : minutof, 59);

                }
                else
                {
                    DateTime FechaInicio = DateTime.MinValue;
                    DateTime FechaFin = DateTime.MinValue;
                    if (DateTime.TryParse(fechaInicio, out FechaInicio) && DateTime.TryParse(fechaFin, out FechaFin))
                    {
                        _fechaInicio = FechaInicio;
                        _fechaFin = FechaFin;
                    }
                }


                if ((!_fechaInicio.HasValue ||  (_fechaInicio.HasValue && _fechaInicio.Value == DateTime.MinValue)) || (!_fechaInicio.HasValue || (_fechaInicio.HasValue && _fechaInicio.Value == DateTime.MinValue)))
                    return Json(new { result = "NOK", mensaje = "Error al recuperar las fechas." });


                codArticulo = codArticulo == 0 ? null : codArticulo;




                List<int> codproveedores = new List<int>() { 189, 369, 246 };

                if (codAlmacenDestino == "YY") 
                {
                    codAlmacenOrigen = "Y";
                    codproveedores = null;
                }

                if (codProveedor != 0)
                {
                    codproveedores = new List<int>() { codProveedor };
                }



                var registroLogAlbaran = _TraspasosModel.GetLogsGestionTraspasos(codAlmacenOrigen, codAlmacenDestino, null, _fechaInicio.Value, _fechaFin.Value, null, null, codArticulo, codProveedor, "ALBARAN_DEV");

                if (registroLogAlbaran.Where(x=> x.IsEjecutado.HasValue && x.IsEjecutado.Value).Any())
                {
                    return PartialView("Traspasos/LogGestionTraspasos", registroLogAlbaran);
                }

                else
                {


                    var traspasoDevoluciones = _TraspasosModel.GetTraspasosProveedorOrigenDestinoDia(codproveedores, codAlmacenOrigen, codAlmacenDestino, _fechaInicio.Value, _fechaFin.Value, "Sin Gestionar", codArticulo, null);

                    result = _AlbaranesModel.GeneraAlbaranesDevolucionTraspasos(traspasoDevoluciones, IsSeparadoPorTienda);


                    _TraspasosModel.InsertLogsGestionTraspasos(codAlmacenOrigen, codAlmacenDestino, null, _fechaInicio.Value, _fechaFin.Value, null, 
                        traspasoDevoluciones.SelectMany(s => s.traspasos).GroupBy(x => x.CodArticulo).Count(), usDrim != null ? usDrim.Id : 0, true, codArticulo, codProveedor, "ALBARAN_DEV", IsSeparadoPorTienda);
                }
            }


            return Json(new { result = string.IsNullOrEmpty(result) ? "OK" : "NOK", mensaje = result });
        }

        #endregion



    }
}
