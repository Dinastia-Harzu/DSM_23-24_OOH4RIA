using NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NemesisNevulaWeb.Models { 
	public class UsuarioPremiumVM : UsuarioVM{

        //[ScaffoldColumn(false)]
        //public int Id { get; set; }

        //[Display(Prompt = "Introduzca su nombre de usuario", Description = "Nombre del usuario", Name = "Nombre de Usuario")]
        //[Required(ErrorMessage = "Debe introducir un nombre de usuario")]
        //[StringLength(maximumLength: 15, ErrorMessage = "El nombre de usuario no puede superar los 15 caracteres")]
        //public string Nombre { get; set; }

        //[Display(Prompt = "Introduzca un email", Description = "Email del usuario", Name = "Email")]
        //[Required(ErrorMessage = "Debe introducir un email")]
        //[DataType(DataType.Password)]
        //[RegularExpression(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,5}", ErrorMessage = "Correo con formato no válido")]
        //public string Correo { get; set; }

        //[ScaffoldColumn(false)]
        //public bool ConGoogle { get; set; }

        //[Display(Prompt = "Inserte su foto de perfil", Description = "Foto de perfil del usuario", Name = "Foto de perfil")]
        //[Required(ErrorMessage = "Debe introducir una foto de perfil")]
        //public string Foto_perfil { get; set; }

        //[ScaffoldColumn(false)]
        //public int PuntosNevula { get; set; }

        //[ScaffoldColumn(false)]
        //[DataType(DataType.Currency, ErrorMessage = "El valor debe ser un valor numérico")]
        //[Range(minimum: 0, maximum: 10000, ErrorMessage = "El precio debe ser mayor que 0 y menor que 10000")]
        //public float Cartera { get; set; }

        //[Display(Prompt = "Introduzca su contraseña", Description = "Contraseña del usuario", Name = "Contraseña")]
        //[Required(ErrorMessage = "Debe introducir una contraseña")]
        //[DataType(DataType.Password)]
        //public string Pass { get; set; }

        [Display(Prompt = "Escribe la fecha de caducidad del usuario premium", Description = "Fecha de caducidad del usuario premium", Name = "fechaCaducidad")]
		[DataType(DataType.Date, ErrorMessage = "Indica la fecha de caducidad del usuario premium")]
		public DateTime FechaCaducidad { get; set; }

		public UsuarioPremiumVM ()
		{

		}
	}
}
