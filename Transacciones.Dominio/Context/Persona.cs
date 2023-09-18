using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Transacciones.Dominio.Context;

[Table("Persona")]
[Index("Identificacion", Name = "Clave")]
[Index("Identificacion", Name = "UQ_Identificacion", IsUnique = true)]
public partial class Persona
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(300)]
    public string? Nombre { get; set; }

    [Column("Genero_Id")]
    public int? GeneroId { get; set; }

    public int? Edad { get; set; }

    [StringLength(300)]
    public string? Identificacion { get; set; }

    [StringLength(500)]
    public string? Direccion { get; set; }

    [StringLength(15)]
    public string? Telefono { get; set; }

    [InverseProperty("Persona")]
    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    [ForeignKey("GeneroId")]
    [InverseProperty("Personas")]
    public virtual Genero? Genero { get; set; }
}
