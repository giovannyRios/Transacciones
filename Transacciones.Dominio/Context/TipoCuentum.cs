using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Transacciones.Dominio.Context;

[Table("Tipo Cuenta")]
public partial class TipoCuentum
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(200)]
    public string? Valor { get; set; }

    public bool? Estado { get; set; }

    [InverseProperty("TipoCuenta")]
    public virtual ICollection<Cuentum> Cuenta { get; set; } = new List<Cuentum>();
}
