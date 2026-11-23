using System.ComponentModel.DataAnnotations.Schema;

namespace Drim.Interfaces.MVC.Entities
{
    public class UsuarioEmpleados
    {
        public string codigo_empleado { get; set; }

        public int IdUser{ get; set; }

        [NotMapped]
        public int codVendedor { get; set; }
    }
}
