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

    public virtual DbSet<Cuenta> Cuentas { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<TipoCuentum> TipoCuenta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=sql-server-container,14330;Database=Transacciones;User Id=sa;Password=Ced1033720903");
        }    
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clientes__3214EC2766C4AD0D");

            entity.HasIndex(e => e.FechaActualizacion, "IX_Fecha_Actuacion").HasFillFactor(80);

            entity.HasIndex(e => e.FechaCreacion, "IX_Fecha_Creacion").HasFillFactor(80);

            entity.HasIndex(e => e.Identificacion, "IX_Identificacion").HasFillFactor(80);
        });

        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cuentas__3214EC272CC59AF6");

            entity.HasIndex(e => e.ClienteId, "IX_Cliente_Id").HasFillFactor(80);

            entity.HasIndex(e => e.FechaActualizacion, "IX_Fecha_Actualizacion_cuenta").HasFillFactor(80);

            entity.HasIndex(e => e.FechaCreacion, "IX_Fecha_Creacion_cuenta").HasFillFactor(80);

            entity.HasIndex(e => e.NumeroCuenta, "IX_NumeroCuenta").HasFillFactor(80);

            entity.HasOne(d => d.Cliente).WithMany(p => p.Cuenta).HasConstraintName("FK_ClienteId");

            entity.HasOne(d => d.TipoCuenta).WithMany(p => p.Cuenta).HasConstraintName("FK_TipoCuentaId");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genero__3214EC27435F3042");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Movimien__3214EC271F1A274F");

            entity.HasIndex(e => e.FechaMovimiento, "IX_FechaMovimiento").HasFillFactor(80);

            entity.HasOne(d => d.Cuenta).WithMany(p => p.Movimientos).HasConstraintName("FK_CuentaId");
        });

        modelBuilder.Entity<TipoCuentum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tipo_Cue__3214EC274DD56CB7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
