namespace Transacciones.Dominio.Context;

public partial class Genero
{
    public int Id { get; set; }

    public string? Valor { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
