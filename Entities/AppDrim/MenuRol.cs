namespace Drim.Interfaces.MVC.Entities
{
    public class MenuRol
    {

        public int IdPagina { get; set; }

        public int? IdPaginaPadre { get; set; }

        public int IdRol { get; set; }

        public string? URL { get; set; }

        public string? Pagina { get; set; }

        public string? DisplayText { get; set; }

        public string? Icono { get; set; }

        public bool? IsMenuPrincipal { get; set; }

        public bool? IsSubMenuPrincipal { get; set; }

        public bool? IsMenuPestayas { get; set; }

        public int? OrderDisplay { get; set; }


        public int IdTipoAcceso { get; set; }

        public bool IsSave { get; set; }

        public string? Controller { get; set; }

        public string? Action { get; set; }

    }
}
