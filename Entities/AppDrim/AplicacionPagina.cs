using System.ComponentModel.DataAnnotations.Schema;

namespace Drim.Interfaces.MVC.Entities
{

    [Table("AplicacionPagina")]
    public class AplicacionPagina
    {
        public int IdAplicacion { get; set; }
        public int IdPagina { get; set; }
        public bool? IsActivada { get; set; }
    }
}
