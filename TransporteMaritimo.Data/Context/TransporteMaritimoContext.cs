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

        public DbSet<UsuarioRol> UsuariosRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios");

                entity.HasKey(e => e.iUsuarioId);

                entity.Property(e => e.iUsuarioId)
                    .HasColumnName("UsuarioId");

                entity.Property(e => e.sNombre)
                    .HasColumnName("Nombre");

                entity.Property(e => e.sEmail)
                    .HasColumnName("Email");

                entity.Property(e => e.sPasswordHash)
                    .HasColumnName("PasswordHash");

                entity.Property(e => e.bActivo)
                    .HasColumnName("Activo");

                entity.Property(e => e.iIntentosFallidos)
                    .HasColumnName("IntentosFallidos");

                entity.Property(e => e.dtBloqueadoHasta)
                    .HasColumnName("BloqueadoHasta");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Roles");

                entity.HasKey(e => e.iRolId);

                entity.Property(e => e.iRolId)
                    .HasColumnName("RolId");

                entity.Property(e => e.sNombreRol)
                    .HasColumnName("NombreRol");
            });

            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.ToTable("Permisos");

                entity.HasKey(e => e.iPermisoId);

                entity.Property(e => e.iPermisoId)
                    .HasColumnName("PermisoId");

                entity.Property(e => e.sNombrePermiso)
                    .HasColumnName("NombrePermiso");

                entity.Property(e => e.sDescripcion)
                    .HasColumnName("Descripcion");
            });

            modelBuilder.Entity<RolPermiso>(entity =>
            {
                entity.ToTable("RolPermisos");

                entity.HasKey(rp => new { rp.iRolId, rp.iPermisoId });

                entity.Property(rp => rp.iRolId)
                    .HasColumnName("RolId");

                entity.Property(rp => rp.iPermisoId)
                    .HasColumnName("PermisoId");
            });

            modelBuilder.Entity<UsuarioRol>(entity =>
            {
                entity.ToTable("UsuarioRoles");

                entity.HasKey(ur => new { ur.UsuarioId, ur.RolId });

                entity.Property(ur => ur.UsuarioId)
                    .HasColumnName("UsuarioId");

                entity.Property(ur => ur.RolId)
                    .HasColumnName("RolId");

                entity.HasOne(ur => ur.Usuario)
                    .WithMany(u => u.UsuarioRoles)
                    .HasForeignKey(ur => ur.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ur => ur.Rol)
                    .WithMany(r => r.UsuarioRoles)
                    .HasForeignKey(ur => ur.RolId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}