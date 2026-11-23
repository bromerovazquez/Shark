using Drim.Interfaces.MVC.Entities;

namespace Drim.Interfaces.MVC.Models.Comun
{
    public interface IAudit
    {

         UsuarioDrim usDriml { get; set; }

         List<MenuRol> MenuRolList { get; set; }

         List<string> rolDrim { get; set; }

    }
}
