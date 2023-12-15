using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using System.ComponentModel.DataAnnotations;

namespace NemesisNevulaWeb.Models
{
    public class ValoracionArticuloVM
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Prompt = "Introduce una valoración del 1 al 5", Description = "Valoración del artículo", Name = "Valoración")]
        [Required(ErrorMessage = "Debes valorar el artículo")]
        [Range(1, 5)]
        public int Valoracion { get; set; }

        public int Articulo { get; set; }

        public int Usuario { get; set; }
    }
}
