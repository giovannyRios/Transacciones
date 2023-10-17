using System;
using System.Collections.Generic;

namespace Transacciones.Dominio.Context;

public partial class Movimiento
{
    public int Id { get; set; }

    public DateTime? FechaMovimiento { get; set; }

    public int? CuentaId { get; set; }

    public decimal? Saldo { get; set; }

    public string? DescripcionMovimiento { get; set; }

    public virtual Cuenta? Cuenta { get; set; }
}
