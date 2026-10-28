using System.Net;
using Xunit;

namespace Parking.Tests
{
    /// <summary>
    /// Pruebas de integración del controlador de tiquetes.
    /// </summary>
    public class TiqueteControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public TiqueteControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        // Feature: parking-system-ci-tests, Property 3: Endpoints de listado disponibles
        [Fact]
        public async Task GetAllTiquetes_DevuelveOk()
        {
            var response = await _client.GetAsync("/api/Tiquete/GetAllTiquetes");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
