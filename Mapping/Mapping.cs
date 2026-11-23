


namespace Drim.Interfaces.MVC.Mapping
{
    public static class Mapping
    {

        #region ViewModel to Entitites

        public static Drim.Interfaces.MVC.Entities.Traspaso TraspasoEntitie(Drim.Interfaces.MVC.ViewModels.Traspaso traspaso, string origenNuevo, string destinoNuevo, string user, bool isNew = false)
        {

            Drim.Interfaces.MVC.Entities.Traspaso result = new Entities.Traspaso()
            {
                Cantidad = traspaso.Cantidad,
                codAlmacenDestino = string.IsNullOrEmpty(destinoNuevo) ? traspaso.codAlmacenDestino : destinoNuevo,
                codAlmacenOrigen = string.IsNullOrEmpty(origenNuevo) ? traspaso.codAlmacenOrigen : origenNuevo,
                CodArticulo = traspaso.CodArticulo,
                Descripcion = traspaso.Descripcion,
                EAN13 = traspaso.EAN13,
                FechaCreacion = isNew? DateTime.Now : traspaso.FechaCreacion,
                FechaModificacion = isNew ? DateTime.Now : traspaso.FechaModificacion,
                IDTraspaso = isNew? 0 : traspaso.IDTraspaso,
                TipoTraspaso = "TRASPASO",
                Traspasado = isNew ? false : traspaso.Traspasado,
                Usuario = string.IsNullOrEmpty(user)?traspaso.Usuario: user,
                Zona = traspaso.Zona,
                StockOrigen = traspaso.StockOrigen,
                StockDestino = traspaso.StockDestino

            };

            return result;
        }


        public static List<Drim.Interfaces.MVC.Entities.Traspaso> TraspasosEntitie(List<Drim.Interfaces.MVC.ViewModels.Traspaso> traspaso, string origenNuevo, string destinoNuevo,string user, bool isNew = false)
        {

            List<Drim.Interfaces.MVC.Entities.Traspaso> result = new List<Entities.Traspaso>();

            foreach (var t in traspaso)
            {
                result.Add(TraspasoEntitie(t, origenNuevo, destinoNuevo, user, isNew));
            }

            return result;
        }


        #endregion



        #region Entities to ViewModel

        public static Drim.Interfaces.MVC.ViewModels.Traspaso TraspasoViewModel(Drim.Interfaces.MVC.Entities.Traspaso traspaso, List<Entities.Almacen> almacenes, string origenNuevo, string destinoNuevo, string user)
        {

            Drim.Interfaces.MVC.ViewModels.Traspaso result = new ViewModels.Traspaso()
            {
                AlmacenOrigen = almacenes.Where(x => x.CodAlmacen == traspaso.codAlmacenOrigen).Select(s => s.NOMBREALMACEN).FirstOrDefault(),
                AlmacenDestino = almacenes.Where(x => x.CodAlmacen == traspaso.codAlmacenDestino).Select(s => s.NOMBREALMACEN).FirstOrDefault(),
                Cantidad = traspaso.Cantidad,
                codAlmacenDestino = string.IsNullOrEmpty(destinoNuevo) ? traspaso.codAlmacenDestino : destinoNuevo,
                codAlmacenOrigen = string.IsNullOrEmpty(origenNuevo) ? traspaso.codAlmacenOrigen : origenNuevo,
                CodArticulo = traspaso.CodArticulo,
                Descripcion = traspaso.Descripcion,
                EAN13 = traspaso.EAN13,
                FechaCreacion = traspaso.FechaCreacion,
                FechaModificacion = traspaso.FechaModificacion,
                IDTraspaso = traspaso.IDTraspaso,
                TipoTraspaso = "TRASPASO",
                Traspasado = traspaso.Traspasado,
                Usuario = traspaso.Usuario,
                Zona = traspaso.Zona,
                RefProveedor = traspaso.Referencia,
                StockOrigen = traspaso.StockOrigen,
                StockDestino = traspaso.StockDestino


            };

            return result;
        }


        public static List<Drim.Interfaces.MVC.ViewModels.Traspaso> TraspasosViewModel(List<Drim.Interfaces.MVC.Entities.Traspaso> traspaso, List<Entities.Almacen> almacenes, string origenNuevo, string destinoNuevo, string user)
        {

            List<Drim.Interfaces.MVC.ViewModels.Traspaso> result = new List<ViewModels.Traspaso>();

            foreach (var t in traspaso)
            {
                result.Add(TraspasoViewModel(t, almacenes, origenNuevo, destinoNuevo, user));
            }

            return result;
        }



        public static Drim.Interfaces.MVC.ViewModels.Traspaso TraspasoViewModel(Drim.Interfaces.MVC.Entities.TraspasosCapDetalle traspaso, List<Entities.Almacen> almacenes, string origenNuevo, string destinoNuevo, string user)
        {

            Drim.Interfaces.MVC.ViewModels.Traspaso result = new ViewModels.Traspaso()
            {
                AlmacenOrigen = almacenes.Where(x => x.CodAlmacen == traspaso.codAlmacenOrigen).Select(s => s.NOMBREALMACEN).FirstOrDefault(),
                AlmacenDestino = almacenes.Where(x => x.CodAlmacen == traspaso.codAlmacenDestino).Select(s => s.NOMBREALMACEN).FirstOrDefault(),
                Cantidad = Convert.ToInt32(traspaso.Cantidad),
                codAlmacenDestino = string.IsNullOrEmpty(destinoNuevo)? traspaso.codAlmacenDestino: destinoNuevo,
                codAlmacenOrigen = string.IsNullOrEmpty(origenNuevo)? traspaso.codAlmacenOrigen: origenNuevo,
                CodArticulo = traspaso.CodArticulo,
                Descripcion = traspaso.Descripcion,
                EAN13 = traspaso.EAN13,
                FechaCreacion = traspaso.FechaCreacion,
                FechaModificacion = traspaso.FechaCreacion,
                IDTraspaso = traspaso.IdTraspaso,
                TipoTraspaso = "TRASPASO",
                Traspasado = true,
                Usuario = string.IsNullOrEmpty(user) ? traspaso.Usuario : user,
                Zona = String.Empty,
                IdAlabaran = traspaso.IdAlbaran,
                IdentificadorTraspasoCap = traspaso.IdentificadorTraspasoCap,
                RefProveedor = traspaso.RefProveedor,
                IdMatchTiektDevolucion = traspaso.IdMatchTiektDevolucion,
                CantidadRestante = traspaso.CantidadRestante,
                codProveedor = traspaso.codProveedor,
                IsExistsTickets = traspaso.ExistsTicketsHistorico,
                EstadoAlbaran = traspaso.EstadoAlbaran,
                StockOrigen = traspaso.StockOrigen,
                StockDestino = traspaso.StockDestino
                 

            };

            return result;
        }


        public static List<Drim.Interfaces.MVC.ViewModels.Traspaso> TraspasosViewModel(List<Drim.Interfaces.MVC.Entities.TraspasosCapDetalle> traspaso, List<Entities.Almacen> almacenes, string origenNuevo, string destinoNuevo, string user)
        {

            List<Drim.Interfaces.MVC.ViewModels.Traspaso> result = new List<ViewModels.Traspaso>();

            foreach (var t in traspaso)
            {
                result.Add(TraspasoViewModel(t, almacenes, origenNuevo, destinoNuevo,  user));
            }

            return result;
        }


        #endregion






    }
}
