using Microsoft.AspNetCore.Identity;

namespace Drim.Interfaces.MVC.Entities
{
    public class UsuarioDrim: IdentityUser<int>
    {
     
        public string? CodAlmacen { get; set; }

        public string? Departaments { get; set; }

        public string? CodEmpleado { get; set; }

        public bool? IsAutentificacionFichaje { get; set; }

    }
}
