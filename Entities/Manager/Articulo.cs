using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drim.Interfaces.MVC.Entities
{
    [Table("ARTICULOS")]
    public class Articulo
    {

        public int CODARTICULO { get; set; }

        public string REFPROVEEDOR { get; set; }

        public string DESCRIPCION { get; set; }

    }
}
