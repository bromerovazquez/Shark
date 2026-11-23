using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drim.Interfaces.MVC.Entities
{
    [Table("DrimGestion_Traspasos_Incidencias")]
    public class TraspasoIncidencias
    {
        public long Id { get; set; }
        public int IDTraspaso { get; set; }
        public int CodArticulo { get; set; }
        public int Cantidad { get; set; }
        public string AlmacenOrigen { get; set; }
        public string AlmacenDestino { get; set; }

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

        public string? TipoInidencia { get; set; }

        public DateTime? FechaLectura { get; set; }

        public int? VersioProducte { get; set; }

    }
}
