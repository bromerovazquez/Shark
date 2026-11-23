using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Drim.Interfaces.MVC.Entities
{
    [Table("Interfaces_Procesos")]
    public class Interfaces_Procesos
    {

        public Interfaces_Procesos() 
        {

            Steps = new List<Interfaces_Procesos_Steps>();
        }


        public long ArchivoId { get; set; }
        public string? NombreProceso { get; set; }
        public string? DescripcionProceso { get; set; }

        public short? Orden { get; set; }

        public DateTime? DateCreate { get; set; }

        public int? UserId { get; set; }

        public bool IsActivo { get; set; }


        public DateTime? LastUpdate { get; set; }

        public string? ExpresionCron { get; set; }


        [JsonIgnore]
        public virtual List<Interfaces_Procesos_Steps> Steps { get; set; }

        

    }
}
