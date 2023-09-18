using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transacciones.Negocio.DTO
{
    public class TipoCuentaDTO:ICloneable
    {
        public int Id { get; set; }

        [StringLength(200, ErrorMessage = "El valor del tipo de cuenta no puede superar los 200 caracteres")]
        public string? Valor { get; set; }

        public bool? Estado { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return $"(Id={Id},Valor={Valor},Estado={Estado})";
        }
    }
}
