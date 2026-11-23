using System.ComponentModel.DataAnnotations.Schema;

namespace Drim.Interfaces.MVC.Entities
{

    [Table("PROVEEDORES")]
    public class Proveedor
    {

      public int CODPROVEEDOR{get;set;}
      public string? CODCONTABLE{get;set;}
      public string? NOMPROVEEDOR{get;set;}
      public string? NOMCOMERCIAL{get;set;}
      public string? CIF{get;set;}
      public string? NIF20{get;set;}
      public string? ALIAS{get;set;}

    }
}
