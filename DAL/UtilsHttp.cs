using System.Net;
using System.Text;

namespace Drim.Interfaces.MVC.Connection
{
    public class UtilsHttp
    {

        string Global_VTex_Dominio = "vtexcommercestable.com.br";
        string Global_VTex_AppKey = "vtexappkey-drimjuguetes-LZDBUZ";
        string Global_VTex_AppKeyDev = "rlleberia@drim.es";
        string Global_VTex_Token = "VRILAZYKPTUQVHGTFPEOBKLCSBCOLTHOZOLWCSMKKDYSXFONGVPQOZFKGGTZWKZVLGRGVJQMCTDYMSEMMOHPDRZAIBPWZMOADCHQOHSWFCNUVHXOBGMHCKRTHQXUTTNQ";
        string Global_VTex_TokenDev = "Vtex2017";
        string Global_VTex_AccoundID = "drimjuguetes";
        string Global_Vtex_Entorno = "";
        string Global_VTex_WhareHouseID = "1_1";
        string Global_Especificacion_No_Definida = "No definido";


        public string Global_MD_Acronimo_OpinionesWeb = "OS";
        public string Global_MD_Acronimo_OpinionesArticulo = "OA";
        public string Global_MD_Acronimo_ConfigDrim = "CD";
        public string Global_MD_Acronimo_Clientes = "CL";
        public string Global_MD_Acronimo_ClientesDirecciones = "AD";
        public string Global_MD_Acronimo_Tiendas = "PR";

        private int _TotalResult { get; set; }

        public bool Call_RESTService(string url, string Method, string content, out string Response, ref string Token, string TipoToken, int PageTotal, int Page, out int TotalResult, bool IsEscriveLog = false)
        {
            TotalResult = 100;

            HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
            req.KeepAlive = false;
            req.Method = Method;
            req.Accept = "application/json";
            req.ContentType = "application/json";//; charset=utf-8";


            if (TipoToken == "VTEX-MD-TOKEN" && !string.IsNullOrWhiteSpace(Token))
            {
                if (!string.IsNullOrWhiteSpace(Token))
                {
                    req.Headers.Add("X-VTEX-MD-TOKEN", Token);
                }
            }
            //Peticiones a VTEX IO
            if (TipoToken == "VTEX-IO" && !string.IsNullOrWhiteSpace(Token))
                req.Headers.Add("Authorization", Token);


            //URL Privada, hacen falta credenciales
            if (url.ToUpper().Contains("/PVT/") || url.ToLower().Contains("api.vtex.com") || url.ToLower().Contains("/api/"))// || url.ToLower().Contains(".myvtex.com"))
            {

                req.Headers.Add("X-VTEX-API-AppKey", Global_VTex_AppKey);
                req.Headers.Add("X-VTEX-API-AppToken", Global_VTex_Token);
            }

            req.Headers.Add("REST-Range", string.Format("resources={0}-{1}", Page, PageTotal));

            if (("POST,PUT,PATCH").Split(',').Contains(Method.ToUpper()))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(content);
                req.ContentLength = buffer.Length;
                Stream PostData = req.GetRequestStream();
                PostData.Write(buffer, 0, buffer.Length);
                PostData.Close();
            }
            else if (Method.ToUpper() == "GET")
            {
                if (url.Contains("?"))
                    url += "&aleat=" + DateTime.Now.ToString("ddMMyyyyHHmmssmmmm");
                else
                    url += "?aleat=" + DateTime.Now.ToString("ddMMyyyyHHmmssmmmm");

            }

            HttpWebResponse resp;
            try
            {
                resp = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException ex)
            {
                if (ex.Response == null || ex.Status != WebExceptionStatus.ProtocolError)
                    throw;

                resp = (HttpWebResponse)ex.Response;
            }


            //HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            if (TipoToken == "VTEX-MD-TOKEN")
                Token = resp.Headers["X-VTEX-MD-TOKEN"];

            if (resp.Headers["REST-Content-Range"] != null)
            {
                string _totalResult = resp.Headers["REST-Content-Range"].Substring(resp.Headers["REST-Content-Range"].IndexOf("/") + 1);
                int.TryParse(_totalResult, out TotalResult);
                _TotalResult = TotalResult;
            }


            Encoding enc = System.Text.Encoding.UTF8;
            StreamReader loResponseStream = new StreamReader(resp.GetResponseStream(), enc);

            Response = loResponseStream.ReadToEnd();
            loResponseStream.Close();
            resp.Close();

            return true;
        }

    }
}
