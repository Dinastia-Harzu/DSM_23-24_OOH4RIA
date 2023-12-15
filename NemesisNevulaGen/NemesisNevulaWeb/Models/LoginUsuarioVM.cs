using System.ComponentModel.DataAnnotations;

namespace NemesisNevulaWeb.Models
{
    public class LoginUsuarioVM
    {
        [Display(Prompt = "Introduzca su nombre de usuario", Description = "Nombre del usuario", Name = "Nombre de Usuario")]
        [Required(ErrorMessage = "Debe introducir un nombre de usuario")]
        public string Nombre { get; set; }

        [Display(Prompt = "Introduzca su contraseña", Description = "Contraseña del usuario", Name = "Contraseña")]
        [Required(ErrorMessage = "Debe introducir una contraseña")]
        [DataType(DataType.Password)]
        public string Pass { get; set; }
    }
}
