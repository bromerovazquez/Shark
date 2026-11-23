namespace Drim.Interfaces.MVC.Entities
{
    public class UsuarioLogins
    {
        public long Id { get; set; }

        public int UserId { get; set; }

        public DateTime? FechaAcceso { get; set; }

        public string Ip { get; set; }
    }
}
