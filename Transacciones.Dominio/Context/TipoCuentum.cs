namespace Transacciones.Dominio.Context;

public partial class TipoCuentum
{
    public int Id { get; set; }

    public string? Valor { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();
}
