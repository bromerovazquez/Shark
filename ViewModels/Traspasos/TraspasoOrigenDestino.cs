namespace Drim.Interfaces.MVC.ViewModels
{
    public class TraspasoOrigenDestino
    {

        public TraspasoOrigenDestino() 
        {
            Traspasos = new List<Traspaso>();
        }



        public int codProveedor  { get; set; }

        public string NombreProveedor { get; set; } 

        public string CodAlmacenOrigen { get; set; }


        public string CodAlmacenDestino { get; set; }



        public string AlmacenOrigen { get; set; }


        public string AlmacenDestino { get; set; }



        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }


        public int TotalArticulos { get; set; }

        public int TotalUnidades { get; set; }


        public List<Traspaso> Traspasos { get; set; }

        public bool IsTraspasados { get; set; }

    }
}
