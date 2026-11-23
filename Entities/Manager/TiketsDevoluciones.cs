using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drim.Interfaces.MVC.Entities
{
    public class TiketsDevoluciones
    {

        public string codAlmacen { get; set; }

        public string NombreAlmacen { get; set; }

        public string NumeroSerie { get; set; }

        public int Numalbaran { get; set; }


        public DateTime Fecha { get; set; }

        public int codArticulo { get; set; }

        public double Unidades { get; set; }

        public int codVendedor { get; set; }

        public string NombreVendedor { get; set; }

        public long IdMatch { get; set; }

        public bool IsMatch { get; set; }

        public int? CantidadRestante { get; set; }


        public DateTime? FechaTraspaso { get; set; }



    }
}
