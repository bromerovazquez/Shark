using System.ComponentModel.DataAnnotations.Schema;

namespace Drim.Interfaces.MVC.Entities
{

    [Table("dtArticulosPorIdioma")]
    public class dtArticulosPorIdioma
    {

      public int CODARTICULO{get;set;}
      public int ID_IDIOMA{get;set;}
      public bool? Activo{get;set;}
      public string? NOMBRE{get;set;}
      public string? DESCRIPCION{get;set;}
      public string? DESCRIPCION_CORTA{get;set;}
      public string? PRESTACIONES{get;set;}
      public string? PALABRAS_SUSTITUTIVAS{get;set;}
      public string? META_TITULO{get;set;}
      public string? META_DESCRIPCION{get;set;}
      public string? META_KEY_1{get;set;}
      public string? META_KEY_2{get;set;}
      public string? ID_VTEX_SKU{get;set;}
      public string? ID_VTEX_PRODUCT{get;set;}
      public DateTime adtFechaModificacion{get;set;}
    }
}
