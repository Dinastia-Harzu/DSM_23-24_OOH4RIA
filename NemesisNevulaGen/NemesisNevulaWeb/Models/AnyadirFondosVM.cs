using System.ComponentModel.DataAnnotations;

namespace NemesisNevulaWeb.Models
{
    public class AnyadirFondosVM
    {
        [Display(Prompt = "Seleccione un método de pago", Description = "Método de pago", Name = "Método de pago")]
        [Required(ErrorMessage = "Debe seleccionar un método de pago")]
        public int MetodoPago { get; set; }

        [Display(Prompt = "Introduzca la cantidad a añadir", Description = "Cantidad", Name = "Cantidad")]
        [Required(ErrorMessage = "Debe introducir una cantidad a añadir")]
        [Range(minimum: 0, int.MaxValue, ErrorMessage = "El precio debe ser mayor que 0")]
        [DataType(DataType.Currency)]
        public float Cantidad { get; set; }
    }
}
