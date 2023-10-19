namespace Transacciones.Dominio.Context;

public partial class Cuenta
{
    public int Id { get; set; }

    public string? NumeroCuenta { get; set; }

    public int? TipoCuentaId { get; set; }

    public int? ClienteId { get; set; }

    public decimal? Saldo { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public bool? Estado { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();

    public virtual TipoCuentum? TipoCuenta { get; set; }
}
