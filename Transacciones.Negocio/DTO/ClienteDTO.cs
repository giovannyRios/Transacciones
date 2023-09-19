using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transacciones.Negocio.DTO
{
    public class ClienteDTO:ICloneable
    {
        public int Id { get; set; }

        [StringLength(300, ErrorMessage = "El nombre no puede superar los 300 caracteres")]
        [Required(ErrorMessage = "El Nombre es requerido")]
        [RegularExpression(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$", ErrorMessage = "El nombre debe contener solo letras y espacios.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El GeneroId es requerido")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El GeneroId debe contener solo números.")]
        [Range(1, 3, ErrorMessage = "El GeneroId debe ser mayor a 0 y menor o igual a 3")]
        public int? GeneroId { get; set; }

        public string ValorGenero { get; set; }


        [Required(ErrorMessage = "la Edad es requerida")]
        [Range(19, 90, ErrorMessage = "La edad debe ser mayor a 18 años y menor o igual a 120 años")]
        public int? Edad { get; set; }

        [StringLength(20, ErrorMessage = "La identificación no puede superar los 300 caracteres")]
        [Required(ErrorMessage = "La identificación es requerida")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "La Identificación solo puede contener solo letras y números.")]
        public string? Identificacion { get; set; }

        [StringLength(500, ErrorMessage = "La dirección no puede superar los 500 caracteres")]
        [Required(ErrorMessage = "La Dirección es requerida")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "El Telefono es requerido")]
        [RegularExpression(@"^3\d{9}$", ErrorMessage = "El número de celular debe empezar con 3 y tener 10 dígitos.")]
        public string? Telefono { get; set; }

        [StringLength(12, ErrorMessage = "La contraseña no puede superar los 100 caracteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^*&+=!])(?=.{8,12}).*$", ErrorMessage = "El campo debe tener mínimo 1 mayúscula, 1 minúscula, un carácter especial '@#$%^&+=!' Y un número, la contraseña debe tener entre 8 y 12 caracteres")]
        public string? Contrasena { get; set; }


        [Required(ErrorMessage = "El campo Estado es requerido")]
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
            return $"(Nombre={Nombre},GeneroId={GeneroId},Edad={Edad},Identificacion={Identificacion},Direccion={Direccion},Telefono={Telefono})";
        }
    }
}
