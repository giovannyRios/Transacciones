using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Transacciones.Dominio.Context;

[Table("Genero")]
public partial class Genero
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(100)]
    public string? Valor { get; set; }

    [InverseProperty("Genero")]
    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
