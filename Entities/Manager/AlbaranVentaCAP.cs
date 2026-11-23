using System.ComponentModel.DataAnnotations.Schema;

namespace Drim.Interfaces.MVC.Entities
{
    [Table("ALBVENTACAB")]
    public class AlbaranVentaCAP
    {
        public string NUMSERIE { get; set; }
        public int NUMALBARAN { get; set; }
        public char N { get; set; }
        public char? FACTURADO { get; set; }
        public string NUMSERIEFAC { get; set; }
        public int NUMFAC { get; set; }
        public string NFAC { get; set; }
        public char? TIQUET { get; set; }
        public char? ESUNPRESTAMO { get; set; }
        public char? ESDEVOLUCION { get; set; }
        public int? CODCLIENTE { get; set; }
        public int? CODVENDEDOR { get; set; }
        public DateTime? FECHA { get; set; }
        public DateTime? HORA { get; set; }
        public string ENVIOPOR { get; set; }
        public char? PORTESPAG { get; set; }
        public decimal? DTOCOMERCIAL { get; set; }
        public decimal? TOTDTOCOMERCIAL { get; set; }
        public decimal? DTOPP { get; set; }
        public decimal? TOTDTOPP { get; set; }
        public decimal? TOTALBRUTO { get; set; }
        public decimal? TOTALIMPUESTOS { get; set; }
        public decimal? TOTALNETO { get; set; }
        public decimal? TOTALCOSTE { get; set; }
        public char? SELECCIONADO { get; set; }
        public string SUALBARAN { get; set; }
        public int? CODMONEDA { get; set; }
        public decimal? FACTORMONEDA { get; set; }
        public char? IVAINCLUIDO { get; set; }
        public int? CODTARIFA { get; set; }
        public char? VIENEDEFO { get; set; }
        public DateTime? FECHAENTRADA { get; set; }
        public decimal? PORC { get; set; }
        public decimal? TOTPORC { get; set; }
        public int? TIPODOC { get; set; }
        public int? TIPODOCFAC { get; set; }
        public int? SALA { get; set; }
        public int? MESA { get; set; }
        public DateTime? HORAFIN { get; set; }
        public int? NUMCOMENSALES { get; set; }
        public int? IMPRESIONES { get; set; }
        public int? FO { get; set; }
        public string? SERIE { get; set; }
        public int? Z { get; set; }
        public int? IDESTADO { get; set; }
        public DateTime? FECHAMODIFICADO { get; set; }
        public char? AUTOMATICO { get; set; }
        public string? CAJA { get; set; }
        public decimal? TOTALCOSTEIVA { get; set; }
        public char? ESBARRA { get; set; }
        public int? NBULTOS { get; set; }
        public int? TRANSPORTE { get; set; }
        public int? CODENVIO { get; set; }
        public int? PUNTOSACUM { get; set; }
        public int? IDTARJETA { get; set; }
        //public string TOTALCARGOSDTOS { get; set; }
        //public string SERIEASUNTO { get; set; }
        //public string NUMEROASUNTO { get; set; }
        //public string NUMROLLO { get; set; }
        //public string NORECIBIDO { get; set; }
        //public string PUNTOSCANJEADOS { get; set; }
        //public string TOTALPUNTOS { get; set; }
        //public string ENTRANSITO { get; set; }
        //public string TRASPASADO { get; set; }
        //public string ENLACE_EMPRESA { get; set; }
        //public string ENLACE_EJERCICIO { get; set; }
        //public string ENLACE_ASIENTO { get; set; }
        //public string ENLACE_USUARIO { get; set; }
        //public string FECHATRASPASO { get; set; }
        //public string TOTALCOSTE2 { get; set; }
        //public string TOTALCOSTEIVA2 { get; set; }
        //public string FECHARECEPCION { get; set; }
        //public string DESCARGAR { get; set; }
        //public string FECHACREACION { get; set; }
        //public string IDMOTIVODTO { get; set; }
        //public string NUMIMPRESIONES { get; set; }
        //public string HORATOTAL { get; set; }
        //public string HORACOCINA { get; set; }
        //public string FECHAINI { get; set; }
        //public string FECHAFIN { get; set; }
        //public string ESTADODELIVERY { get; set; }
        //public string HORAELABORADO { get; set; }
        //public string HORAASIGNADO { get; set; }
        //public string HORAENTREGADO { get; set; }
        //public string PUNTOSCANJEOPORDTOCOM { get; set; }
        //public string DTOCOMANTESCANJEOPUNTOS { get; set; }
        //public string NUMEROSERIAL { get; set; }
        //public string MMFIJADO { get; set; }
        //public string MODIFIEDTOTALES { get; set; }
        //public string DTOANTESPROMOCIONAENA { get; set; }
        //public string CARGOSERVART { get; set; }
        //public string DTOSERVART { get; set; }
        //public string IMPORTECARGOSERVART { get; set; }
        //public string IMPORTEDTOSERVART { get; set; }
        //public string CARGOANTESSERVART { get; set; }
        //public string IDMOTIVODTOANTESSERVART { get; set; }
        //public string IDHOTEL { get; set; }
        //public string CORREUNICO { get; set; }
        //public string CODCLIENTEPENDIENTE { get; set; }
        //public string REGIMFACT { get; set; }
        //public string ABONOCORREUNICO { get; set; }


    }
}
