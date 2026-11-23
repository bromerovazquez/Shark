namespace Drim.Interfaces.MVC.Entities
{
    public class Interfaces_Logs_Resumen
    {
        public long ProcesoId { get; set; }

        public string NombreProceso { get; set; }

        public bool IsActivo { get; set; }

        public DateTime? UltimaActualziacion { get; set; }

        public string Error { get; set; }

        public string UltimoEstado { get; set; }

        public int NoSucces { get; set; }

        public int NoErrores { get; set; }
    }
}
