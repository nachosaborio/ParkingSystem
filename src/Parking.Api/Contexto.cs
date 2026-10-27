using Microsoft.EntityFrameworkCore;
using Proyecto1.Models;

namespace Proyecto3API
{
    public partial class Contexto : DbContext
    {
        public virtual DbSet<Empleado> Empleados { get; set; }
        public virtual DbSet<Parqueo> Parqueos { get; set; }
        public virtual DbSet<Tiquete> Tiquetes { get; set; }

        public Contexto(DbContextOptions<Contexto> options) : base (options) { }

        //Enlaza las clases con las tablas en la BD
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleado>().ToTable("Empleado");
            modelBuilder.Entity<Tiquete>().ToTable("Tiquete");
            modelBuilder.Entity<Parqueo>().ToTable("Parqueo");
        }
    }
}
