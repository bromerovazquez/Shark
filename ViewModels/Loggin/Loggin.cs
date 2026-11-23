using Drim.Interfaces.MVC.Entities;

namespace Drim.Interfaces.MVC.ViewModels
{
    public class Login
    {
        public Login()
        {
            Usuario = new UsuarioDrim();
        }

        public UsuarioDrim Usuario { get; set; }


        public string Token { get; set; }

        public string Ip { get; set; }

        public string Error { get; set; }

        public bool IsAutentification { get; set; }

    }
}
