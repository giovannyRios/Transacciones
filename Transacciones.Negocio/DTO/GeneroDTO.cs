using System.ComponentModel.DataAnnotations;

namespace Transacciones.Negocio.DTO
{
    public class GeneroDTO : ICloneable
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
