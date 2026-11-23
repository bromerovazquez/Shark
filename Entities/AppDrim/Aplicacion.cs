namespace Drim.Interfaces.MVC.Entities
{
    public class Aplicacion
    {
        public Aplicacion()
        {
            
        }

        public int IdAplicacion { get; set; }
        public string NombreAplicacion { get; set; }
        public string NombreProyecto { get; set; }
        public string Host { get; set; }
        public string URL { get; set; }

    }
}
