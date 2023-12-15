using NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NemesisNevulaWeb.Models
{
    public class PaypalVM : MetodoPagoVM
    {

        [Display(Prompt = "Email del Paypal", Description = "email del Paypal", Name = "Email")]
        [Required(ErrorMessage = "Debe indicar email")]
        [StringLength(maximumLength: 256, ErrorMessage = "Has alcanzado el máximo")]
        public string Email { get; set; }

        [Display(Prompt = "La contraseña de PayPal", Description = "contraseña de PayPal", Name = "contraseña")]
        [Required(ErrorMessage = "Debe indicar email")]
        [StringLength(maximumLength: 50, ErrorMessage = "Has alcanzado el máximo")]
        public string Pass { get; set; }

    }
}

