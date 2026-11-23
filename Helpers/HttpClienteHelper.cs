using Newtonsoft.Json;
using System.Text;

namespace Drim.Interfaces.MVC.Helpers
{
    public class HttpClienteHelper
    {


        private static async Task UploadSampleFile(dynamic payload, Dictionary<string, string> files)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(EntornoHelper.HttpBaseAddress)
            };


            var stream_CB = System.IO.File.OpenRead(files["CB"]);
            var stream_CDTO = System.IO.File.OpenRead(files["CTO"]);


            var request = new HttpRequestMessage(HttpMethod.Post,"");

            var content = new MultipartFormDataContent()
            {
                // file
                { new StreamContent(stream_CB), "file", "ContratoCB.pdf" },
                { new StreamContent(stream_CDTO), "file", "ContradoCTO.pdf" },

                // payload
                { new StringContent(
                    JsonConvert.SerializeObject(payload),
                    Encoding.UTF8,
                    "application/json"),
                    "Data" },
            };

            request.Content = content;

            
            


            await client.SendAsync(request);
        }


    }
}
