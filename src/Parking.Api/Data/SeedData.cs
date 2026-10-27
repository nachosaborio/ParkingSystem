using Parking.Shared.Models;

namespace Parking.Api.Data
{
    /// <summary>
    /// Datos ficticios y deterministas usados por <see cref="DatabaseSeeder"/>.
    /// No contiene datos personales reales: los nombres son inventados, los emails
    /// usan el dominio reservado <c>example.com</c> y todas las fechas son fijas.
    /// Los <c>Id</c> no se asignan porque son columnas de identidad.
    /// </summary>
    internal static class SeedData
    {
        /// <summary>
        /// Devuelve tres parqueos de ejemplo con <c>HoraApertura</c> anterior a <c>HoraCierre</c>.
        /// </summary>
        public static List<Parqueo> Parqueos() => new()
        {
            new Parqueo
            {
                Nombre = "Parqueo Central",
                CapacidadMaxima = 120,
                HoraApertura = new DateTime(2024, 1, 1, 6, 0, 0),
                HoraCierre = new DateTime(2024, 1, 1, 22, 0, 0)
            },
            new Parqueo
            {
                Nombre = "Parqueo Norte",
                CapacidadMaxima = 80,
                HoraApertura = new DateTime(2024, 1, 1, 7, 0, 0),
                HoraCierre = new DateTime(2024, 1, 1, 20, 0, 0)
            },
            new Parqueo
            {
                Nombre = "Parqueo Sur",
                CapacidadMaxima = 60,
                HoraApertura = new DateTime(2024, 1, 1, 8, 0, 0),
                HoraCierre = new DateTime(2024, 1, 1, 18, 30, 0)
            }
        };

        /// <summary>
        /// Devuelve tres empleados de ejemplo asignados de forma cíclica a los Ids de parqueo dados.
        /// </summary>
        /// <param name="parqueoIds">Ids de parqueos existentes; no debe estar vacío.</param>
        public static List<Empleado> Empleados(IReadOnlyList<int> parqueoIds)
        {
            var empleados = new List<Empleado>
            {
                new Empleado
                {
                    FechaIngreso = new DateTime(2020, 3, 15),
                    Nombre = "Ana",
                    Apellidos = "Mora Ejemplo",
                    FechaDeNacimiento = new DateTime(1990, 5, 20),
                    Cedula = 100000001,
                    Direccion = "Calle Ficticia 1, Ciudad Ejemplo",
                    Email = "ana.mora@example.com",
                    Telefono = 80000001,
                    PersonaDeContacto = "Contacto Ejemplo Uno"
                },
                new Empleado
                {
                    FechaIngreso = new DateTime(2021, 7, 1),
                    Nombre = "Luis",
                    Apellidos = "Vargas Prueba",
                    FechaDeNacimiento = new DateTime(1985, 11, 2),
                    Cedula = 100000002,
                    Direccion = "Avenida Imaginaria 2, Ciudad Ejemplo",
                    Email = "luis.vargas@example.com",
                    Telefono = 80000002,
                    PersonaDeContacto = "Contacto Ejemplo Dos"
                },
                new Empleado
                {
                    FechaIngreso = new DateTime(2022, 1, 10),
                    Nombre = "Sofia",
                    Apellidos = "Rojas Demo",
                    FechaDeNacimiento = new DateTime(1995, 8, 30),
                    Cedula = 100000003,
                    Direccion = "Paseo Inventado 3, Ciudad Ejemplo",
                    Email = "sofia.rojas@example.com",
                    Telefono = 80000003,
                    PersonaDeContacto = "Contacto Ejemplo Tres"
                }
            };

            for (var i = 0; i < empleados.Count; i++)
            {
                empleados[i].Parqueo = parqueoIds[i % parqueoIds.Count];
            }

            return empleados;
        }

        /// <summary>
        /// Devuelve tres tiquetes de ejemplo asignados de forma cíclica a los Ids de parqueo dados.
        /// </summary>
        /// <param name="parqueoIds">Ids de parqueos existentes; no debe estar vacío.</param>
        public static List<Tiquete> Tiquetes(IReadOnlyList<int> parqueoIds)
        {
            var tiquetes = new List<Tiquete>
            {
                new Tiquete
                {
                    FechaYHoraEntrada = new DateTime(2024, 1, 15, 8, 0, 0),
                    FechaYHoraSalida = new DateTime(2024, 1, 15, 10, 30, 0),
                    Placa = "ABC-123",
                    TarifaPorHora = 1000.0,
                    TarifaPorMediaHora = 600.0
                },
                new Tiquete
                {
                    FechaYHoraEntrada = new DateTime(2024, 1, 15, 9, 15, 0),
                    FechaYHoraSalida = new DateTime(2024, 1, 15, 12, 0, 0),
                    Placa = "XYZ-789",
                    TarifaPorHora = 1200.0,
                    TarifaPorMediaHora = 700.0
                },
                new Tiquete
                {
                    FechaYHoraEntrada = new DateTime(2024, 1, 16, 14, 0, 0),
                    FechaYHoraSalida = new DateTime(2024, 1, 16, 15, 0, 0),
                    Placa = "DEF-456",
                    TarifaPorHora = 800.0,
                    TarifaPorMediaHora = 450.0
                }
            };

            for (var i = 0; i < tiquetes.Count; i++)
            {
                tiquetes[i].Parqueo = parqueoIds[i % parqueoIds.Count];
            }

            return tiquetes;
        }
    }
}
