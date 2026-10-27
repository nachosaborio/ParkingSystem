namespace Parking.Api.Data
{
    /// <summary>
    /// Inserta datos ficticios de ejemplo en las tablas vacías.
    /// Es idempotente: una tabla con al menos una fila no se modifica.
    /// </summary>
    public static class DatabaseSeeder
    {
        /// <summary>
        /// Siembra <c>Parqueo</c> primero y después <c>Empleado</c> y <c>Tiquete</c>,
        /// referenciando los Ids de parqueo existentes. Solo inserta en tablas vacías.
        /// Si no existe ningún parqueo, no se siembran empleados ni tiquetes.
        /// </summary>
        /// <param name="context">Contexto de base de datos ya creado (tras <c>EnsureCreated()</c>).</param>
        public static void Seed(Contexto context)
        {
            if (!context.Parqueos.Any())
            {
                context.Parqueos.AddRange(SeedData.Parqueos());
                context.SaveChanges();
            }

            // Ids de Parqueo existentes (en el primer arranque, los recién sembrados).
            var parqueoIds = context.Parqueos.OrderBy(p => p.Id).Select(p => p.Id).ToList();
            if (parqueoIds.Count == 0)
            {
                return;
            }

            if (!context.Empleados.Any())
            {
                context.Empleados.AddRange(SeedData.Empleados(parqueoIds));
                context.SaveChanges();
            }

            if (!context.Tiquetes.Any())
            {
                context.Tiquetes.AddRange(SeedData.Tiquetes(parqueoIds));
                context.SaveChanges();
            }
        }
    }
}
