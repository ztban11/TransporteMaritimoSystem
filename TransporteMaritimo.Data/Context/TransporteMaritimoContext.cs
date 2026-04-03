using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using TransporteMaritimo.Core.Models;

namespace TransporteMaritimo.Data.Context
{
    public class TransporteMaritimoContext : DbContext
    {
        public TransporteMaritimoContext(DbContextOptions<TransporteMaritimoContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Rol> Roles { get; set; }

        public DbSet<Permiso> Permisos { get; set; }

        public DbSet<RolPermiso> RolPermisos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.HasKey(p => p.iPermisoId);

                entity.Property(p => p.iPermisoId)
                    .HasColumnName("PermisoId");

                entity.Property(p => p.sNombrePermiso)
                    .HasColumnName("NombrePermiso");

                entity.Property(p => p.sDescripcion)
                    .HasColumnName("Descripcion");
            });
        }
    }
}