using System.ComponentModel.DataAnnotations;

namespace NemesisNevulaWeb.Models
{
    public class NoticiaVM
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(
            Prompt = "Pon un título a la noticia",
            Description = "Título de la noticia",
            Name = "Título"
        )]
        [Required(ErrorMessage = "Debes nombrar la noticia")]
        [StringLength(
            maximumLength: 50,
            ErrorMessage = "El título no puede tener más de 50 caracteres"
        )]
        public required string Titulo { get; set; }

        [Display(
            Prompt = "Describe la noticia",
            Description = "Descripción de la noticia",
            Name = "Descripción"
        )]
        [Required(ErrorMessage = "Debes indicar un nombre para la noticia")]
        [StringLength(
            maximumLength: 5000,
            ErrorMessage = "La descripción no puede tener más de 5000 caracteres"
        )]
        public string? Descripcion { get; set; }

        public string? Foto { get; set; }

        [Display(Prompt = "¿Publicas la noticia?")]
        public bool EsPublicada { get; set; }
    }
}
