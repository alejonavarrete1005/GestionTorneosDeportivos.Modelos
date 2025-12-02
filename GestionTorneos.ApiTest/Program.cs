using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace GestionTorneos.ApiTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            var rutaTorneos = "api/Torneos";
            httpClient.BaseAddress = new Uri("https://localhost:7215/");

            //LECTURA DE DATOS
            var response = httpClient.GetAsync(rutaTorneos).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            
            

        }
    
    }

}
