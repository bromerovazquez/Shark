using Microsoft.AspNetCore.Identity;

namespace Drim.Interfaces.MVC.Entities
{
    public class UsuarioRolesDrim : IdentityUserRole<int>
    {
     
       public int IdAplicacion { get; set; }

    }
}
