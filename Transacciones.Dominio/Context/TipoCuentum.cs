using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Transacciones.Dominio.Context;

[Table("Tipo_Cuenta")]
public partial class TipoCuentum
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(30)]
    public string? Valor { get; set; }

    public bool? Estado { get; set; }

    [InverseProperty("TipoCuenta")]
    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();
}
