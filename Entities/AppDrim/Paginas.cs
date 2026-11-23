namespace Drim.Interfaces.MVC.Entities
{
    public class Pagina
    {

        public Pagina()
        {
        }

        public int IdPagina { get; set; }
        public int? IdPaginaPadre { get; set; }


        public string? NombrePagina { get; set; }
        public string? TituloDisplay { get; set; }
        public int? OrderDisplay { get; set; }
        public string? URL { get; set; }
        public bool? IsMenuPrincipal { get; set; }
        public bool? IsSubMenu { get; set; }
        public bool? IsMenuPestayas { get; set; }

        public string? Icono { get; set; }

        public string? Controller { get; set; }

        public string? Action { get; set; }

    }
}
