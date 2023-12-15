using NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NemesisNevulaWeb.Models
{
    public class MetodoPagoVM
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

    }
}
