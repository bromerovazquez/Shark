namespace Drim.Interfaces.MVC.Helpers
{
    public static class TiposExtensions
    {

        public static string GetTipoTraspasoDb(this string tipo)
        {
            string tipoTraspaso = string.Empty;
            switch (tipo)
            {

                case "Tienda":
                case "DevProv":
                case "Roturas":
                case "DevCarnaval":
                case "DevHalloween":
                case "DevVerano":
                case "DevPueri":
                    return "TRASPASO";

                case "NoConforme":
                    return "NOCONFORME";

                case "InventarioCorrecion":
                    return "INV_C";

                case "InventarioParcial":
                    return "INVENTARIO";



            }

            return tipoTraspaso;
        }


        public static TipoTraspaso GetTipoTraspaso(this string tipo)
        {
            string tipoTraspaso = string.Empty;
            switch (tipo)
            {

                case "Tienda":
                    return TipoTraspaso.Tiendas;
                case "DevProv":
                    return TipoTraspaso.DevolucionProveedor;
                case "Roturas":
                    return TipoTraspaso.Roturas;
                case "DevCarnaval":
                    return TipoTraspaso.DevolucionCarnaval;
                case "DevHalloween":
                    return TipoTraspaso.DevolucionHalloween;
                case "DevVerano":
                    return TipoTraspaso.DevolucionVerano;
                case "DevPueri":
                    return TipoTraspaso.DevolucionPueri;

                case "NoConforme":
                    return TipoTraspaso.DevolucionNoConforme;

                case "InventarioCorrecion":
                    return TipoTraspaso.InventarioCorrecion;
                case "InventarioParcial":
                    return TipoTraspaso.InventarioParcial;
            }

            return TipoTraspaso.Tiendas;
        }


        public static string GetAlmacenDestino(this TipoTraspaso tipo)
        {
            string tipoTraspaso = string.Empty;
            switch (tipo)
            {

                case TipoTraspaso.Tiendas:
                case TipoTraspaso.InventarioCorrecion:
                case TipoTraspaso.InventarioParcial:
                    return "";

                case TipoTraspaso.DevolucionProveedor:
                case TipoTraspaso.Roturas:
                case TipoTraspaso.DevolucionNoConforme:
                    return "Y";

                case TipoTraspaso.DevolucionCarnaval:
                    return "W1";

                case TipoTraspaso.DevolucionHalloween:
                    return "W2";

                case TipoTraspaso.DevolucionVerano:
                    return "W3";

                case TipoTraspaso.DevolucionPueri:
                    return "W4";

            }

            return "";
        }

    }


    public enum TipoTraspaso
    {
        Roturas = 0,
        Tiendas = 1,
        DevolucionProveedor = 2,
        DevolucionCarnaval = 3,
        DevolucionHalloween = 4,
        DevolucionVerano = 5,
        DevolucionPueri = 6,
        DevolucionNoConforme = 7,
        InventarioParcial = 8,
        InventarioCorrecion = 9
    }


}
