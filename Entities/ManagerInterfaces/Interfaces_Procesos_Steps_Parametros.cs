using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Drim.Interfaces.MVC.Entities
{ 
    [Table("Interfaces_Procesos_Steps_Parametros")]
    public class Interfaces_Procesos_Steps_Parametros
    {
        public long ParametroId { get; set; }
        public long? StepId { get; set; }
        public string? TipoParametros { get; set; }
        public string? NombreParametro { get; set; }
        public string? TypeDataParametro { get; set; }
        public string? ValueParametro { get; set; }

        [NotMapped]
        public string? ValueParametroDescripcion { get; set; }

        public short? Value_IndexParametros { get; set; }

        public short? LineaArchivo { get; set; }

        public bool? IsKey { get; set; }

        public bool? IsParametroNameFile { get; set; }

        public short? IndexParametroNameFile { get; set; }

        public string? FormatDateTime { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Interfaces_Procesos_Steps Step { get; set; }
    }
}
