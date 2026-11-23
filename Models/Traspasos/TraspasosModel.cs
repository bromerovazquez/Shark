using Drim.Interfaces.MVC.Entities;
using Drim.Interfaces.MVC.Models.Comun;
using Drim.Interfaces.MVC.Repositorys;
using Drim.Interfaces.MVC.ViewModels;

namespace Drim.Interfaces.MVC.Models
{
    public class TraspasosModel : ITraspasosModel, IAudit
    {

        public readonly IDrimRepository _DrimRepository;

        public readonly IManagerRepository _ManagerRepository;

        public readonly IWebDatosRepository _WebDatosRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public Entities.UsuarioDrim usDriml { get; set; }
        public List<Entities.MenuRol> MenuRolList { get; set; }
        public List<string> rolDrim { get; set; }

        public TraspasosModel(IDrimRepository drimRepository, IManagerRepository managerRepository, IWebDatosRepository webDatosRepository, IHttpContextAccessor httpContextAccessor)
        {
            _DrimRepository = drimRepository;
            _ManagerRepository = managerRepository;
            _WebDatosRepository = webDatosRepository;
            _httpContextAccessor = httpContextAccessor;
            usDriml = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<UsuarioDrim>("usDrim");
            rolDrim = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<List<string>>("rolDrim");
            MenuRolList = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<List<MenuRol>>("menuUserRol");
        }


        string ITraspasosModel.InsertLogsGestionTraspasos(string codAlmacenOrigen, string codAlmacenDestino, string codAlmacenDestinoNuevo, DateTime FechaInicio, DateTime FechaFin, bool? IsTraspasados, int NoArticulos, int userId, bool IsEjecutado, int? codArticulo, int? codProveedor, string tipo, bool? IsSeparadoPorTienda)
        {
            string result = string.Empty;

            result = _DrimRepository.InsertLogsGestionTraspasos(codAlmacenOrigen, codAlmacenDestino, codAlmacenDestinoNuevo, FechaInicio, FechaFin, IsTraspasados, NoArticulos, userId, IsEjecutado, codArticulo, codProveedor, tipo, IsSeparadoPorTienda);

            return result;
        }


        List<LogGestionTraspasosAlbaranes> ITraspasosModel.GetLogsGestionTraspasos(string codAlmacenOrigen, string codAlmacenDestino, string codAlmacenDestinoNuevo, DateTime FechaInicio, DateTime FechaFin, DateTime? DateCreate, bool? IsTraspasados, int? codArticulo, int? codProveedor, string tipo)
        {
            return _DrimRepository.GetLogsGestionTraspasos(codAlmacenOrigen, codAlmacenDestino, codAlmacenDestinoNuevo, FechaInicio, FechaFin, DateCreate, IsTraspasados, codArticulo, codProveedor, tipo);
        }


        public List<TraspasoOrigenDestino> GetTraspasosOrigenDestino(string codAlmacenOrigen, string codAlmacenDestino, List<int> codProveedores, DateTime FechaInicio, DateTime FechaFin, string estado, int? codArticulo, bool? IsCuadreDevolucionVentaAlb, string estadoAlbaran)
        {

            List<TraspasoOrigenDestino> traspasoOrigenDestinos = new List<TraspasoOrigenDestino>();
            var almacenes = _ManagerRepository.GetAlmacenes(false);
            var proveedores = _ManagerRepository.GetProveedores(null, null);

            if (string.IsNullOrEmpty(estado) || estado == "0")
            {

                //Obtenemos los traspasos Pendientes
                var traspasos_Pendientes = _DrimRepository.GetTraspasos(codAlmacenOrigen, codAlmacenDestino, FechaInicio, FechaFin, false, codArticulo);
                foreach (var traspasoOrigenDestinoDia in traspasos_Pendientes.GroupBy(g => new { g.codAlmacenOrigen, g.codAlmacenDestino }))
                {
                    traspasoOrigenDestinos.Add(new TraspasoOrigenDestino()
                    {
                        CodAlmacenOrigen = traspasoOrigenDestinoDia.Key.codAlmacenOrigen,
                        CodAlmacenDestino = traspasoOrigenDestinoDia.Key.codAlmacenDestino,
                        TotalArticulos = traspasoOrigenDestinoDia.GroupBy(g => g.CodArticulo).Count(),
                        FechaInicio = traspasoOrigenDestinoDia.Min(m => m.FechaCreacion),
                        FechaFin = traspasoOrigenDestinoDia.Max(m => m.FechaCreacion),
                        AlmacenOrigen = almacenes.Where(x => x.CodAlmacen == traspasoOrigenDestinoDia.Key.codAlmacenOrigen).Select(s => s.NOMBREALMACEN).FirstOrDefault(),
                        AlmacenDestino = almacenes.Where(x => x.CodAlmacen == traspasoOrigenDestinoDia.Key.codAlmacenDestino).Select(s => s.NOMBREALMACEN).FirstOrDefault(),
                        Traspasos = Mapping.Mapping.TraspasosViewModel(traspasoOrigenDestinoDia.ToList(), almacenes, null, null, null),
                        IsTraspasados = false,

                    });
                }
            }
            if (string.IsNullOrEmpty(estado) || estado == "1")
            {
                var traspasos_Ejecutados = _ManagerRepository.GetTraspasosCapDetalle(codAlmacenOrigen, codAlmacenDestino, FechaInicio, FechaFin, codProveedores, codArticulo, null, estadoAlbaran, IsCuadreDevolucionVentaAlb);

                foreach (var traspasoOrigenDestinoDia in traspasos_Ejecutados.GroupBy(g => new { g.codAlmacenOrigen, g.codAlmacenDestino }))
                {
                    traspasoOrigenDestinos.Add(new TraspasoOrigenDestino()
                    {
                        CodAlmacenOrigen = traspasoOrigenDestinoDia.Key.codAlmacenOrigen,
                        CodAlmacenDestino = traspasoOrigenDestinoDia.Key.codAlmacenDestino,
                        TotalArticulos = traspasoOrigenDestinoDia.GroupBy(g => g.CodArticulo).Count(),
                        FechaInicio = traspasoOrigenDestinoDia.Min(m => m.FechaCreacion),
                        FechaFin = traspasoOrigenDestinoDia.Max(m => m.FechaCreacion),
                        AlmacenOrigen = almacenes.Where(x => x.CodAlmacen == traspasoOrigenDestinoDia.Key.codAlmacenOrigen).Select(s => s.NOMBREALMACEN).FirstOrDefault(),
                        AlmacenDestino = almacenes.Where(x => x.CodAlmacen == traspasoOrigenDestinoDia.Key.codAlmacenDestino).Select(s => s.NOMBREALMACEN).FirstOrDefault(),
                        Traspasos = Mapping.Mapping.TraspasosViewModel(traspasoOrigenDestinoDia.ToList(), almacenes, null, null, null),
                        IsTraspasados = true,
                    });
                }
            }


            traspasoOrigenDestinos.SelectMany(x => x.Traspasos).ToList().ForEach(x =>
                {
                    x.NombreProveedor = proveedores.Where(z => z.CODPROVEEDOR == x.codProveedor).Select(s => s.NOMPROVEEDOR).FirstOrDefault();
                }
            );




            return traspasoOrigenDestinos;
        }



        public List<TraspasoOrigenDestino> GetTraspasosOrigenDestinoDia(string codAlmacenOrigen, string codAlmacenDestino, DateTime FechaInicio, DateTime FechaFin, string estado, int? codArticulo, bool? IsCuadreTicketDevolucion)
        {

            List<TraspasoOrigenDestino> traspasoOrigenDestinos = new List<TraspasoOrigenDestino>();
            var almacenes = _ManagerRepository.GetAlmacenes(false);


            if (string.IsNullOrEmpty(estado) || estado == "0")
            {

                //Obtenemos los traspasos Pendientes
                var traspasos_Pendientes = _DrimRepository.GetTraspasos(codAlmacenOrigen, codAlmacenDestino, FechaInicio, FechaFin, false, codArticulo);

                if (traspasos_Pendientes.Any())
                {

                    List<int> coarticulos = traspasos_Pendientes.Select(s => s.CodArticulo).Distinct().ToList();

                    var articulosReferencias = _ManagerRepository.GetArticulosRef(coarticulos);


                    traspasos_Pendientes = (from tras in traspasos_Pendientes
                                            join refs in articulosReferencias on tras.CodArticulo equals refs.codArticulo into refs2
                                            from referencias in refs2.DefaultIfEmpty()
                                            select new Entities.Traspaso()
                                            {
                                                CodArticulo = tras.CodArticulo,
                                                Referencia = referencias != null ? referencias.RefProveedor : String.Empty,
                                                EAN13 = tras.EAN13,
                                                Cantidad = tras.Cantidad,
                                                codAlmacenDestino = tras.codAlmacenDestino,
                                                codAlmacenOrigen = tras.codAlmacenOrigen,
                                                Descripcion = tras.Descripcion,
                                                FechaCreacion = tras.FechaCreacion,
                                                FechaModificacion = tras.FechaModificacion,
                                                IdLog = tras.IdLog,
                                                IDTraspaso = tras.IDTraspaso,
                                                IsError = tras.IsError,
                                                TipoTraspaso = tras.TipoTraspaso,
                                                Traspasado = tras.Traspasado,
                                                Usuario = tras.Usuario,
                                                Zona = tras.Zona

                                            }).ToList();

                }

                foreach (var traspasoOrigenDestinoDia in traspasos_Pendientes.GroupBy(g => new { g.codAlmacenOrigen, g.codAlmacenDestino, Dia = new DateTime(g.FechaCreacion.Year, g.FechaCreacion.Month, g.FechaCreacion.Day, 0, 0, 0) }))
                {
                    traspasoOrigenDestinos.Add(new TraspasoOrigenDestino()
                    {
                        CodAlmacenOrigen = traspasoOrigenDestinoDia.Key.codAlmacenOrigen,
                        CodAlmacenDestino = traspasoOrigenDestinoDia.Key.codAlmacenDestino,
                        TotalArticulos = traspasoOrigenDestinoDia.GroupBy(g => g.CodArticulo).Count(),
                        FechaInicio = traspasoOrigenDestinoDia.Key.Dia,
                        FechaFin = new DateTime(traspasoOrigenDestinoDia.Key.Dia.Year, traspasoOrigenDestinoDia.Key.Dia.Month, traspasoOrigenDestinoDia.Key.Dia.Day, 23, 59, 59),
                        AlmacenOrigen = almacenes.Where(x => x.CodAlmacen == traspasoOrigenDestinoDia.Key.codAlmacenOrigen).Select(s => s.NOMBREALMACEN).FirstOrDefault(),
                        AlmacenDestino = almacenes.Where(x => x.CodAlmacen == traspasoOrigenDestinoDia.Key.codAlmacenDestino).Select(s => s.NOMBREALMACEN).FirstOrDefault(),
                        Traspasos = Mapping.Mapping.TraspasosViewModel(traspasoOrigenDestinoDia.ToList(), almacenes, null, null, null),
                        IsTraspasados = false
                    });
                }
            }
            if (string.IsNullOrEmpty(estado) || estado == "1")
            {
                var traspasos_Ejecutados = _ManagerRepository.GetTraspasosCapDetalle(codAlmacenOrigen, codAlmacenDestino, FechaInicio, FechaFin, null, codArticulo, null, null, IsCuadreTicketDevolucion);


                foreach (var traspasoOrigenDestinoDia in traspasos_Ejecutados.GroupBy(g => new { g.codAlmacenOrigen, g.codAlmacenDestino, Dia = new DateTime(g.FechaCreacion.Year, g.FechaCreacion.Month, g.FechaCreacion.Day, 0, 0, 0) }))
                {
                    traspasoOrigenDestinos.Add(new TraspasoOrigenDestino()
                    {
                        CodAlmacenOrigen = traspasoOrigenDestinoDia.Key.codAlmacenOrigen,
                        CodAlmacenDestino = traspasoOrigenDestinoDia.Key.codAlmacenDestino,
                        TotalArticulos = traspasoOrigenDestinoDia.GroupBy(g => g.CodArticulo).Count(),
                        FechaInicio = traspasoOrigenDestinoDia.Key.Dia,
                        FechaFin = new DateTime(traspasoOrigenDestinoDia.Key.Dia.Year, traspasoOrigenDestinoDia.Key.Dia.Month, traspasoOrigenDestinoDia.Key.Dia.Day, 23, 59, 59),
                        AlmacenOrigen = almacenes.Where(x => x.CodAlmacen == traspasoOrigenDestinoDia.Key.codAlmacenOrigen).Select(s => s.NOMBREALMACEN).FirstOrDefault(),
                        AlmacenDestino = almacenes.Where(x => x.CodAlmacen == traspasoOrigenDestinoDia.Key.codAlmacenDestino).Select(s => s.NOMBREALMACEN).FirstOrDefault(),
                        Traspasos = Mapping.Mapping.TraspasosViewModel(traspasoOrigenDestinoDia.ToList(), almacenes, null, null, null),
                        IsTraspasados = true
                    });
                }
            }




            return traspasoOrigenDestinos;
        }



        List<TraspasoProveedorOrigenDestino> ITraspasosModel.GetTraspasosProveedorOrigenDestinoDia(List<int> codProveedores, string codAlmacenOrigen, string codAlmacenDestino, DateTime FechaInicio, DateTime FechaFin, string estadoAlbaran, int? codArticulo, bool? IsCuadreDevolucionVentaAlb)
        {
            List<TraspasoProveedorOrigenDestino> traspasoProveedorOrigenDestino = new List<TraspasoProveedorOrigenDestino>();
            var almacenes = _ManagerRepository.GetAlmacenes(false);
            var proveedores = _ManagerRepository.GetProveedores(null, null);

            var traspasos_Ejecutados = _ManagerRepository.GetTraspasosCapDetalle(codAlmacenOrigen, codAlmacenDestino, FechaInicio, FechaFin, codProveedores, codArticulo, null, estadoAlbaran, IsCuadreDevolucionVentaAlb);

            foreach (var traspasoOrigenDestinoDia in traspasos_Ejecutados.GroupBy(g => new { g.codProveedor, g.codAlmacenOrigen, g.codAlmacenDestino }))
            {
                traspasoProveedorOrigenDestino.Add(new TraspasoProveedorOrigenDestino()
                {
                    codProveedor = traspasoOrigenDestinoDia.Key.codProveedor,
                    NombreProveedor = proveedores.Where(x => x.CODPROVEEDOR == traspasoOrigenDestinoDia.Key.codProveedor).Select(x => x.NOMPROVEEDOR).FirstOrDefault(),
                    codAlmacenOrigen = traspasoOrigenDestinoDia.Key.codAlmacenOrigen,
                    codAlmacenDestino = traspasoOrigenDestinoDia.Key.codAlmacenDestino,
                    TotalArticulos = traspasoOrigenDestinoDia.GroupBy(g => g.CodArticulo).Count(),
                    TotalUnidades = traspasoOrigenDestinoDia.Sum(g => g.Cantidad),
                    FechaInicio = traspasoOrigenDestinoDia.Min(m => m.FechaCreacion),
                    FechaFin = traspasoOrigenDestinoDia.Max(m => m.FechaCreacion),
                    
                    NombreAlmacenOrigen = almacenes.Where(x => x.CodAlmacen == traspasoOrigenDestinoDia.Key.codAlmacenOrigen).Select(s => s.NOMBREALMACEN).FirstOrDefault(),

                    NombreAlmacenDestino = almacenes.Where(x => x.CodAlmacen == traspasoOrigenDestinoDia.Key.codAlmacenDestino).Select(s => s.NOMBREALMACEN).FirstOrDefault(),

                    traspasos = Mapping.Mapping.TraspasosViewModel(traspasoOrigenDestinoDia.ToList(), almacenes, null, null, null),
                });
            }

            return traspasoProveedorOrigenDestino;
        }



        TraspasosModificacion ITraspasosModel.GetTraspasosOrigenModificar(int Id, string codAlmacenOrigen, string codAlmacenDestino, DateTime? FechaInicio, DateTime? FechaFin, string? identificadorTraspasoCap, int? codArticulo, bool IsTraspasado)
        {
            TraspasosModificacion _TraspasosModificacion = null;
            var almacenes = _ManagerRepository.GetAlmacenes(false);
            string NombreArticulo = string.Empty;
            if (codArticulo.HasValue && codArticulo.Value > 0)
            {

                var _ArticuloWeb = _WebDatosRepository.GetArticuloByCodArticulo(codArticulo.Value);
                var _ArticuloManager = _ManagerRepository.GetArticuloByCodArticulo(codArticulo.Value);

                NombreArticulo = (_ArticuloManager != null ? "(" + _ArticuloManager.CODARTICULO.ToString() + "/" + _ArticuloManager.REFPROVEEDOR + ")" : "") + (_ArticuloWeb != null ? _ArticuloWeb.NOMBRE : _ArticuloManager != null ? _ArticuloManager.DESCRIPCION : "");
            }


            if (!IsTraspasado)
            {
                if (Id != 0)
                {
                    var _traspaso = _DrimRepository.GetTraspaso(Id);

                    _TraspasosModificacion = new TraspasosModificacion()
                    {
                        codAlmacenOrigen = _traspaso.codAlmacenOrigen,
                        codAlmacenDestino = _traspaso.codAlmacenDestino,
                        AlmacenOrigen = almacenes.Where(x => x.CodAlmacen == _traspaso.codAlmacenOrigen).Select(s => s.NOMBREALMACEN).FirstOrDefault(),
                        AlmacenDestino = almacenes.Where(x => x.CodAlmacen == _traspaso.codAlmacenDestino).Select(s => s.NOMBREALMACEN).FirstOrDefault(),
                        Cantidad = _traspaso.Cantidad,
                        FechaInicio = _traspaso.FechaCreacion,
                        FechaFin = _traspaso.FechaCreacion,
                        codArticulo = _traspaso.CodArticulo,
                        DescripcionArticulo = _traspaso.Descripcion,
                        EAN = _traspaso.EAN13,
                        NombreArticulo = NombreArticulo,
                        IsTraspasado = IsTraspasado
                    };
                }
                else
                {
                    var _traspasos = _DrimRepository.GetTraspasos(codAlmacenOrigen, codAlmacenDestino, FechaInicio.Value, FechaFin.Value, false, null);

                    _TraspasosModificacion = new TraspasosModificacion()
                    {
                        codAlmacenOrigen = _traspasos.FirstOrDefault().codAlmacenOrigen,
                        codAlmacenDestino = _traspasos.FirstOrDefault().codAlmacenDestino,
                        AlmacenOrigen = almacenes.Where(x => x.CodAlmacen == _traspasos.FirstOrDefault().codAlmacenOrigen).Select(s => s.NOMBREALMACEN).FirstOrDefault(),
                        AlmacenDestino = almacenes.Where(x => x.CodAlmacen == _traspasos.FirstOrDefault().codAlmacenDestino).Select(s => s.NOMBREALMACEN).FirstOrDefault(),
                        Cantidad = _traspasos.GroupBy(g => g.CodArticulo).Count(),
                        FechaInicio = _traspasos.Min(m => m.FechaCreacion),
                        FechaFin = _traspasos.Max(m => m.FechaCreacion),
                        codArticulo = 0,
                        IsTraspasado = IsTraspasado
                    };


                }
            }
            else
            {
                var traspasos = _ManagerRepository.GetTraspasosCapDetalle(codAlmacenOrigen, codAlmacenDestino, FechaInicio.Value, FechaFin.Value, null, codArticulo, identificadorTraspasoCap, null, null);

                _TraspasosModificacion = new TraspasosModificacion()
                {
                    codAlmacenOrigen = codAlmacenOrigen,
                    codAlmacenDestino = codAlmacenDestino,
                    AlmacenOrigen = almacenes.Where(x => x.CodAlmacen == codAlmacenOrigen).Select(s => s.NOMBREALMACEN).FirstOrDefault(),
                    AlmacenDestino = almacenes.Where(x => x.CodAlmacen == codAlmacenDestino).Select(s => s.NOMBREALMACEN).FirstOrDefault(),
                    Cantidad = traspasos.GroupBy(g => g.CodArticulo).Count(),
                    FechaInicio = traspasos.Min(m => m.FechaCreacion),
                    FechaFin = traspasos.Max(m => m.FechaCreacion),
                    codArticulo = codArticulo.HasValue ? codArticulo.Value : 0,
                    NombreArticulo = NombreArticulo,
                    DescripcionArticulo = String.Empty,
                    EAN = String.Empty,
                    IsTraspasado = IsTraspasado
                };


            }

            return _TraspasosModificacion;
        }



        string ITraspasosModel.AccionTraspasos(string codAlmacenOrigen, string codAlmacenDestino, DateTime FechaInicio, DateTime FechaFin, string origenNuevo, string destinoNuevo, string tipo, int idTraspaso, bool IsTraspasado, string identificadorTraspasoCap, int? codArticulo, int cantidad)
        {
            string result = string.Empty;

            List<TraspasoOrigenDestino> traspasosSource = new List<TraspasoOrigenDestino>();


            traspasosSource = GetTraspasosOrigenDestino(codAlmacenOrigen, codAlmacenDestino, null, FechaInicio, FechaFin, (IsTraspasado ? "1" : "0"), codArticulo, null, null);

            if (!traspasosSource.Any() || (traspasosSource.Any() && !traspasosSource.Where(x => x.Traspasos != null && x.Traspasos.Any()).Any()))
                return "No se encontraton traspasos a procesar";


            if (traspasosSource.Sum(s => s.TotalArticulos) != cantidad)
                return "Se detectaron nuevos traspasos, vuelve a cargar la consulta.";

            if (FechaInicio.ToString("dd/MM/yyyy") != FechaFin.ToString("dd/MM/yyyy"))
            {

                return "No se puede realizar traspasos de mas de un dia";
            }


            var almacenes = _ManagerRepository.GetAlmacenes(false);


            try
            {
                switch (tipo)
                {
                    case "cambioDestino":
                        _DrimRepository.TraspasoCambioDestino(
                            Mapping.Mapping.TraspasosEntitie(traspasosSource.SelectMany(s => s.Traspasos).ToList(), IsTraspasado ? codAlmacenDestino : codAlmacenOrigen, IsTraspasado ? codAlmacenOrigen : codAlmacenDestino, (usDriml != null ? usDriml.UserName : string.Empty), IsTraspasado),
                            Mapping.Mapping.TraspasosEntitie(traspasosSource.SelectMany(s => s.Traspasos).ToList(), codAlmacenOrigen, destinoNuevo, (usDriml != null ? usDriml.UserName : string.Empty), true),
                            IsTraspasado);
                        break;
                    case "inversa":
                        //Si ya se traspaso se crea Traspaso Invertido para cancelar el traspaso
                        if (IsTraspasado)
                        {
                            result = _DrimRepository.InsertTraspasos(Mapping.Mapping.TraspasosEntitie(traspasosSource.SelectMany(s => s.Traspasos).ToList().Where(x => x.Traspasado).ToList(), codAlmacenDestino, codAlmacenOrigen, (usDriml != null ? usDriml.UserName : string.Empty), true));
                        }
                        else
                        //Si aun no se trapsasa solo lo quita de la tabla de Dim_Traspasos para que no lo genere
                        {
                            result = _DrimRepository.RemoveTraspasos(Mapping.Mapping.TraspasosEntitie(traspasosSource.SelectMany(s => s.Traspasos).ToList().Where(x => !x.Traspasado).ToList(), codAlmacenOrigen, codAlmacenDestino, (usDriml != null ? usDriml.UserName : string.Empty), false));
                        }
                        break;
                }

                if (string.IsNullOrEmpty(result))
                {
                    result = _DrimRepository.InsertLogsGestionTraspasos(codAlmacenOrigen, codAlmacenDestino, destinoNuevo, FechaInicio, FechaFin, IsTraspasado, traspasosSource.Sum(s => s.TotalArticulos), (usDriml != null ? usDriml.Id : 0), true, codArticulo, null, "TRASPASO", null);
                }


            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }



        List<TraspasoIncidencias> ITraspasosModel.GetTraspasosIncidencias(string codAlmacenOrigen, string codAlmacenDestino, DateTime? FechaInicio, DateTime? FechaFin, string tipo, int? codArticulo)
        {
            List<TraspasoIncidencias> response = new List<TraspasoIncidencias>();

            response = _DrimRepository.GetTraspasosIncidencias(codAlmacenOrigen, codAlmacenDestino, FechaInicio, FechaFin, tipo, codArticulo);

            return response;
        }
    }
}
