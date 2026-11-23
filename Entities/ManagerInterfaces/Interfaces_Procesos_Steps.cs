using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Drim.Interfaces.MVC.Entities
{

    [Table("Interfaces_Procesos_Steps")]
    public class Interfaces_Procesos_Steps
    {
        public Interfaces_Procesos_Steps()
        {
            Parametros = new List<Interfaces_Procesos_Steps_Parametros>();
        }

        public long StepId { get; set; }
        public long? ArchivoId { get; set; }
        public short? IdTipoAccion { get; set; }
        public string? Descripcion { get; set; }
        public char? Direccion { get; set; }


        public string? NombreArchivo { get; set; }
        public string? Path_Origen_Manager { get; set; }

        public string? Path_Destino_Manager { get; set; }

        public string? Path_Origen_Adaia { get; set; }

        public string? Path_Destino_Adaia { get; set; }
        public bool? IsDeleteArchivoOrigen { get; set; }
        public bool? IsCopiaArchivo { get; set; }
        public string? QuerySQL { get; set; }
        public char? TipoQuerySQL { get; set; }
        public string? DBSQL { get; set; }
        public int? IdMapDataExchanged { get; set; }

        public DateTime? DateCreate { get; set; }
        public int? UserId { get; set; }
        public short? Orden { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string? emailsAviso { get; set; }

        public bool IsParametroNameFile { get; set; }

        public string? SeparadorParametrosNameFile { get; set; }

        public bool? IsParametrosIn { get; set; }
        public bool? IsParametrosOut { get; set; }
        public bool? IsMatrizParametrosIn { get; set; }

        public bool? IsMatrizParametrosOut { get; set; }

        public short NivelIteracion { get; set; }


        public bool IsCABParametros { get; set; }

        

        [JsonIgnore]
        public virtual List<Interfaces_Procesos_Steps_Parametros> Parametros { get; set; }
        [JsonIgnore]
        public virtual Interfaces_Procesos Archivo { get; set; }

        [NotMapped]
        [JsonIgnore]
        public IFormFile _Archivo { get; set; }
    }
}
