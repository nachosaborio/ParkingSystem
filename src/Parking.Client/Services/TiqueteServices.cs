using Newtonsoft.Json;
using Proyecto1.Models;
using Proyecto2Client.Interfaces;
using System.Text;
using System.Text.Json.Serialization;

namespace Proyecto2Client.Services
{
    public class TiqueteServices : ITiqueteServices { 

        private string _baseurl;

        public TiqueteServices() {
            _baseurl = "http://localhost:5154";
        }

        public async Task<bool> AddTiquete(Tiquete tiquete) { 
            bool answer = false;
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseurl);

            var contenido = new StringContent(JsonConvert.SerializeObject(tiquete),Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"api/Tiquete/AddTiquete", contenido);

            if(response.IsSuccessStatusCode)
            {
                answer = true;
            }
            return answer;
        }

        public async Task<List<Tiquete>> GetAllTiquetes() {
            List<Tiquete> listaTiquetes = new List<Tiquete> ();

            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseurl);

            var response = await client.GetAsync($"api/Tiquete/GetAllTiquetes");

            if(response.IsSuccessStatusCode)
            {
                var jsonRespuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Tiquete>>(jsonRespuesta);
                listaTiquetes = resultado;
            }
            return listaTiquetes;
        }

        public async Task<Tiquete> GetTiqueteXId(int Id)
        {
            Tiquete tiquete = new Tiquete();

            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseurl);

            var response = await client.GetAsync($"api/Tiquete/GetTiqueteXId/{Id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonRespuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Tiquete>(jsonRespuesta);
                tiquete = resultado;
            }
            return tiquete;
        }

        public async Task<bool> UpdateTiquete(Tiquete tiquete) {
            bool answer = false;
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseurl);

            var contenido = new StringContent(JsonConvert.SerializeObject(tiquete), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"api/Tiquete/UpdateTiquete", contenido);

            if (response.IsSuccessStatusCode)
            {
                answer = true;
            }
            return answer;
        }

        public async Task<bool> DeleteTiquete(int Id) {
            bool answer = false;
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseurl);

            var response = await client.DeleteAsync($"api/Tiquete/DeleteTiquete/{Id}");

            if (response.IsSuccessStatusCode)
            {
                answer = true;
            }
            return answer;
        }
    }
}
