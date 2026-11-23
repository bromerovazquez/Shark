using Drim.Interfaces.MVC.Data;
using Drim.Interfaces.MVC.Entities;
using Drim.Interfaces.MVC.Models.Comun;
using Drim.Interfaces.MVC.Repositorys;
using Drim.Interfaces.MVC.ViewModels;

namespace Drim.Interfaces.MVC.Models
{
    public class AlbaranesModel : IAlbaranesModel, IAudit
    {


        public readonly IDrimRepository _DrimRepository;

        public readonly IManagerRepository _ManagerRepository;

        public readonly IWebDatosRepository _WebDatosRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public Entities.UsuarioDrim usDriml { get; set; }
        public List<Entities.MenuRol> MenuRolList { get; set; }
        public List<string> rolDrim { get; set; }



        public AlbaranesModel(IDrimRepository drimRepository, IManagerRepository managerRepository, IWebDatosRepository webDatosRepository, IHttpContextAccessor httpContextAccessor)
        {
            _DrimRepository = drimRepository;
            _ManagerRepository = managerRepository;
            _WebDatosRepository = webDatosRepository;
            _httpContextAccessor = httpContextAccessor;
            usDriml = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<UsuarioDrim>("usDrim");
            rolDrim = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<List<string>>("rolDrim");
            MenuRolList = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<List<MenuRol>>("menuUserRol");
        }



        string IAlbaranesModel.GeneraAlbaranesDevolucionTraspasos(List<TraspasoProveedorOrigenDestino> traspasos,bool? IsSeparadoPorTienda)
        {
            string result = string.Empty;
            DateTime fechaProcesado = DateTime.Now;

            try
            {
                using (DRIMDbContext dRIMDbContext = new DRIMDbContext())
                {
                    foreach (var traspasoProveedor in traspasos)
                    {

                        List<Albaran> AlbaranesDrim = new List<Albaran>();



                        DateTime _fechaInicio = traspasoProveedor.FechaInicio;
                        DateTime _fechaFin = traspasoProveedor.FechaFin;

                        var _albaranesDrim = dRIMDbContext.Albaranes.Where(x => x.codProveedor == traspasoProveedor.codProveedor && 
                        x.AlmacenOrigen == traspasoProveedor.codAlmacenOrigen && x.AlmacenDestino == traspasoProveedor.codAlmacenDestino && _fechaInicio >= x.FechaTraspaso && x.FechaTraspaso >= _fechaFin).ToList();


                        foreach (var traspaso in traspasoProveedor.traspasos)
                        {

                            string identificadorTraspaso = traspaso.codAlmacenOrigen + traspaso.codAlmacenDestino + traspaso.FechaCreacion.ToString("ddMMyyyyHHmmss");

                            if (!_albaranesDrim.Where(x => x.IdentificadorTraspasoCap == identificadorTraspaso && x.CodArticulo == traspaso.CodArticulo).Any())
                            {
                                AlbaranesDrim.Add(new Albaran()
                                {
                                    AlmacenOrigen = traspaso.codAlmacenOrigen,
                                    AlmacenDestino = traspaso.codAlmacenDestino,
                                    codProveedor = traspaso.codProveedor,
                                    Cantidad = traspaso.Cantidad,
                                    CodArticulo = traspaso.CodArticulo,
                                    FechaTraspaso = traspaso.FechaCreacion,
                                    FechaCreacion = fechaProcesado,
                                    FechaModificacion = fechaProcesado,
                                    IsProcesado = false,
                                    TipoAlbaran = "DEVOLUCIONES" ,
                                    IdentificadorTraspasoCap = identificadorTraspaso,
                                    IdentificadorAlbaranCap = traspaso.codAlmacenOrigen + traspaso.codProveedor + fechaProcesado.ToString("ddMMyyyyHHmmss"),
                                    Usuario = usDriml != null ? usDriml.UserName : "1",
                                    IsSeparadoPorTienda = IsSeparadoPorTienda
                                });
                            }
                        }


                        dRIMDbContext.Albaranes.AddRange(AlbaranesDrim);
                        dRIMDbContext.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }




            return result;
        }

    }
}
