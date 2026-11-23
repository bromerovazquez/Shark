using Drim.Interfaces.MVC.Entities;

namespace Drim.Interfaces.MVC.ViewModels
{
    public class Albaranes
    {
        public string codAlmacenOrigen { get; set; }
        public string codAlmacenDestino { get; set; }
        public int codProveedor { get; set; }


        public string AlmacenOrigen { get; set; }

        public string AlmacenDestino { get; set; }


        public string NombreProveedor { get; set; }


        public List<AlbaranesCapDetalle> AlbaranesList { get; set; }

    }
}
