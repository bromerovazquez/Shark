namespace Drim.Interfaces.MVC.Entities
{
    public class TraspasosCapDetalle
    {
        public int CodArticulo { get; set; }
        public int Cantidad { get; set; }
        public string codAlmacenOrigen { get; set; }
        public string codAlmacenDestino { get; set; }
        public string AlmacenOrigen { get; set; }
        public string AlmacenDestino { get; set; }
        public string EAN13 { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }

        public string RefProveedor { get; set; }
        public int codProveedor { get; set; }

        public int Numero { get; set; }

        public int IdAlbaran { get; set; }    

        public string IdentificadorTraspasoCap { get; set; }


        public int IdTraspaso { get; set; }

        public string Usuario { get; set; }


        public int TotalVentaDevolucion { get; set; }


        public long? IdMatchTiektDevolucion { get; set; }

        public int? CantidadRestante { get; set; }


        public bool? IsAnulado { get; set; }


        public bool? ExistsTicketsHistorico { get; set; }

        public string EstadoAlbaran { get; set; }


        public int StockOrigen { get; set; }

        public int StockDestino { get; set; }

        public List<TiketsDevoluciones> TiketsDevoluciones { get; set; }

    }
}
