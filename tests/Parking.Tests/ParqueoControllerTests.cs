using System.Net;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Parking.Api;
using Xunit;

namespace Parking.Tests
{
    /// <summary>
    /// Pruebas de integración de ParqueoController sobre la base SQLite en memoria.
    /// </summary>
    public class ParqueoControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;

        public ParqueoControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        // Feature: parking-system-ci-tests, Property 3: Endpoints de listado disponibles
        // Feature: parking-system-ci-tests, Property 4: Aislamiento de la base de datos
        [Fact]
        public async Task GetAllParqueos_DevuelveOk()
        {
            // El endpoint de listado responde 200.
            var respuesta = await _client.GetAsync("/api/Parqueo/GetAllParqueos");
            Assert.Equal(HttpStatusCode.OK, respuesta.StatusCode);

            // El Contexto usa la conexión en memoria de la factory, no parking.db.
            using var scope = _factory.Services.CreateScope();
            var contexto = scope.ServiceProvider.GetRequiredService<Contexto>();
            var conexion = contexto.Database.GetDbConnection();

            Assert.Same(_factory.Connection, conexion);
            // La cadena de conexión declara ":memory:". Con la conexión abierta,
            // Microsoft.Data.Sqlite expone en DataSource el archivo real de la base
            // (sqlite3_db_filename), que es vacío para una base en memoria.
            Assert.Equal(":memory:", new SqliteConnectionStringBuilder(conexion.ConnectionString).DataSource);
            Assert.Equal(string.Empty, conexion.DataSource);
            Assert.False(File.Exists(Path.Combine(AppContext.BaseDirectory, "parking.db")));
        }
    }
}
