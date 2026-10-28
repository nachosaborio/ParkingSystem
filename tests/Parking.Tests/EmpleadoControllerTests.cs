using System.Net;
using System.Net.Http.Json;
using Parking.Shared.Models;
using Xunit;

namespace Parking.Tests
{
    /// <summary>
    /// Pruebas de integración de los endpoints de /api/Empleado sobre la Api hospedada en memoria.
    /// </summary>
    public class EmpleadoControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public EmpleadoControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        // Feature: parking-system-ci-tests, Property 2: Siembra visible a través de la Api
        [Fact]
        public async Task GetAllEmpleados_DevuelveEmpleadosSembrados()
        {
            var response = await _client.GetAsync("/api/Empleado/GetAllEmpleados");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var empleados = await response.Content.ReadFromJsonAsync<List<Empleado>>();
            Assert.NotNull(empleados);
            Assert.NotEmpty(empleados!);
        }

        // Feature: parking-system-ci-tests, Property 2: Siembra visible a través de la Api
        [Fact]
        public async Task GetEmpleadoXId_ConIdUno_DevuelveEmpleado()
        {
            var response = await _client.GetAsync("/api/Empleado/GetEmpleadoXId/1");

            // 204 indicaría que la siembra no ocurrió sobre la base en memoria.
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var empleado = await response.Content.ReadFromJsonAsync<Empleado>();
            Assert.NotNull(empleado);
            Assert.Equal(1, empleado!.Id);
        }

        // Feature: parking-system-ci-tests, Property 1: Escritura y lectura del empleado (round-trip)
        [Fact]
        public async Task AddEmpleado_LuegoSeLeeElRegistroCreado()
        {
            // Valores ficticios; Cedula única para no depender del estado de otras pruebas.
            var nuevo = new Empleado
            {
                Parqueo = 1,
                FechaIngreso = new DateTime(2024, 1, 15),
                Nombre = "Prueba",
                Apellidos = "Integracion Ficticia",
                FechaDeNacimiento = new DateTime(1990, 5, 20),
                Cedula = 900000000L + Random.Shared.Next(1, 99999999),
                Direccion = "Calle Ficticia 123",
                Email = "empleado.prueba@example.com",
                Telefono = 88880000,
                PersonaDeContacto = "Contacto Ficticio"
            };

            var post = await _client.PostAsJsonAsync("/api/Empleado/AddEmpleado", nuevo);
            Assert.Equal(HttpStatusCode.OK, post.StatusCode);

            var empleados = await _client.GetFromJsonAsync<List<Empleado>>("/api/Empleado/GetAllEmpleados");
            Assert.NotNull(empleados);
            Assert.Contains(empleados!, e =>
                e.Nombre == nuevo.Nombre &&
                e.Apellidos == nuevo.Apellidos &&
                e.Cedula == nuevo.Cedula);
        }
    }
}
