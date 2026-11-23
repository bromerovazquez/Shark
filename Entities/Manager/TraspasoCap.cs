using System.ComponentModel.DataAnnotations.Schema;

namespace Drim.Interfaces.MVC.Entities
{
    [Table("TRASPASOSCAB")]
    public class TraspasoCap
    {

        public string SERIE { get; set; }
        public int NUMERO { get; set; }
        public string CAJA { get; set; }
        public DateTime? FECHA { get; set; }
        public string CODALMACENORIGEN { get; set; }
        public string CODALMACENDESTINO { get; set; }
        public string SERIECOMPRA { get; set; }
        public int? NUMFACCOMPRA { get; set; }
        public char? CONTABILIZADO { get; set; }
        public double TOTAL { get; set; }
        public char? ANULADO { get; set; }
        public string SERIEANULADO { get; set; }
        public string CAJAANULADO { get; set; }
        public int?  NUMEROANULADO { get; set; }
        public char? RECIBIDO { get; set; }
        public DateTime? FECHARECIBIDO { get; set; }
        public string IDENTIFICADOR { get; set; }
        public int? RECIBIDOPORCODVENDEDOR { get; set; }
        public char? DESCARGADO { get; set; }
        public string OBSERVACIONES { get; set; }
        public double TOTALDMN { get; set; }
        public char? ESAUTOMATICO { get; set; }
        public char? ESRECUENTO { get; set; }
        public char? ESAJUSTE { get; set; }
        public string CUENTAGASTOSEXISTENCIAS { get; set; }
        public string CUENTAGASTOSEXISTENCIASDMN { get; set; }
        public int? IDCONCEPTOAJUSTE { get; set; }
        public int? IDCONCEPTOAJUSTEDMN { get; set; }
        public char? ESCONTABILIZABLE { get; set; }
        public DateTime? FECHAANULADO { get; set; }
        public string SERIEVENTA { get; set; }
        public int? NUMFACVENTA { get; set; }
        public DateTime? FECHACREACION { get; set; }
        public int? IMPRESIONES { get; set; }
        public DateTime? FECHATRANSPORTE { get; set; }
        public string MODIFICABLE { get; set; }
        public int? IDMOTIVO { get; set; }
        public string DESCMOTIVO { get; set; }
        public string ESTRANSPORTE { get; set; }
        public string VEHICULO { get; set; }

    }
}
