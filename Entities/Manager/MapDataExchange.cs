using System.ComponentModel.DataAnnotations.Schema;

namespace Drim.Interfaces.MVC.Entities
{
    [Table("MAPPINGSCAB")]
    public class MapDataExchange
    {

        public int IDMAP { get; set; }

        public int? IDTIPO { get; set; }

        public string? NOMBRE { get; set; }
        public char? VISIBLE { get; set; }
        public char? REMESA { get; set; }
        public int? NORMA { get; set; }
        public int? USO { get; set; }

    }
}
