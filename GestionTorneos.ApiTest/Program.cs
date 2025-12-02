
using GestionTorneosDeportivos.Modelos;
using Newtonsoft.Json;

namespace GestionTorneos.ApiTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            var rutaTorneos = "api/Torneos";
         
            httpClient.BaseAddress = new Uri("https://localhost:7155/");

            //LECTURA DE DATOS
            var response = httpClient.GetAsync(rutaTorneos).Result;
        
            var json = response.Content.ReadAsStringAsync().Result;
           


            //INSERCION DE DATOS
            var nuevoTorneo = new GestionTorneosDeportivos.Modelos.Torneo()
            {
                Id = 0,
                Nombre = "Copa Primavera 2024",
                Tipo="Mixto",
                FechaInicio= new DateTime(),
                FechaFin=new DateTime(),
                Estado="Jugando"
            };
         

            //Invocar al servicio web para insertar nuevo torneo
            var torneoJson = Newtonsoft.Json.JsonConvert.SerializeObject(nuevoTorneo);
            
            var content = new StringContent(torneoJson, System.Text.Encoding.UTF8, "application/json");
            
            response = httpClient.PostAsync(rutaTorneos, content).Result;
          
            json = response.Content.ReadAsStringAsync().Result;
            

            Console.WriteLine(json);
            
            Console.ReadLine();
        }
    }
}
