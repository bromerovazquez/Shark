using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Drim.Interfaces.MVC.Entities
{
    [Table("Interfaces_Procesos_Logs_Estados")]
    public class Interfaces_Procesos_Logs_Estados
    {
        public long Id { get; set; }
        public long? ProcesoId { get; set; }
        public long? StepId { get; set; }
        public string? NombreArchivo {get;set;}
        public string? DataIn { get; set; }
        public string? DataOut   { get; set; }
        public string? Estado { get; set; }
        public string? Error { get; set; }
        public DateTime? DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public Guid? IdProcess { get; set; }
    }
}
