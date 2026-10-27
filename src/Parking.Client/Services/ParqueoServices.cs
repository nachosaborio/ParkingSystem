using Newtonsoft.Json;
using Proyecto1.Models;
using Proyecto2Client.Interfaces;
using System.Text;
using System.Text.Json.Serialization;

namespace Proyecto2Client.Services
{
    public class ParqueoServices : IParqueoServices { 

        private string _baseurl;

        public ParqueoServices() {
            _baseurl = "http://localhost:5154";
        }

        public async Task<bool> AddParqueo(Parqueo parqueo) { 
            bool answer = false;
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseurl);

            var contenido = new StringContent(JsonConvert.SerializeObject(parqueo),Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"api/Parqueo/AddParqueo", contenido);

            if(response.IsSuccessStatusCode)
            {
                answer = true;
            }
            return answer;
        }

        public async Task<List<Parqueo>> GetAllParqueos() {
            List<Parqueo> listaParqueos = new List<Parqueo> ();

            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseurl);

            var response = await client.GetAsync($"api/Parqueo/GetAllParqueos");

            if(response.IsSuccessStatusCode)
            {
                var jsonRespuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Parqueo>>(jsonRespuesta);
                listaParqueos = resultado;
            }
            return listaParqueos;
        }

        public async Task<Parqueo> GetParqueoXId(int Id)
        {
            Parqueo parqueo = new Parqueo();

            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseurl);

            var response = await client.GetAsync($"api/Parqueo/GetParqueoXId/{Id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonRespuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Parqueo>(jsonRespuesta);
                parqueo = resultado;
            }
            return parqueo;
        }

        public async Task<bool> UpdateParqueo(Parqueo parqueo) {
            bool answer = false;
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseurl);

            var contenido = new StringContent(JsonConvert.SerializeObject(parqueo), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"api/Parqueo/UpdateParqueo", contenido);

            if (response.IsSuccessStatusCode)
            {
                answer = true;
            }
            return answer;
        }

        public async Task<bool> DeleteParqueo(int Id) {
            bool answer = false;
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseurl);

            var response = await client.DeleteAsync($"api/Parqueo/DeleteParqueo/{Id}");

            if (response.IsSuccessStatusCode)
            {
                answer = true;
            }
            return answer;
        }
    }
}
