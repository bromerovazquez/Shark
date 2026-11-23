using System.ComponentModel.DataAnnotations.Schema;

namespace Drim.Interfaces.MVC.Entities
{
    public class AuditBase
    {

        public AuditBase()
        {
            FechaModificacion = DateTime.Now;
            UserId = 1;
        }

        [Column("UserId")]
        public int UserId { get; set; }
        [Column("FechaModificacion")]
        public DateTime FechaModificacion { get; set; }
        [NotMapped]
        public DateTime FechaCreacion { get; set; }
        [NotMapped]
        public string Ip { get; set; }

    }
}
