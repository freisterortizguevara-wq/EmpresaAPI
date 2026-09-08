using Microsoft.EntityFrameworkCore;
using EmpresaAPI.Models;

namespace EmpresaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<EmpleadoProyecto> EmpleadoProyectos { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar clave compuesta para EmpleadoProyecto
            modelBuilder.Entity<EmpleadoProyecto>()
                .HasKey(ep => new { ep.IdEmpleado, ep.IdProyecto });
                
            // Configurar relaciones
            modelBuilder.Entity<EmpleadoProyecto>()
                .HasOne(ep => ep.Empleado)
                .WithMany(e => e.EmpleadoProyectos)
                .HasForeignKey(ep => ep.IdEmpleado);
                
            modelBuilder.Entity<EmpleadoProyecto>()
                .HasOne(ep => ep.Proyecto)
                .WithMany(p => p.EmpleadoProyectos)
                .HasForeignKey(ep => ep.IdProyecto);
                
            // Índices para optimización
            modelBuilder.Entity<Empleado>()
                .HasIndex(e => e.Apellido)
                .HasDatabaseName("IX_Empleados_Apellido");
                
            modelBuilder.Entity<Empleado>()
                .HasIndex(e => e.IdDepartamento)
                .HasDatabaseName("IX_Empleados_IdDepartamento");
                
            modelBuilder.Entity<Empleado>()
                .HasIndex(e => e.Email)
                .IsUnique()
                .HasDatabaseName("IX_Empleados_Email");
        }
    }
}