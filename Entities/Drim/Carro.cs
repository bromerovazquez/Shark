using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drim.Interfaces.MVC.Entities
{
    public class Carro
    {
        public int CarroID { get; set; }
        public DateTime DataInici { get; set; }
        public DateTime DataFi { get; set; }
        public string CodiBotiga { get; set; }
        public bool Finalitzat { get; set; }

    }
}
