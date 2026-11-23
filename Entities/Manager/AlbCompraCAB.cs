using System.ComponentModel.DataAnnotations.Schema;

namespace Drim.Interfaces.MVC.Entities
{
    [Table("ALBCOMPRACAB")]
    public class AlbCompraCAB
    {

        public string NUMSERIE { get; set; }
        public int NUMALBARAN { get; set; }
        public char N { get; set; }
        public string SUALBARAN { get; set; }
        public char? FACTURADO { get; set; }
        public string NUMSERIEFAC { get; set; }
        public int? NUMFAC { get; set; }
        public char? NFAC { get; set; }
        public char? ESUNDEPOSITO { get; set; }
        public char? ESDEVOLUCION { get; set; }
        public int CODPROVEEDOR { get; set; }
        public DateTime? FECHAALBARAN { get; set; }
        public string ENVIOPOR { get; set; }
        public char? PORTESPAG { get; set; }
        public double? DTOCOMERCIAL { get; set; }
        public double? TOTDTOCOMERCIAL { get; set; }
        public double? DTOPP { get; set; }
        public double? TOTDTOPP { get; set; }
        public double? TOTALBRUTO { get; set; }
        public double? TOTALIMPUESTOS { get; set; }
        public double? TOTALNETO { get; set; }
        public char? SELECCIONADO { get; set; }
        public int CODMONEDA { get; set; }
        public double? FACTORMONEDA { get; set; }
        public char? IVAINCLUIDO { get; set; }
        public DateTime? FECHAENTRADA { get; set; }
        public int? TIPODOC { get; set; }
        public int? TIPODOCFAC { get; set; }
        public int? IDESTADO { get; set; }
        public DateTime? FECHAMODIFICADO { get; set; }
        public DateTime? HORA { get; set; }
        public int? TRANSPORTE { get; set; }
        public int? NBULTOS { get; set; }
        public double? TOTALCARGOSDTOS { get; set; }
        public int? CODCLIENTE { get; set; }
        public char? CHEQUEADO { get; set; }
        public char? NORECIBIDO { get; set; }
        public DateTime? FECHAALBARANVENTA { get; set; }
        public DateTime? FECHACREACION { get; set; }
        public int? NUMIMPRESIONES { get; set; }
        public string MODIFIEDTOTALES { get; set; }
        public bool? IVANODEDUCIBLE { get; set; }
        public string REGIMFACT { get; set; }
    }
}
