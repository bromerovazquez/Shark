using System.ComponentModel.DataAnnotations.Schema;

namespace Drim.Interfaces.MVC.Entities
{

    [Table("RolPagina")]
    public class RolPagina
    {
        public int IdRol { get; set; }

        public int IdPagina { get; set; }

        public int IdTipoAcceso { get; set; }
    }
}
