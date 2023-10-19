using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transacciones.Negocio.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Es requerido es usuario")]
        public string? user { get; set; }

        [Required(ErrorMessage = "Es requerida la contraseña")]
        public string? password { get; set; }
    }
}
