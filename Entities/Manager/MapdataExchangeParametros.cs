using System.ComponentModel.DataAnnotations.Schema;

namespace Drim.Interfaces.MVC.Entities
{
    [Table("MAPPINGSFIELDS")]
    public class MapdataExchangeParametros
    {

        public int IDMAP { get; set; }
        public int IDFILE { get; set; }
        public int IDFIELD { get; set; }
        public int? POSICION { get; set; }
        public int? TIPOFIELD { get; set; }
        public string? FIELDNAME { get; set; }
        public int? FIELDSIZE { get; set; }
        public int? NUMDIGITOSDECIMALES { get; set; }
        public string? FORMATOFECHA { get; set; }
        public string? TAGXML { get; set; }
        public string? DOCUMENTACION { get; set; }
        public string? ESATRIBUTO { get; set; }
        public string? ISCAMPOCLAVE { get; set; }
        public string? SUMABLE { get; set; }
        public int? ORDENIMPORTACION { get; set; }

    }
}
