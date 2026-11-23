using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drim.Interfaces.MVC.Entities
{

    [Table("REFERENCIASPROV")]
    public class ReferenciasProveedor
    {
        public int CODARTICULO { get; set; }
        public string REFPROVEEDOR { get; set; }
        public int CODPROVEEDOR { get; set; }
        public double? UDSDEFECTO { get; set; }
        public string PRINCIPAL { get; set; }

        [NotMapped]
        public string Referencia { get; set; }

    }
}
