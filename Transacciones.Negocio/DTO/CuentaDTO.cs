using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transacciones.Negocio.DTO
{
    public class CuentaDTO:ICloneable
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "El número de cuenta no puede superar los 50 caracteres")]
        [Required(ErrorMessage = "El numero de cuenta es requerido")]
        public string? NumeroCuenta { get; set; }

        [Required(ErrorMessage = "El campo TipoCuentaId es requerido")]
        [Range(1,2,ErrorMessage = "El campo TipoCuentaId debe ser mayor a 0 y menor o igual a 2")]
        public int? TipoCuentaId { get; set; }

        public string? ValorTipoCuenta { get; set; } 

        [Required(ErrorMessage = "El campo ClienteId es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo ClienteId debe ser mayor a 0")]
        public int? ClienteId { get; set; }


        [Required(ErrorMessage = "El campo Saldo es requerido")]
        public decimal? Saldo { get; set; }

        [Required(ErrorMessage = "El campo estado es requerido")]
        public bool? Estado { get; set; }

        [Required(ErrorMessage = "El valor  FechaCreacion es requerido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? FechaCreacion { get; set; }

        [Required(ErrorMessage = "El valor  FechaActualizacion es requerido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? FechaActualizacion { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return $"(Id={Id},NumeroCuenta={NumeroCuenta},TipoCuentaId={TipoCuentaId},ClienteId={ClienteId},Saldo={Saldo},Estado={Estado})";
        }
    }
}
