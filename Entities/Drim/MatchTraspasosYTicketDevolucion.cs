using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drim.Interfaces.MVC.Entities
{

    [Table("MatchTraspasosYTicketDevolucion")]
    public class MatchTraspasosYTicketDevolucion
    {
        public long Id { get; set; }
        public string IdentificadorTraspaso { get; set; }
        public string IdentificadorAlbaran { get; set; }
        public string codAlmacenOrigen { get; set; }
        public int? codArticulo { get; set; }
        public int? CantidadTraspaso { get; set; }
        public int? CantidadTicket { get; set; }
        public int? CantidadRestante { get; set; }
        public DateTime? FechaTraspaso { get; set; }
        public DateTime? FechaTicket { get; set; }
        public DateTime? DateCreate { get; set; }

    }
}
