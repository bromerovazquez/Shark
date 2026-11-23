using System.ComponentModel.DataAnnotations.Schema;

namespace Drim.Interfaces.MVC.Entities
{

    [Table("DrimGestion_Albaranes")]
    public class Albaran
    {
        public int IDAlbaran { get; set; }
        public int CodArticulo { get; set; }
        public int codProveedor { get; set; }
        public int Cantidad { get; set; }
        public string AlmacenOrigen { get; set; }
        public string AlmacenDestino { get; set; }
        
        public string TipoAlbaran { get; set; }
        public bool IsProcesado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FechaCreacion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FechaModificacion { get; set; }
        public long? IdLog { get; set; }
        [NotMapped]
        public bool? IsError { get; set; }

        public string IdentificadorTraspasoCap { get; set; }
        public string IdentificadorAlbaranCap { get; set; }
        public string Usuario { get; set; }

        public DateTime? FechaTraspaso { get; set; }


        public bool? IsSeparadoPorTienda { get; set; }

    }
}
