using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drim.Interfaces.MVC.Entities
{
    [Table("DrimGestion_Traspasos")]
    public class Traspaso
    {

        public int IDTraspaso { get; set; }
        public int CodArticulo { get; set; }
        public int Cantidad { get; set; }
        [Column("AlmacenOrigen")]
        public string codAlmacenOrigen { get; set; }
        [Column("AlmacenDestino")]
        public string codAlmacenDestino { get; set; }
        public string? EAN13 { get; set; }
        public string? Descripcion { get; set; }
        public string TipoTraspaso { get; set; }
        public string? Zona { get; set; }
        public string? Usuario { get; set; }
        public bool Traspasado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FechaCreacion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FechaModificacion { get; set; }
        public long? IdLog { get; set; }
        [NotMapped]
        public bool? IsError { get; set; }

        [NotMapped]
        public string? Referencia { get; set; }

        [NotMapped]
        public int StockOrigen { get; set; }

        [NotMapped]
        public int StockDestino { get; set; }
        

    }
}
