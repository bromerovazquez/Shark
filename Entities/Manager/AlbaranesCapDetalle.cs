namespace Drim.Interfaces.MVC.Entities
{
    public class AlbaranesCapDetalle
    {
        public int CodArticulo { get; set; }
        public int Cantidad { get; set; }
        public string codAlmacenOrigen { get; set; }
        public string codAlmacenDestino { get; set; }
        public string AlmacenOrigen { get; set; }
        public string AlmacenDestino { get; set; }
        public string EAN13 { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaAlbaran { get; set; }
        public string RefProveedor { get; set; }
        public int codProveedor { get; set; }
        public string NUMSERIE { get; set; }
        public int NUMALBARAN { get; set; }
        public string N { get; set; }
        public string SUALBARAN { get; set; }
        public bool ESDEVOLUCION { get; set; }
        public int IdEstadoAlbaran { get; set; }
        public int IdAlbaran { get; set; }    
        public string IdentificadorTraspasoCap { get; set; }
        public string IdentificadorAlbaranCap { get; set; }
        public string Usuario { get; set; }
        public string TipoAlbaran { get; set; }
    }
}
