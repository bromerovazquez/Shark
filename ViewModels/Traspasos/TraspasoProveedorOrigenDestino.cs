namespace Drim.Interfaces.MVC.ViewModels
{
    public class TraspasoProveedorOrigenDestino
    {

        public TraspasoProveedorOrigenDestino() 
        {
            traspasos = new List<Traspaso>();
        }


        public int codProveedor  { get; set; }

        public string NombreProveedor { get; set; } 


        public string codAlmacenOrigen { get; set; }

        public string NombreAlmacenOrigen { get; set; }


        public string codAlmacenDestino { get; set; }

        public string NombreAlmacenDestino { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public int TotalArticulos { get; set; }

        public int TotalUnidades { get; set; }

        public List<Traspaso> traspasos { get; set; }


        public int StockOrigen { get; set; }


        public int StockDestino { get; set; }
    }
}
