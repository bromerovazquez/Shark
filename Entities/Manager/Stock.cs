using System.ComponentModel.DataAnnotations.Schema;

namespace Drim.Interfaces.MVC.Entities
{
    [Table("STOCKS")]
    public class Stock
    {


      public int CODARTICULO { get;set;}
      public string TALLA{get;set;}
      public string COLOR {get;set;}
      public string CODALMACEN {get;set;}
      public double? STOCK{get;set;}
      public double? PEDIDO {get;set;}
      public double? ASERVIR {get;set;}
      public double? PRESTADO {get;set;}
      public double? DEPOSITO {get;set;}
      public double? FABRICACION {get;set;}
      public double? MINIMO {get;set;}
      public DateTime? FECHAMODIFICADO{get;set;}
      public double? MAXIMO {get;set;}
      public string UBICACION{get;set;}
      public DateTime? FECHAREGUL{get;set;}
      public double? STOCKREGUL{get;set;}
      public double? ENREPARACION {get;set;}
      public double? ENTRANSITO {get;set;}
      public double? MERMA {get;set;}
      public double? STOCKCONTABLE {get;set;}
      public double? STOCK2 {get;set;}
      public double? STOCKREGUL2 {get;set;}

    }
}
