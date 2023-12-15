using System.ComponentModel.DataAnnotations;

namespace NemesisNevulaWeb.Models
{
    public class ValoracionArticuloVA
    {
        public int id { get; set; }

        [Display(Prompt = "Introduce una valoración del 1 al 5", Description = "Valoración del artículo", Name = "Valoración")]
        [Required(ErrorMessage = "Debes valorar el artículo")]
        [Range(1, 5)]
        public int Valoracion { get; set; }
    }
}
