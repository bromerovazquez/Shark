using Drim.Interfaces.MVC.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drim.Interfaces.MVC.ViewModels
{
    public class Traspaso
    {

        public int IDTraspaso { get; set; }
        public int CodArticulo { get; set; }
        public int Cantidad { get; set; }
        public string codAlmacenOrigen { get; set; }
        public string codAlmacenDestino { get; set; }

        public string AlmacenOrigen { get; set; }
        public string AlmacenDestino { get; set; }

        public string? EAN13 { get; set; }
        public string? Descripcion { get; set; }
        public string TipoTraspaso { get; set; }
        public string? Zona { get; set; }
        public string? Usuario { get; set; }
        public bool Traspasado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

        public int IdAlabaran { get; set; }

        public string IdentificadorTraspasoCap { get; set; }

        public string RefProveedor { get; set; }

        public long? IdMatchTiektDevolucion { get; set; }

        public int? CantidadRestante { get; set; }

        public int codProveedor { get; set; }

        public string NombreProveedor { get; set; }

        public bool? IsExistsTickets { get; set; }


        public string EstadoAlbaran { get; set; }

        public int StockOrigen { get; set; }

        public int StockDestino { get; set; }

        public List<TiketsDevoluciones> TiketsDevoluciones {get;set;}

    }
}
