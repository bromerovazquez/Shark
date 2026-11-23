using System.ComponentModel.DataAnnotations.Schema;

namespace Drim.Interfaces.MVC.Entities
{
    [Table("Almacen")]
    public class Almacen
    {
        public string CodAlmacen { get; set; }

        public string NOMBREALMACEN { get; set; }

        public string? POPERACIONAL { get; set; }
    }
}
