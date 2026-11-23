using System.ComponentModel.DataAnnotations.Schema;

namespace Drim.Interfaces.MVC.Entities
{
    [Table("MAPPINGSFILES")]
    public class MapConfigFilters
    {

        public int IDMAP { get; set; }
        public int IDFILE { get; set; }
        public int? POSICION { get; set; }
        public int? TIPOFICHERO { get; set; }
        public int? PRIMERALINEAIMPORTAR { get; set; }
        public string? HAYSEPARADORDECIMAL { get; set; }
        public string? SEPARADORDECIMAL { get; set; }
        public string? FORMATOFECHA { get; set; }
        public string? FILTROLINEAS { get; set; }
        public string? DELIMITADOR { get; set; }
        public string? DELIMITADORFINAL { get; set; }
        public string? SQLEXPORT { get; set; }
        public int? IDFILEPARENT { get; set; }
        public int? TIPOJOIN { get; set; }
        public string? HAYSEPARADORMILES { get; set; }
        public string? SEPARADORMILES { get; set; }
        public string? FORMATOHORA { get; set; }
        public string? DESCRIPCION { get; set; }
        public string? PARAMTAG { get; set; }
        public string? TIENECAMPOS { get; set; }
        public string? SEPARADORLINEAS { get; set; }

    }
}
