using System.ComponentModel.DataAnnotations;

namespace NemesisNevulaWeb.Models
{
    public class NoticiaVM
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        
        [Display(Prompt = "Describe la noticia", Description = "Descripción de la noticia", Name = "Descripción")]
        [Required(ErrorMessage = "Debe indicar un nombre para la noticia")]
        [StringLength(maximumLength: 200, ErrorMessage = "La descripción no puede tener más de 200 caracteres")]
        public required string Descripcion { get; set; }

        [Display(Prompt = "¿Publicas la noticia?")]
        public bool EsPublicada { get; set; }
    }
}
