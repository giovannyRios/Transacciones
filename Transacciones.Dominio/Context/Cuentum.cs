using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Transacciones.Dominio.Context;

[Index("NumeroCuenta", Name = "Clave")]
[Index("NumeroCuenta", Name = "UQ_NumeroCuenta", IsUnique = true)]
public partial class Cuentum
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(50)]
    public string? NumeroCuenta { get; set; }

    public int? TipoCuentaId { get; set; }

    public int? ClienteId { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? Saldo { get; set; }

    public bool? Estado { get; set; }

    [ForeignKey("ClienteId")]
    [InverseProperty("Cuenta")]
    public virtual Cliente? Cliente { get; set; }

    [InverseProperty("Cuenta")]
    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();

    [ForeignKey("TipoCuentaId")]
    [InverseProperty("Cuenta")]
    public virtual TipoCuentum? TipoCuenta { get; set; }
}
