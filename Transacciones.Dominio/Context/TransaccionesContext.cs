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
            optionsBuilder.UseSqlServer("Server=host.docker.internal\\SQLEXPRESS;Database=Transacciones;User Id=desarrollo;Password=Prueba123*;Encrypt=False;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clientes__3214EC272DC490D2");

            entity.HasIndex(e => e.FechaActualizacion, "IX_Fecha_Actuacion").HasFillFactor(80);

            entity.HasIndex(e => e.FechaCreacion, "IX_Fecha_Creacion").HasFillFactor(80);

            entity.HasIndex(e => e.Identificacion, "IX_Identificacion").HasFillFactor(80);

            entity.HasIndex(e => e.Contrasena, "UQ_Contraseña").IsUnique();

            entity.HasIndex(e => e.Identificacion, "UQ_Identificacion").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Contrasena).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(500);
            entity.Property(e => e.FechaActualizacion)
                .HasColumnType("date")
                .HasColumnName("Fecha_Actualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("date")
                .HasColumnName("Fecha_Creacion");
            entity.Property(e => e.GeneroId).HasColumnName("Genero_Id");
            entity.Property(e => e.Identificacion).HasMaxLength(300);
            entity.Property(e => e.Nombre).HasMaxLength(300);
            entity.Property(e => e.Telefono).HasMaxLength(15);

            entity.HasOne(d => d.Genero).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.GeneroId)
                .HasConstraintName("FK_generoId");
        });

        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cuentas__3214EC2772554DCC");

            entity.HasIndex(e => e.ClienteId, "IX_Cliente_Id").HasFillFactor(80);

            entity.HasIndex(e => e.FechaActualizacion, "IX_Fecha_Actualizacion_cuenta").HasFillFactor(80);

            entity.HasIndex(e => e.FechaCreacion, "IX_Fecha_Creacion_cuenta").HasFillFactor(80);

            entity.HasIndex(e => e.NumeroCuenta, "IX_NumeroCuenta").HasFillFactor(80);

            entity.HasIndex(e => e.NumeroCuenta, "UQ_NumeroCuenta").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FechaActualizacion)
                .HasColumnType("date")
                .HasColumnName("Fecha_Actualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("date")
                .HasColumnName("Fecha_Creacion");
            entity.Property(e => e.NumeroCuenta).HasMaxLength(50);
            entity.Property(e => e.Saldo).HasColumnType("numeric(18, 2)");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK_ClienteId");

            entity.HasOne(d => d.TipoCuenta).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.TipoCuentaId)
                .HasConstraintName("FK_TipoCuentaId");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genero__3214EC276AFA0F19");

            entity.ToTable("Genero");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Valor).HasMaxLength(50);
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Movimien__3214EC2756B9A50A");

            entity.HasIndex(e => e.FechaMovimiento, "Clave");

            entity.HasIndex(e => e.FechaMovimiento, "IX_FechaMovimiento").HasFillFactor(80);

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DescripcionMovimiento).HasMaxLength(200);
            entity.Property(e => e.FechaMovimiento).HasColumnType("datetime");
            entity.Property(e => e.Saldo).HasColumnType("numeric(18, 2)");

            entity.HasOne(d => d.Cuenta).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.CuentaId)
                .HasConstraintName("FK_CuentaId");
        });

        modelBuilder.Entity<TipoCuentum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tipo_Cue__3214EC2759FFFCFF");

            entity.ToTable("Tipo_Cuenta");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Valor).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
