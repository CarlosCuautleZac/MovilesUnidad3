﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public partial class ItesrcneDocentesContext : DbContext
{
    public ItesrcneDocentesContext()
    {
    }

    public ItesrcneDocentesContext(DbContextOptions<ItesrcneDocentesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departamentos> Departamentos { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=204.93.216.11;database=itesrcne_docentes;user=itesrcne_docente;password=docentes1", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.29-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Departamentos>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("departamentos");

            entity.HasIndex(e => e.IdSuperior, "deptosuper_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Clave).HasMaxLength(45);
            entity.Property(e => e.Contraseña).HasMaxLength(45);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Eliminado).HasColumnType("bit(1)");
            entity.Property(e => e.IdSuperior).HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.IdSuperiorNavigation).WithMany(p => p.InverseIdSuperiorNavigation)
                .HasForeignKey(d => d.IdSuperior)
                .HasConstraintName("deptosuper");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.HasIndex(e => new { e.IdDepartamento, e.Eliminado }, "deptoeliminado");

            entity.HasIndex(e => e.IdDepartamento, "deptous_idx");

            entity.HasIndex(e => e.NumEmpleado, "user");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Eliminado).HasColumnType("bit(1)");
            entity.Property(e => e.IdDepartamento).HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.NumEmpleado).HasMaxLength(10);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("deptous");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
