using System.ComponentModel.DataAnnotations.Schema;

namespace Drim.Interfaces.MVC.Entities
{
    [Table("TipoAcceso")]
    public class TipoAccesoDato
    {

        public TipoAccesoDato()
        {
        }

        public int IdTipoAcceso { get; set; }

        public string Descripcion { get; set; }
        
    }
}
