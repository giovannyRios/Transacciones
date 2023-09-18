using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Transacciones.Dominio.Context;

public partial class TransaccionesContext : DbContext
{
    public TransaccionesContext()
    {
    }

    public TransaccionesContext(DbContextOptions<TransaccionesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cuentum> Cuenta { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<TipoCuentum> TipoCuenta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-CUUI3OV;Database=Transacciones;User Id=giovanny;Password=Giovanny;TrustServerCertificate=True");
        }
    
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cliente__3214EC27FBA56C2D");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Persona).WithMany(p => p.Clientes).HasConstraintName("FK_PersonaId");
        });

        modelBuilder.Entity<Cuentum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cuenta__3214EC2750108263");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Cliente).WithMany(p => p.Cuenta).HasConstraintName("FK_ClienteId");

            entity.HasOne(d => d.TipoCuenta).WithMany(p => p.Cuenta).HasConstraintName("FK_TipoCuentaId");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genero__3214EC27FFC3EDF1");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Movimien__3214EC279AF94B83");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Cuenta).WithMany(p => p.Movimientos).HasConstraintName("FK_CuentaId");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Persona__3214EC27229BE9AC");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Genero).WithMany(p => p.Personas).HasConstraintName("FK_Genero_Id");
        });

        modelBuilder.Entity<TipoCuentum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tipo Cue__3214EC276D27343E");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
