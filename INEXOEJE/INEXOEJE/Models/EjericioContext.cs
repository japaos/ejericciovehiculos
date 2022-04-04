using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace INEXOEJE.Models
{
    public partial class EjericioContext : DbContext
    {
        public EjericioContext()
        {
        }

        public EjericioContext(DbContextOptions<EjericioContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Inspeccion> Inspeccions { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<Revision> Revisions { get; set; } = null!;
        public virtual DbSet<TipoRevision> TipoRevisions { get; set; } = null!;
        public virtual DbSet<Vehiculo> Vehiculos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-STQQ3HV\\BDSQLSERVER;Database=Ejericio;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inspeccion>(entity =>
            {
                entity.ToTable("Inspeccion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Inspeccions)
                    .HasForeignKey(d => d.PersonaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inspeccion_Persona");

                entity.HasOne(d => d.Revision)
                    .WithMany(p => p.Inspeccions)
                    .HasForeignKey(d => d.RevisionId)
                    .HasConstraintName("FK_Inspeccion_Revision");

                entity.HasOne(d => d.TipoInspeccionNavigation)
                    .WithMany(p => p.Inspeccions)
                    .HasForeignKey(d => d.TipoInspeccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inspeccion_TipoRevision");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("Persona");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(10)
                    .HasColumnName("apellido")
                    .IsFixedLength();

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(10)
                    .HasColumnName("identificacion")
                    .IsFixedLength();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Revision>(entity =>
            {
                entity.ToTable("Revision");

                entity.Property(e => e.Aprovado)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.FechaInspeccion).HasColumnType("date");

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Revisions)
                    .HasForeignKey(d => d.PersonaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Revision_Persona");

                entity.HasOne(d => d.Vehiculo)
                    .WithMany(p => p.Revisions)
                    .HasForeignKey(d => d.VehiculoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Revision_Vehiculo");
            });

            modelBuilder.Entity<TipoRevision>(entity =>
            {
                entity.ToTable("TipoRevision");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.NombreTipo)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.ToTable("Vehiculo");

                entity.Property(e => e.Marca)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Modelo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Patente)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Vehiculos)
                    .HasForeignKey(d => d.PersonaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehiculo_Vehiculo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
