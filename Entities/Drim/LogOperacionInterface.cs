using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drim.Interfaces.MVC.Entities
{
    [Table("LOG_OPERACIONES_INTERFACES")]
    public class LogOperacionInterface
    {
        public long Id { get; set; }
        public string? TipoOperacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? NombreArchivo { get; set; }
        public string? CodAlmacenOrigen { get; set; }
        public string? codAlmacenDestino { get; set; }
        public string? Data { get; set; }
        public int? NoArticulos { get; set; }

        public int? CantidadArticulos { get; set; }

        public string? EANPROVEIDOR { get; set; }

        public bool? IsOK { get; set; }

        public string? Pale { get; set; }    

        public string? NumSerie { get; set; }

        public string? Caja { get; set; }

        public int? Numero { get; set; }
    }
}
