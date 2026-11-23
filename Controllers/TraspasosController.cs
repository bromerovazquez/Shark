using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Drim.Interfaces.MVC.Data;
using Drim.Interfaces.MVC.ViewModels;
using Drim.Interfaces.MVC.Repositorys;
using Microsoft.AspNetCore.Authorization;
using Drim.Interfaces.MVC.Models;
using Drim.Interfaces.MVC.Entities;
using Drim.Interfaces.MVC.Helpers;

namespace Drim.Interfaces.MVC.Controllers
{

    public class TraspasosController : Controller
    {
        private readonly IManagerRepository _ManagerRepository;
        private readonly ITraspasosModel _TraspasosModel;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IDrimRepository _DrimRepository;

        private UsuarioDrim usDrim = null;

        private List<MenuRol> MenuRolList = null;

        private List<string> rolDrim = null;

        public TraspasosController(IManagerRepository ManagerRepository, ITraspasosModel TraspasosModel, IHttpContextAccessor accessor, IDrimRepository DrimRepository)
        {
            _httpContextAccessor = accessor;
            usDrim = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<UsuarioDrim>("usDrim");
            rolDrim = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<List<string>>("rolDrim");
            MenuRolList = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<List<MenuRol>>("menuUserRol");
            _ManagerRepository = ManagerRepository;
            _TraspasosModel = TraspasosModel;
            _DrimRepository = DrimRepository;
        }

        // GET: Traspasos
        public async Task<IActionResult> Index()
        {

            ViewData["almacenes"] = _ManagerRepository.GetAlmacenesSelect(false);

            //DateTime fechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            //DateTime fechaFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            //ViewData["traspasos"] = _TraspasosModel.GetTraspasosOrigenDestinoDia(null, null, fechaInicio, fechaFin, null, null, null);


            return View();
        }

        public async Task<IActionResult> Incidencias()
        {

            ViewData["almacenes"] = _ManagerRepository.GetAlmacenesSelect(false);

            DateTime fechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            DateTime fechaFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            var model = _TraspasosModel.GetTraspasosIncidencias(null, null, fechaInicio, fechaFin, null, null);


            return View(model);
        }


        public async Task<IActionResult> GetTraspasosIncidencias(string codAlmacenOrigen, string codAlmacenDestino, DateTime fechaInicio, DateTime fechaFin,string tipo,int? codArticulo)
        {

            ViewData["almacenes"] = _ManagerRepository.GetAlmacenesSelect(false);
            

            var model = _TraspasosModel.GetTraspasosIncidencias(codAlmacenOrigen, codAlmacenDestino, fechaInicio, fechaFin, tipo, codArticulo);


            return PartialView("Traspasos/TraspasoIncidenciasGrid", model);
        }



        public async Task<IActionResult> GetTraspasos(string codAlmacenOrigen, string codAlmacenDestino, DateTime fechaInicio, DateTime fechaFin, string estado, int? codArticulo, bool? IsCuadreTicketDevolucion, string DescuadresYTickets)
        {

            estado = estado == "undefined" ? String.Empty : estado;


            if (fechaInicio.Hour == 0)
            {
                fechaInicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 0, 0, 0);
                fechaFin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, 23, 59, 59);
            }

            bool IsActivaDescuadreY = IsCuadreTicketDevolucion.HasValue && IsCuadreTicketDevolucion.Value;

            if (codAlmacenDestino != "Y" || string.IsNullOrEmpty(DescuadresYTickets))
            {
                IsCuadreTicketDevolucion = null;
            }

            else if (DescuadresYTickets == "1")
            {
                IsCuadreTicketDevolucion = true;
            }
            else
            {
                IsCuadreTicketDevolucion = false;
            }




            var trapasos = _TraspasosModel.GetTraspasosOrigenDestinoDia(codAlmacenOrigen, codAlmacenDestino, fechaInicio, fechaFin, estado, codArticulo, IsCuadreTicketDevolucion);



            if (IsActivaDescuadreY)
            {

                ViewData["IsActivaDescuadreY"] = IsActivaDescuadreY;


                var traspsosY = trapasos.SelectMany(x => x.Traspasos).ToList();

                if (DescuadresYTickets == "3")
                {
                    traspsosY = traspsosY.Where(x => !x.IsExistsTickets.HasValue || (x.IsExistsTickets.HasValue && !x.IsExistsTickets.Value)).ToList();
                }
                else if (DescuadresYTickets == "2")
                {
                    traspsosY = traspsosY.Where(x => !x.IdMatchTiektDevolucion.HasValue && x.IsExistsTickets.HasValue && x.IsExistsTickets.Value).ToList();
                }

                ViewData["DescuadresYTickets"] = DescuadresYTickets;

                return PartialView("Traspasos/TraspasoOrigenDestinoDetalleGrid", traspsosY);
            }
            else
            {

                return PartialView("Traspasos/TraspasoOrigenDestinoGrid", trapasos);
            }
        }


        public async Task<IActionResult> GetTraspasosModificacion(int Id, string codAlmacenOrigen, string codAlmacenDestino, string fechaInicio, string fechaFin, string? identificadorTraspasoCap, int? codArticulo, bool IsTraspasado)
        {

            DateTime? _fechaInicio = null;
            DateTime? _fechaFin = null;


            if (!string.IsNullOrEmpty(fechaInicio) && !string.IsNullOrEmpty(fechaFin))
            {
                int añoi = DateTime.Now.Year;
                int mesi = DateTime.Now.Month;
                int diai = DateTime.Now.Day;
                int horai = DateTime.Now.Hour;
                int minutoi = DateTime.Now.Minute;

                int añof = DateTime.Now.Year;
                int mesf = DateTime.Now.Month;
                int diaf = DateTime.Now.Day;
                int horaf = DateTime.Now.Hour;
                int minutof = DateTime.Now.Minute;


                int.TryParse(fechaInicio.Substring(0, 2), out diai);
                int.TryParse(fechaInicio.Substring(2, 2), out mesi);
                int.TryParse(fechaInicio.Substring(4, 4), out añoi);
                int.TryParse(fechaInicio.Substring(8, 2), out horai);
                int.TryParse(fechaInicio.Substring(10, 2), out minutoi);

                int.TryParse(fechaFin.Substring(0, 2), out diaf);
                int.TryParse(fechaFin.Substring(2, 2), out mesf);
                int.TryParse(fechaFin.Substring(4, 4), out añof);
                int.TryParse(fechaFin.Substring(8, 2), out horaf);
                int.TryParse(fechaFin.Substring(10, 2), out minutof);



                _fechaInicio = new DateTime(añoi, mesi, diai, horai, minutoi, 0);
                _fechaFin = new DateTime(añof, mesf, diaf, horaf, minutof, 59);

            }

            codArticulo = codArticulo == 0 ? null : codArticulo;
            identificadorTraspasoCap = string.IsNullOrEmpty(identificadorTraspasoCap) ? null : identificadorTraspasoCap;

            ViewData["almacenes"] = _ManagerRepository.GetAlmacenesSelect(false);

            var registroLogTraspaso = _TraspasosModel.GetLogsGestionTraspasos(codAlmacenOrigen, codAlmacenDestino, null, _fechaInicio.Value, _fechaFin.Value, null, IsTraspasado, codArticulo, null, "TRASPASO");

            if (registroLogTraspaso.Where(x => x.IsEjecutado.HasValue && x.IsEjecutado.Value).Any())
            {
                return PartialView("Traspasos/LogGestionTraspasos", registroLogTraspaso);
            }
            else
            {
                var trapasosModificacion = _TraspasosModel.GetTraspasosOrigenModificar(Id, codAlmacenOrigen, codAlmacenDestino, _fechaInicio, _fechaFin, identificadorTraspasoCap, codArticulo, IsTraspasado);


                if (trapasosModificacion == null)
                {
                    return NotFound();
                }


                if (!registroLogTraspaso.Any())
                {

                    _TraspasosModel.InsertLogsGestionTraspasos(codAlmacenOrigen, codAlmacenDestino, null, _fechaInicio.Value, _fechaFin.Value, IsTraspasado, trapasosModificacion.Cantidad, (usDrim != null ? usDrim.Id : 0), false, codArticulo, null, "TRASPASO", null);
                }




                return PartialView("Traspasos/ModificarTraspasos", trapasosModificacion);
            }
        }


        [HttpPost]
        public async Task<IActionResult> ModificacionTraspasos(string codAlmacenOrigen, string codAlmacenDestino, string fechaInicio, string fechaFin, string origenNuevo, string destinoNuevo, string tipo, int idTraspaso, bool IsTraspasado, string identificadorTraspasoCap, int? codArticulo, int cantidad)
        {
            string result = string.Empty;

            DateTime? _fechaInicio = null;
            DateTime? _fechaFin = null;


            if (!string.IsNullOrEmpty(fechaInicio) && !string.IsNullOrEmpty(fechaFin))
            {
                int añoi = DateTime.Now.Year;
                int mesi = DateTime.Now.Month;
                int diai = DateTime.Now.Day;
                int horai = DateTime.Now.Hour;
                int minutoi = DateTime.Now.Minute;

                int añof = DateTime.Now.Year;
                int mesf = DateTime.Now.Month;
                int diaf = DateTime.Now.Day;
                int horaf = DateTime.Now.Hour;
                int minutof = DateTime.Now.Minute;


                int.TryParse(fechaInicio.Substring(0, 2), out diai);
                int.TryParse(fechaInicio.Substring(2, 2), out mesi);
                int.TryParse(fechaInicio.Substring(4, 4), out añoi);
                int.TryParse(fechaInicio.Substring(8, 2), out horai);
                int.TryParse(fechaInicio.Substring(10, 2), out minutoi);

                int.TryParse(fechaFin.Substring(0, 2), out diaf);
                int.TryParse(fechaFin.Substring(2, 2), out mesf);
                int.TryParse(fechaFin.Substring(4, 4), out añof);
                int.TryParse(fechaFin.Substring(8, 2), out horaf);
                int.TryParse(fechaFin.Substring(10, 2), out minutof);



                _fechaInicio = new DateTime(añoi, mesi, diai, horai, minutoi, 0);
                _fechaFin = new DateTime(añof, mesf, diaf, horaf, minutof, 59);



                codArticulo = codArticulo == 0 ? null : codArticulo;
                identificadorTraspasoCap = string.IsNullOrEmpty(identificadorTraspasoCap) ? null : identificadorTraspasoCap;

                result = _TraspasosModel.AccionTraspasos(codAlmacenOrigen, codAlmacenDestino, _fechaInicio.Value, _fechaFin.Value, origenNuevo, destinoNuevo, tipo, idTraspaso, IsTraspasado, identificadorTraspasoCap, codArticulo, cantidad);

            }




            return Json(new { result = string.IsNullOrEmpty(result) ? "OK" : "NOK", mensaje = result });
        }




        public async Task<IActionResult> GetAgregarTraspasosValidate(string codAlmacenOrigen, string codAlmacenDestino, string fecha, string tipo, string codArticulo, string cantidad, string zona)
        {
            string sAlmacenOrigen = string.Empty, sAlmacenDestino = string.Empty, refArticulo = string.Empty, NombreArticulo = string.Empty;


            var almacenes = _ManagerRepository.GetAlmacenesSelect(false);
            sAlmacenOrigen = almacenes.Where(x => x.Value == codAlmacenOrigen).Select(x => x.Text).FirstOrDefault();
            sAlmacenDestino = almacenes.Where(x => x.Value == codAlmacenDestino).Select(x => x.Text).FirstOrDefault();

            int _codArticulo = 0;

            int.TryParse(codArticulo, out _codArticulo);
            var articulo = _ManagerRepository.GetArticuloByCodArticulo(_codArticulo);

            refArticulo = articulo != null ? articulo.REFPROVEEDOR : String.Empty;
            NombreArticulo = articulo != null ? articulo.DESCRIPCION : String.Empty;


            ViewData["codAlmacenOrigen"] = codAlmacenOrigen;
            ViewData["codAlmacenDestino"] = codAlmacenDestino;
            ViewData["AlmacenOrigen"] = sAlmacenOrigen;
            ViewData["AlmacenDestino"] = sAlmacenDestino;
            ViewData["codArticulo"] = codArticulo;
            ViewData["NombreArticulo"] = NombreArticulo;
            ViewData["refArticulo"] = refArticulo;
            ViewData["cantidad"] = cantidad;
            ViewData["Fecha"] = fecha;
            ViewData["tipo"] = tipo;
            ViewData["zona"] = zona;




            return PartialView("Traspasos/AddVerificacionTraspasos");
        }


        [HttpPost]
        public async Task<IActionResult> AgregarTraspasosValidate(string codAlmacenOrigen, string codAlmacenDestino, string fecha, string tipo, string codArticulo, string cantidad, string zona)
        {
            string result = string.Empty;
            DateTime _fecha = DateTime.MinValue;
            int _cantidad = 0;
            int _codArticulo = 0;


            if (DateTime.TryParse(fecha, out _fecha) && int.TryParse(codArticulo, out _codArticulo) && int.TryParse(cantidad, out _cantidad))
            {

                var _articulo = _ManagerRepository.GetArticuloByCodArticulo(_codArticulo);
                var ean = _ManagerRepository.GetEAN(_codArticulo);

                result = _DrimRepository.InsertTraspaso(new Entities.Traspaso()
                {
                    Cantidad = _cantidad,
                    FechaCreacion = _fecha,
                    FechaModificacion = _fecha,
                    codAlmacenOrigen = codAlmacenOrigen,
                    codAlmacenDestino = tipo.GetTipoTraspaso() == TipoTraspaso.Tiendas ? codAlmacenDestino : tipo.GetTipoTraspaso().GetAlmacenDestino(),
                    CodArticulo = _codArticulo,
                    TipoTraspaso = tipo.GetTipoTraspasoDb(),
                    Usuario = usDrim.UserName,
                    Zona = zona,
                    Descripcion = _articulo != null ? _articulo.DESCRIPCION : String.Empty,
                    EAN13 = ean
                });

            }

            return Json(new { result = string.IsNullOrEmpty(result) ? "OK" : "NOK", mensaje = result });
        }

    }
}
