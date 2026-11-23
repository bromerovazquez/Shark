using Drim.Interfaces.MVC.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Drim.Interfaces.MVC.Repositorys
{
    public interface IManagerRepository
    {
        List<Almacen> GetAlmacenes(bool IsOnlyOperativo);
        List<SelectListItem> GetAlmacenesSelect(bool IsOnlyOperativo);
        List<TraspasosCapDetalle> GetTraspasosCapDetalle(string codAlmacenOrigen, string codAlmacenDestino, DateTime FechaInicio, DateTime FechaFin,List<int> codProveedores,int? codArticulo,string indentificador,string estadoAlbaran,bool? IsMatch);
        List<AlbaranesCapDetalle> GetAlbaranesCapDetalle(string codAlmacenOrigen, string codAlmacenDestino, DateTime FechaInicio, DateTime FechaFin, int? codProveedor, int? codArticulo, string indentificador);
        List<TiketsDevoluciones> GetTiketsDevoluciones(string codAlmacen, DateTime FechaInicio, DateTime FechaFin, int? codArticulo,  long? IdMatch);
        Task<IEnumerable<SelectListItem>> GetArticulosSelectByKeyWord(string keyword, List<int> codarticulos);
        Task<IEnumerable<SelectListItem>> GetProveedoresSelectByKeyWord(string keyword);
        List<Proveedor> GetProveedores(int? codProveedor,string nombreproveedor);
        Articulo GetArticuloByCodArticulo(int codArticulo);
        string GetEAN(int codArticulo);

        List<ArticuloRef> GetArticulosRef(List<int> codArticulos);
        List<MapDataExchange> GetMapsDataExchage();
        List<MapdataExchangeParametros> GetParametrosMapsDataExchage(int idMap);


        List<MapConfigFilters> GetParametrosLineaFilterMapsDataExchage(int idMap);
    }
}
