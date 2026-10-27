using Newtonsoft.Json;
using Proyecto1.Models;
using Proyecto2Client.Interfaces;
using System.Text;
using System.Text.Json.Serialization;

namespace Proyecto2Client.Services
{
    public class EmpleadoServices : IEmpleadoServices { 

        private string _baseurl;

        public EmpleadoServices() {
            _baseurl = "http://localhost:5154";
        }

        public async Task<bool> AddEmpleado(Empleado empleado) { 
            bool answer = false;
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseurl);

            var contenido = new StringContent(JsonConvert.SerializeObject(empleado),Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"api/Empleado/AddEmpleado", contenido);

            if(response.IsSuccessStatusCode)
            {
                answer = true;
            }
            return answer;
        }

        public async Task<List<Empleado>> GetAllEmpleados() {
            List<Empleado> listaEmpleados = new List<Empleado> ();

            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseurl);

            var response = await client.GetAsync($"api/Empleado/GetAllEmpleados");

            if(response.IsSuccessStatusCode)
            {
                var jsonRespuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Empleado>>(jsonRespuesta);
                listaEmpleados = resultado;
            }
            return listaEmpleados;
        }

        public async Task<Empleado> GetEmpleadoXId(int Id)
        {
            Empleado empleado = new Empleado();

            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseurl);

            var response = await client.GetAsync($"api/Empleado/GetEmpleadoXId/{Id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonRespuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Empleado>(jsonRespuesta);
                empleado = resultado;
            }
            return empleado;
        }

        public async Task<bool> UpdateEmpleado(Empleado empleado) {
            bool answer = false;
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseurl);

            var contenido = new StringContent(JsonConvert.SerializeObject(empleado), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"api/Empleado/UpdateEmpleado", contenido);

            if (response.IsSuccessStatusCode)
            {
                answer = true;
            }
            return answer;
        }

        public async Task<bool> DeleteEmpleado(int Id) {
            bool answer = false;
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseurl);

            var response = await client.DeleteAsync($"api/Empleado/DeleteEmpleado/{Id}");

            if (response.IsSuccessStatusCode)
            {
                answer = true;
            }
            return answer;
        }
    }
}
