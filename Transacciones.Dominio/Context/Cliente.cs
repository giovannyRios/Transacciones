using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Transacciones.Dominio.Context;

[Index("Contrasena", Name = "UQ_Contraseña", IsUnique = true)]
[Index("Identificacion", Name = "UQ_Identificacion", IsUnique = true)]
public partial class Cliente
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

    [StringLength(100)]
    public string? Contrasena { get; set; }

    public bool? Estado { get; set; }

    [Column("Fecha_Creacion", TypeName = "date")]
    public DateTime? FechaCreacion { get; set; }

    [Column("Fecha_Actualizacion", TypeName = "date")]
    public DateTime? FechaActualizacion { get; set; }

    [InverseProperty("Cliente")]
    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();
}
