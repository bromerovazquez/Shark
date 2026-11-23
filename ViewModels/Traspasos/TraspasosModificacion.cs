namespace Drim.Interfaces.MVC.ViewModels
{
    public class TraspasosModificacion
    {

        public int IdTraspaso { get; set; }

        public string codAlmacenOrigen { get; set; }
        public string codAlmacenDestino { get; set; }


        public string AlmacenOrigen { get; set; }

        public string AlmacenDestino { get; set; }


        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }


        public int Cantidad { get; set; }

        public int codArticulo { get; set; }

        public string DescripcionArticulo { get; set; }

        public string EAN { get; set; }

        public string IdentificadorTraspasoCap { get; set; }

        public bool IsTraspasado { get; set; }

        public string NombreArticulo { get; set; }
    }
}
