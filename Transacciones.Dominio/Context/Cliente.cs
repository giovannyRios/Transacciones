using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Transacciones.Dominio.Context;

[Table("Cliente")]
[Index("ClienteId", Name = "Clave")]
[Index("ClienteId", Name = "UQ_ClientId", IsUnique = true)]
public partial class Cliente
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(100)]
    public string? ClienteId { get; set; }

    [StringLength(100)]
    public string? Contrasena { get; set; }

    public int? PersonaId { get; set; }

    public bool? Estado { get; set; }

    [InverseProperty("Cliente")]
    public virtual ICollection<Cuentum> Cuenta { get; set; } = new List<Cuentum>();

    [ForeignKey("PersonaId")]
    [InverseProperty("Clientes")]
    public virtual Persona? Persona { get; set; }
}
