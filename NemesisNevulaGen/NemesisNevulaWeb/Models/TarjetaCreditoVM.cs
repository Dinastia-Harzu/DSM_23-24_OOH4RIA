
using NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NemesisNevulaWeb.Models
{
    public class TarjetaCreditoVM : MetodoPagoVM
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Prompt = "Nombre de la tarjeta", Description = "Nombre en la tarejta de crédito", Name = "Tarjeta")]
        [Required(ErrorMessage = "Debe indicar el nombre de tarjeta")]
        [StringLength(maximumLength: 256, ErrorMessage = "Has alcanzado el máximo")]
        public string NombreEnTarjeta { get; set; }

        [Display(Prompt = "Número de la tarjeta", Description = "Número de la tarjeta de crédito", Name = "numero")]
        [Required(ErrorMessage = "Debe indicar un número de tarjeta")]
        [StringLength(maximumLength: 20, ErrorMessage = "Has alcanzado el máximo")]
        [RegularExpression(@"^(?:(4\d{3}\s?\d{4}\s?\d{4}\s?\d{4})|(5\d{3}\s?\d{4}\s?\d{4}\s?\d{4}))$", ErrorMessage = "Número de tarjeta no válido")]
        public string Numero { get; set; }

        [Display(Prompt = "Código de seguridad", Description = "Código de seguridad de la tarjeta", Name = "codigoseguridad")]
        [Required(ErrorMessage = "Debe indicar email")]
        [StringLength(maximumLength: 50, ErrorMessage = "Has alcanzado el máximo")]
        public string CodigoSeguridad { get; set; }

        [Display(Prompt = "Fecha de expedición", Description = "Fecha de expedición de la tarjeta de crédito", Name = "fechaExpedicion")]
        [Required(ErrorMessage = "Debe indicar email")]
        [DataType(DataType.Date, ErrorMessage = "Debe ser de tipo fecha")]
        public DateTime FechaExpedicion { get; set; }

        [Display(Prompt = "Tipo de tarjeta", Description = "Tipo de la tarjeta de crédito a utilizar", Name = "tipoTrajeta")]
        [Required(ErrorMessage = "Debe indicar el ipo de tarjeta")]
        public TipoTarjetaEnum TipoTarjeta { get; set; }

    
    }
}

