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

        [StringLength(100,ErrorMessage = "El Id del cliente no puede superar los 100 caracteres")]
        [Required(ErrorMessage = "El campo ClienteId es obligatorio")]
        public string? ClienteId { get; set; }

        [StringLength(12, ErrorMessage = "La contraseña no puede superar los 100 caracteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^*&+=!])(?=.{8,12}).*$", ErrorMessage = "El campo debe tener mínimo 1 mayúscula, 1 minúscula, un carácter especial '@#$%^&+=!' Y un número, la contraseña debe tener entre 8 y 12 caracteres")]
        public string? Contrasena { get; set; }

        [Required(ErrorMessage = "El campo PersonaId es obligatorio")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El PersonaId debe contener solo números.")]
        [Range(1, int.MaxValue, ErrorMessage = "El PersonaId debe ser mayor a 0")]
        public int? PersonaId { get; set; }

        [Required(ErrorMessage = "El campo Estado es requerido")]
        public bool? Estado { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return $"(Id={Id},ClienteId={ClienteId},Contrasena={Contrasena},PersonaId={PersonaId},Estado={Estado})";
        }
    }
}
