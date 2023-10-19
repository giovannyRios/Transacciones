using System.ComponentModel.DataAnnotations;

namespace Transacciones.Negocio.DTO
{
    public class MovimientoDTO : ICloneable
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El valor  FechaMovimiento es requerido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? FechaMovimiento { get; set; }

        [Required(ErrorMessage = "El campo CuentaId es requerido")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El CuentaId debe contener solo números.")]
        public int? CuentaId { get; set; }

        [Required(ErrorMessage = "El campo Saldo es requerido")]
        public decimal? Saldo { get; set; }

        public string? NumeroDeCuenta { get; set; }

        [StringLength(500, ErrorMessage = "La descripción del movimiento no puede superar los 500 caracteres")]

        [Required(ErrorMessage = "El campo DescripcionMovimiento es requerido")]
        public string? DescripcionMovimiento { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return $"(Id={Id},FechaMovimiento={FechaMovimiento},CuentaId={CuentaId}),Saldo={Saldo},DescripcionMovimiento={DescripcionMovimiento})";
        }
    }
}
