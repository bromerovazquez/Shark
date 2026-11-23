using Drim.Interfaces.MVC.Connection;
using Drim.Interfaces.MVC.Data;
using Drim.Interfaces.MVC.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Drim.Interfaces.MVC.Repositorys
{
    public class ManagerRepository : IManagerRepository
    {

        public readonly G001Context _G001Context;

        public ManagerRepository(IDbContextFactory<G001Context> g001Context)
        {

            _G001Context = g001Context.CreateDbContext();
        }

        List<Almacen> IManagerRepository.GetAlmacenes(bool IsOnlyOperativo)
        {
            List<Almacen> result = new List<Almacen>();
            try
            {
                result = _G001Context.Almacenes.Where(x => x.POPERACIONAL != "0" || !IsOnlyOperativo).ToList();
            }
            catch (Exception ex) { }
            return result;
        }


        List<SelectListItem> IManagerRepository.GetAlmacenesSelect(bool IsOnlyOperativo)
        {
            return _G001Context.Almacenes.Where(x => x.POPERACIONAL != "0" || !IsOnlyOperativo).OrderBy(x => x.CodAlmacen).Select(s => new SelectListItem()
            { Text = "(" + s.CodAlmacen + ")" + s.NOMBREALMACEN , Value = s.CodAlmacen }).ToList();
        }


        List<TraspasosCapDetalle> IManagerRepository.GetTraspasosCapDetalle(string codAlmacenOrigen, string codAlmacenDestino, DateTime FechaInicio, DateTime FechaFin, List<int> codProveedores, int? codArticulo, string indentificador, string estadoAlbaran, bool? IsMatch)
        {
            List<TraspasosCapDetalle> response = new List<TraspasosCapDetalle>();


            using (DAL dal = new DAL(ConnectionCatalogDB.G001))
            {
                dal.AddParameters("@CodAlmacenOrigen", codAlmacenOrigen, SqlDbType.NVarChar, 3);
                dal.AddParameters("@CodAlmacenDestino", codAlmacenDestino, SqlDbType.NVarChar, 3);
                dal.AddParameters("@codProveedor", (codProveedores != null ? string.Join(",", codProveedores.ToArray()) : null), SqlDbType.NVarChar, 1000);
                dal.AddParameters("@FechaInicio", FechaInicio, SqlDbType.DateTime, 10);
                dal.AddParameters("@FechaFin", FechaFin, SqlDbType.DateTime, 10);
                dal.AddParameters("@codArticulo", codArticulo, SqlDbType.Int, 1);
                dal.AddParameters("@indentificador", indentificador, SqlDbType.NVarChar, 50);
                dal.AddParameters("@estadoAlbaran", estadoAlbaran, SqlDbType.NVarChar, 50);
                dal.AddParameters("@IsMatch", IsMatch, SqlDbType.Bit, 50);


                using (var rdr = dal.DataReader("dbo.GetTraspasosCAPDetalle", CommandType.StoredProcedure))
                {

                    while (rdr.Read())
                    {

                        response.Add(new TraspasosCapDetalle()
                        {
                            codAlmacenOrigen = rdr.IsDBNull(0) ? String.Empty : rdr.GetString(0),
                            codAlmacenDestino = rdr.IsDBNull(1) ? String.Empty : rdr.GetString(1),
                            FechaCreacion = rdr.IsDBNull(2) ? DateTime.Now : rdr.GetDateTime(2),
                            CodArticulo = rdr.IsDBNull(3) ? 0 : rdr.GetInt32(3),
                            EAN13 = rdr.IsDBNull(4) ? String.Empty : rdr.GetString(4),
                            Descripcion = rdr.IsDBNull(5) ? String.Empty : rdr.GetString(5),
                            Cantidad = rdr.IsDBNull(6) ? 0 : rdr.GetInt32(6),
                            RefProveedor = rdr.IsDBNull(7) ? String.Empty : rdr.GetString(7),
                            codProveedor = rdr.IsDBNull(8) ? 0 : rdr.GetInt32(8),
                            Numero = rdr.IsDBNull(9) ? 0 : rdr.GetInt32(9),
                            IdentificadorTraspasoCap = rdr.IsDBNull(10) ? string.Empty : rdr.GetString(10),
                            IdAlbaran = rdr.IsDBNull(11) ? 0 : rdr.GetInt32(11),
                            IdTraspaso = rdr.IsDBNull(12) ? 0 : rdr.GetInt32(12),
                            Usuario = rdr.IsDBNull(13) ? string.Empty : rdr.GetString(13),
                            IsAnulado = rdr.IsDBNull(14) ? null : (bool?)rdr.GetBoolean(14),
                            IdMatchTiektDevolucion = rdr.IsDBNull(15) ? null : (int?)rdr.GetInt64(15),
                            ExistsTicketsHistorico = rdr.IsDBNull(17) ? null : (bool?)rdr.GetBoolean(17),
                            EstadoAlbaran = rdr.IsDBNull(18) ? string.Empty : rdr.GetString(18),
                            StockOrigen = rdr.IsDBNull(19) ? 0 : rdr.GetInt32(19),
                            StockDestino = rdr.IsDBNull(20) ? 0 : rdr.GetInt32(20)
                        });
                    }
                }
            }


            return response;
        }


        List<AlbaranesCapDetalle> IManagerRepository.GetAlbaranesCapDetalle(string codAlmacenOrigen, string codAlmacenDestino, DateTime FechaInicio, DateTime FechaFin, int? codProveedor, int? codArticulo, string indentificador)
        {
            List<AlbaranesCapDetalle> response = new List<AlbaranesCapDetalle>();





            return response;
        }



        List<TiketsDevoluciones> IManagerRepository.GetTiketsDevoluciones(string codAlmacen, DateTime FechaInicio, DateTime FechaFin, int? codArticulo, long? IdMatch)
        {
            List<TiketsDevoluciones> response = new List<TiketsDevoluciones>();


            using (DAL dal = new DAL(ConnectionCatalogDB.G001))
            {

                //dal.AddParameters("@Id", Id, SqlDbType.BigInt, 1);
                dal.AddParameters("@CodAlmacen", codAlmacen, SqlDbType.NVarChar, 3);
                dal.AddParameters("@codArticulo", codArticulo, SqlDbType.Int, 1);
                dal.AddParameters("@FechaInicio", FechaInicio, SqlDbType.DateTime, 10);
                dal.AddParameters("@FechaFin", FechaFin, SqlDbType.DateTime, 10);
                dal.AddParameters("@IdMatch", IdMatch, SqlDbType.BigInt, 1);


                using (var rdr = dal.DataReader("dbo.GetTicketsDevoluciones", CommandType.StoredProcedure))
                {

                    while (rdr.Read())
                    {

                        response.Add(new TiketsDevoluciones()
                        {
                            codAlmacen = rdr.IsDBNull(0) ? String.Empty : rdr.GetString(0),
                            NumeroSerie = rdr.IsDBNull(1) ? String.Empty : rdr.GetString(1),
                            Numalbaran = rdr.IsDBNull(2) ? 0 : rdr.GetInt32(2),
                            Fecha = rdr.IsDBNull(3) ? DateTime.MinValue : rdr.GetDateTime(3),
                            codArticulo = rdr.IsDBNull(4) ? 0 : rdr.GetInt32(4),
                            Unidades = rdr.IsDBNull(5) ? 0 : rdr.GetDouble(5),
                            codVendedor = rdr.IsDBNull(6) ? 0 : rdr.GetInt32(6),
                            IdMatch = rdr.IsDBNull(7) ? 0 : rdr.GetInt64(7),
                            CantidadRestante = rdr.IsDBNull(8) ? null : (Int32?)rdr.GetInt32(8),
                            NombreAlmacen = rdr.IsDBNull(9) ? string.Empty : rdr.GetString(9),
                            NombreVendedor = rdr.IsDBNull(10) ? string.Empty : rdr.GetString(10),
                            FechaTraspaso = rdr.IsDBNull(11) ? null : (DateTime?)rdr.GetDateTime(11),
                        });

                    }
                }
            }


            return response;
        }



        async Task<IEnumerable<SelectListItem>> IManagerRepository.GetArticulosSelectByKeyWord(string keyword, List<int> codArticulos)
        {

            return await _G001Context.Articulos.Where(x => codArticulos.Contains(x.CODARTICULO) || x.CODARTICULO.ToString().Contains(keyword) || (!string.IsNullOrEmpty(x.REFPROVEEDOR) && x.REFPROVEEDOR.Contains(keyword)) || (!string.IsNullOrEmpty(x.DESCRIPCION) && x.DESCRIPCION.Contains(keyword))).Select(s => new SelectListItem()
            {
                Value = s.CODARTICULO.ToString(),
                Text = "(" + s.CODARTICULO.ToString() + "/" + s.REFPROVEEDOR + ")" + s.DESCRIPCION

            }).ToListAsync();


        }


        async Task<IEnumerable<SelectListItem>> IManagerRepository.GetProveedoresSelectByKeyWord(string keyword)
        {

            return await _G001Context.Proveedores.Where(x => x.CODPROVEEDOR.ToString().Contains(keyword) || (!string.IsNullOrEmpty(x.NOMCOMERCIAL) && x.NOMCOMERCIAL.Contains(keyword)) || (!string.IsNullOrEmpty(x.NOMPROVEEDOR) && x.NOMPROVEEDOR.Contains(keyword))).Select(s => new SelectListItem()
            {
                Value = s.CODPROVEEDOR.ToString(),
                Text = s.CODPROVEEDOR.ToString() + "-" + s.NOMPROVEEDOR

            }).ToListAsync();


        }


        List<Proveedor> IManagerRepository.GetProveedores(int? codProveedor, string nombreproveedor)
        {

            return _G001Context.Proveedores.Where(x =>
           (x.CODPROVEEDOR == codProveedor || !codProveedor.HasValue) &&
           ((!string.IsNullOrEmpty(x.NOMCOMERCIAL) && x.NOMCOMERCIAL.Contains(nombreproveedor)) || (!string.IsNullOrEmpty(x.NOMPROVEEDOR) && x.NOMPROVEEDOR.Contains(nombreproveedor)) || string.IsNullOrEmpty(nombreproveedor))).ToList();


        }


        Articulo IManagerRepository.GetArticuloByCodArticulo(int codArticulo)
        {
            return _G001Context.Articulos.Where(x => x.CODARTICULO == codArticulo).FirstOrDefault();
        }


        string IManagerRepository.GetEAN(int codArticulo)
        {
            return _G001Context.ArticulosReferenciasProveedores.Where(x => x.CODARTICULO == codArticulo && x.CODPROVEEDOR == 0).Select(s => s.REFPROVEEDOR).FirstOrDefault();
        }



        List<ArticuloRef> IManagerRepository.GetArticulosRef(List<int> codArticulos)
        {
            List<ArticuloRef> response = new List<ArticuloRef>();

            //response =  (from a in  _G001Context.Articulos.Where(x => codArticulos.Contains(x.CODARTICULO))
            //            join eans in _G001Context.ArticulosReferenciasProveedores.Where(x=> x.CODPROVEEDOR == 0) on a.CODARTICULO equals eans.CODARTICULO into eans2
            //            from refs in eans2.DefaultIfEmpty()
            //            select new ArticuloRef()
            //            {
            //                codArticulo = a.CODARTICULO,
            //                EAN = refs!= null? refs.REFPROVEEDOR: string.Empty,
            //                RefProveedor = a.REFPROVEEDOR

            //            }).ToList();

            response = _G001Context.Articulos.Where(x => codArticulos.Contains(x.CODARTICULO)).Select(s => new ArticuloRef()
            {
                codArticulo = s.CODARTICULO,
                RefProveedor = s.REFPROVEEDOR
            }).ToList();



            return response;
        }


        //bool IManagerRepository.ExistsTicketsDevolucionHistocio(string codAlmacen, int codArticulo, DateTime FechaInicio, DateTime FechaFin) 
        //{

        //    bool result = false;






        //}

        List<MapDataExchange> IManagerRepository.GetMapsDataExchage()
        {
            List<MapDataExchange> response = new List<MapDataExchange>();

            using (G001Context g001Context = new G001Context())
            {
                response = g001Context.mapDataExchanges.Where(x => x.VISIBLE == 'T' && x.IDMAP > 0).ToList();


            }

            return response;

        }



        List<MapdataExchangeParametros> IManagerRepository.GetParametrosMapsDataExchage(int idMap)
        {


            List<MapdataExchangeParametros> parametros = new List<MapdataExchangeParametros>();

            using (G001Context g001Context = new G001Context())
            {
                parametros = g001Context.mapdataExchangeParametros.AsNoTracking().Where(x => x.IDMAP == idMap && x.IDFILE > 0 && x.IDFIELD > 0).ToList();
            }






            return parametros;
        }


        List<MapConfigFilters> IManagerRepository.GetParametrosLineaFilterMapsDataExchage(int idMap)
        {
            List<MapConfigFilters> parametros = new List<MapConfigFilters>();
            using (G001Context g001Context = new G001Context())
            {
                parametros = g001Context.MapsConfigsFilters.AsNoTracking().Where(x => x.IDMAP == idMap && x.IDFILE > 0).ToList();
            }

            return parametros;
        }


    }
}
