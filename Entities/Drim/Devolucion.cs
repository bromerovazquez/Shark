using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drim.Interfaces.MVC.Entities
{
    public class Devolucion
    {


        public int DevolucioID { get; set; }
        public int CarroID { get; set; }
        public string EAN { get; set; }
        public int CODARTICULO { get; set; }
        public DateTime DataHora { get; set; }
        public int? MovimentID { get; set; }
        public string EANPROVEIDOR { get; set; }
        public bool ExportatFitxerContainer { get; set; }
        public bool ExportatFitxerProveidor { get; set; }
        public DateTime? DataExportatFitxerContainer { get; set; }
        public DateTime? DataExportatFitxerProveidor { get; set; }
        public bool SeparadoPorProveedor { get; set; }
        public string Pale { get; set; }
        public string Contenidor { get; set; }
        public string UsuariPale { get; set; }

        public long? IdLogContainer { get; set; }

        public long? IdLogProveidor { get; set; }

    }
}
