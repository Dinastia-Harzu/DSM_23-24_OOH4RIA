using NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula;
using Newtonsoft.Json;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NemesisNevulaWeb.Models
{
	public class CompraVM
	{
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Prompt = "Escribe la fecha de la compra", Description = "Fecha de compra", Name = "fecha")]
        [DataType(DataType.Date, ErrorMessage = "Indica la fecha de la compra")]
        public DateTime Fecha { get; set; }

        [Display(Prompt = "Escribe el precio total de la compra", Description = "Precio total de compra", Name = "precioTotal")]
        [Required(ErrorMessage = "Debe indicar el precio total de la compra")]
        [System.ComponentModel.DataAnnotations.Range(minimum: 0, maximum: 100, ErrorMessage = "El precio debe ser mayor que 0 y menor que 100")]
        public float PrecioTotal { get; set; }

        [ScaffoldColumn(false)]
        public int IdComprador { get; set; }

        [ScaffoldColumn(false)]
        public int IdArticulo { get; set; }

        public CompraVM()
		{

		}
	}
}
