using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Transacciones.Dominio.Context;

[Index("FechaMovimiento", Name = "Clave")]
public partial class Movimiento
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaMovimiento { get; set; }

    public int? CuentaId { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? Saldo { get; set; }

    [StringLength(200)]
    public string? DescripcionMovimiento { get; set; }

    [ForeignKey("CuentaId")]
    [InverseProperty("Movimientos")]
    public virtual Cuenta? Cuenta { get; set; }
}
