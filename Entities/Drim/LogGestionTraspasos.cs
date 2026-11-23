using System.ComponentModel.DataAnnotations.Schema;

namespace Drim.Interfaces.MVC.Entities
{
    [Table("LOG_Gestion_TraspasosAlbaranes")]
    public class LogGestionTraspasosAlbaranes
    {

        public long Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? DateCreate { get; set; }
        public string? codAlmacenOrigen { get; set; }
        public string? codAlmacenDestino { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? codAlmacenDestinoNuevo { get; set; }
        public bool? IsTraspasado { get; set; }
        public int? NoArticulos { get; set; }
        public bool? IsEjecutado { get; set; }

        public int? codArticulo { get; set; }
        public int? codProveedor { get; set; }
        public string Tipo { get; set; }

        public bool? IsSeparadoPorTienda { get; set; }


    }
}
