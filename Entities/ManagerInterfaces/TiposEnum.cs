using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drim.Interfaces.MVC.Entities
{
    public enum TipoAccion 
    {
        Undefined = 0,
        Archivo = 1,
        QuerySql = 2,
        DataExchanged = 3,
        LogEstado = 4,
        Comparador = 5,
        Traspaso = 6,
        Inventarios = 7,
        Albaran_Devoluciones = 8,
        Albaran_TraspasosY = 9
    }



    public class ColumnInfo
    {
        public string ColumnName { get; set; }
        public Type DataType { get; set; }

        public string TipoDato { get; set; }

        public string Variable { get; set; }

        public bool IsKey { get; set; }
    }


    public static class TipoAccionExtender 
    {



        public static TipoAccion GetTipoAccion(this short IdTipoAccion) 
        {

            switch (IdTipoAccion)
            {
                case 1:
                    return TipoAccion.Archivo;
                    break;
                case 2:
                    return TipoAccion.QuerySql;
                    break;
                case 3:
                    return TipoAccion.DataExchanged;
                    break;
                case 4:
                    return TipoAccion.LogEstado;
                case 5:
                    return TipoAccion.Comparador;
                case 6:
                    return TipoAccion.Traspaso;
                case 7:
                    return TipoAccion.Inventarios;
                case 8:
                    return TipoAccion.Albaran_Devoluciones;
                case 9:
                    return TipoAccion.Albaran_TraspasosY;
                    break;
                default:
                    return TipoAccion.Undefined;
                    break;
            }
        }
        public static TipoAccion GetTipoAccion(this string sTipoAccion)
        {

            switch (sTipoAccion)
            {
                case "Archivo":
                    return TipoAccion.Archivo;
                    break;
                case "QuerySql":
                    return TipoAccion.QuerySql;
                    break;
                case "DataExchanged":
                    return TipoAccion.DataExchanged;
                    break;
                case "LogEstado":
                    return TipoAccion.LogEstado;
                case "Comparador":
                    return TipoAccion.Comparador;
                case "Traspaso":
                    return TipoAccion.Traspaso;
                case "Inventarios":
                    return TipoAccion.Inventarios;
                case "Albaran_Devoluciones":
                    return TipoAccion.Albaran_Devoluciones;
                case "Albaran_TraspasosY":
                    return TipoAccion.Albaran_TraspasosY;
                    break;
                default:
                    return TipoAccion.Undefined;
                    break;
            }
        }

    }


}
