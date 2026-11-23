using Drim.Interfaces.MVC.Entities;
using Drim.Interfaces.MVC.ViewModels;

namespace Drim.Interfaces.MVC.Models
{
    public interface ITraspasosModel
    {
        string InsertLogsGestionTraspasos(string codAlmacenOrigen, string codAlmacenDestino, string codAlmacenDestinoNuevo, DateTime FechaInicio, DateTime FechaFin, bool? IsTraspasados, int NoArticulos, int userId,bool IsEjecutado, int? codArticulo, int? codProveedor, string tipo,bool? IsSeparadoPorTienda);
        List<LogGestionTraspasosAlbaranes> GetLogsGestionTraspasos(string codAlmacenOrigen, string codAlmacenDestino, string codAlmacenDestinoNuevo, DateTime FechaInicio, DateTime FechaFin, DateTime? DateCreate, bool? IsTraspasados, int? codArticulo, int? codProveedor, string tipo);
        List<TraspasoOrigenDestino> GetTraspasosOrigenDestino(string codAlmacenOrigen, string codAlmacenDestino, List<int> codProveedores, DateTime FechaInicio, DateTime FechaFin,string estado, int? codArticulo, bool? IsCuadreDevolucionVentaAlb, string estadoAlbaran);

        List<TraspasoOrigenDestino> GetTraspasosOrigenDestinoDia(string codAlmacenOrigen, string codAlmacenDestino, DateTime FechaInicio, DateTime FechaFin, string estado, int? codArticulo, bool? IsCuadreDevolucionVentaAlb);

        List<TraspasoIncidencias> GetTraspasosIncidencias(string codAlmacenOrigen, string codAlmacenDestino, DateTime? FechaInicio, DateTime? FechaFin, string tipo, int? codArticulo);

        List<TraspasoProveedorOrigenDestino> GetTraspasosProveedorOrigenDestinoDia(List<int> codProveedores, string codAlmacenOrigen, string codAlmacenDestino, DateTime FechaInicio, DateTime FechaFin, string estadoAlbaran, int? codArticulo, bool? IsCuadreDevolucionVentaAlb);

        TraspasosModificacion GetTraspasosOrigenModificar(int id,string codAlmacenOrigen, string codAlmacenDestino, DateTime? FechaInicio, DateTime? FechaFin, string? identificadorTraspasoCap,int? codArticulo,bool IsTraspasado);


        string AccionTraspasos(string codAlmacenOrigen, string codAlmacenDestino, DateTime FechaInicio, DateTime FechaFin, string origenNuevo, string destinoNuevo, string tipo, int idTraspaso, bool IsTraspasado, string identificadorTraspasoCap, int? codArticulo, int cantidad);

    }
}
