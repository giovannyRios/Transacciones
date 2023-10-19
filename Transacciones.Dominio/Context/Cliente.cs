namespace Transacciones.Dominio.Context;

public partial class Cliente
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public int? GeneroId { get; set; }

    public int? Edad { get; set; }

    public string? Identificacion { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Contrasena { get; set; }

    public bool? Estado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();

    public virtual Genero? Genero { get; set; }
}
