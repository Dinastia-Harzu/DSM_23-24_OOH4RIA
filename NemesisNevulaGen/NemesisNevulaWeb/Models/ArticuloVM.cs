
using NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NemesisNevulaWeb.Models
{
    public class ArticuloVM
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(
            Prompt = "Dale un nombre al artículo",
            Description = "Nombre del artículo",
            Name = "Nombre"
        )]
        [Required(ErrorMessage = "Debe indicar el nombre del artículo")]
        [StringLength(
            maximumLength: 51,
            ErrorMessage = "El nombre no debe superar los 51 caracteres"
        )]
        public string Nombre { get; set; }

        [Display(
            Prompt = "Introduce el precio del artículo",
            Description = "Precio del artículo",
            Name = "Precio")
        ]
        [Required(ErrorMessage = "Debe indicar el precio del artículo")]
        [DataType(DataType.Currency, ErrorMessage = "El formato no es válido")]
        [RegularExpression(
            @"^[0-9]+[.,]?[0-9]{0,2}$",
            ErrorMessage = "Formato no válido"
        )]
        [Range(
            minimum: 0, maximum: 100,
            ErrorMessage = "El precio debe ser mayor que cero y menor que 100"
        )]
        public float Precio { get; set; }

        [Display(
            Prompt = "Indica el tipo de artículo",
            Description = "Tipo del artículo",
            Name = "Tipo"
        )]
        [Required(ErrorMessage = "Debe indicar el tipo de artículo")]
        public TipoArticuloEnum Tipo { get; set; }

        [Display(
            Prompt = "Indica el nivel de rareza de artículo",
            Description = "Rareza del artículo",
            Name = "Rareza"
        )]
        [Required(ErrorMessage = "Debe indicar la rareza de artículo")]
        public RarezaArticuloEnum Rareza { get; set; }

        [Display(
            Prompt = "Nombre de la imagen del artículo",
            Description = "Fotografía del artículo",
            Name = "Fotografía")
        ]
        [Required(ErrorMessage = "Debe indicar el nombre de una imagen del artículo")]
        [StringLength(
            maximumLength: 51,
            ErrorMessage = "El nombre de la fotografía no debe superar los 51 caracteres"
        )]
        public string? Fotografia { get; set; }
        public IFormFile? Fotografia2 { get; set; }

        [Display(
            Prompt = "Describe el artículo",
            Description = "Descripción del artículo",
            Name = "Descripción"
        )]
        [Required(ErrorMessage = "Debe indicar un nombre para el artículo")]
        [StringLength(
            maximumLength: 200,
            ErrorMessage = "La descripción no puede tener más de 200 caracteres"
        )]
        public string Descripcion { get; set; }

        [Display(
            Prompt = "Valora el artículo",
            Description = "Valoración del artículo",
            Name = "Valoración"
        )]
        [Range(
            minimum: 0, maximum: 5,
            ErrorMessage = "La valoración debe estar entre 0 y 5 estrellas"
        )]
        [RegularExpression(
            "([0-5]+)",
            ErrorMessage = "Por favor, introduce un número entero para la valoración"
        )]
        public int Valoracion { get; set; }

        [Display(
            Prompt = "Está publicado el artículo?",
            Description = "Estado de publicación del artículo",
            Name = "Artículo publicado"
        )]
        [Required(ErrorMessage = "Por favor, indica el estado de publicación del artículo")]
        public bool EsPublicado { get; set; }

        [Display(
            Prompt = "Introduce la fecha de publicación",
            Description = "La fecha en la que fue publicado el artículo",
            Name = "Fecha de publicación"
        )]
        [DataType(DataType.Date, ErrorMessage = "Indica la fecha de publicación")]
        public DateTime FechaPublicacion { get; set; }

        [Display(
            Prompt = "Indica la temporada",
            Description = "Temporada del artículo",
            Name = "Temporada"
        )]
        [Required(ErrorMessage = "Debe indicar la temporada del artículo")]
        [StringLength(
            maximumLength: 200,
            ErrorMessage = "El nombre de temporada no puede tener más de 200 caracteres"
        )]
        public string Temporada { get; set; }

        [Display(
            Prompt = "Indica el nombre de la previsualización",
            Description = "Previsualización del artículo",
            Name = "Previsualización"
        )]
        [Required(ErrorMessage = "Debe indicar el nombre del fichero de la previsualización del artículo")]
        [StringLength(
            maximumLength: 51,
            ErrorMessage = "El nombre del fichero no puede tener más de 51 caracteres"
        )]
        public string Previsualizacion { get; set; }

    }
}
