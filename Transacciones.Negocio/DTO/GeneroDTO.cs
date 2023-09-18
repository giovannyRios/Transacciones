using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transacciones.Negocio.DTO
{
    public class GeneroDTO:ICloneable
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "El campo genero no puede superar los 100 caracteres")]
        public string? Valor { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return $"(Id={Id},Valor={Valor})";
        }
    }
}
