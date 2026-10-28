using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Parking.Api;

namespace Parking.Tests
{
    /// <summary>
    /// Hospeda Parking.Api en memoria y reemplaza la base SQLite en archivo
    /// por una base SQLite en memoria propia de esta instancia.
    /// </summary>
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly SqliteConnection _connection;

        /// <summary>
        /// Crea y abre la conexión SQLite en memoria antes de construir el host.
        /// </summary>
        public CustomWebApplicationFactory()
        {
            // La base en memoria vive mientras la conexión siga abierta.
            // Se abre aquí, antes de que se construya el host y corra EnsureCreated + Seed.
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
        }

        /// <summary>Conexión en memoria usada por el Contexto de la Api.</summary>
        public SqliteConnection Connection => _connection;

        /// <summary>
        /// Reemplaza el registro de <see cref="DbContextOptions{Contexto}"/> para que
        /// <see cref="Contexto"/> use la conexión SQLite en memoria.
        /// </summary>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Quita el registro que apunta a parking.db.
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<Contexto>));
                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                // Instancia externa: el contenedor no la libera; la libera Dispose(bool).
                services.AddSingleton(_connection);

                // Sin BuildServiceProvider: la conexión se resuelve al crear las opciones.
                services.AddDbContext<Contexto>((sp, options) =>
                    options.UseSqlite(sp.GetRequiredService<SqliteConnection>()));
            });
        }

        /// <summary>
        /// Detiene el host y después libera la conexión en memoria.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            // Primero se detiene el host; después se cierra la conexión.
            base.Dispose(disposing);
            if (disposing)
            {
                _connection.Dispose();
            }
        }
    }
}
