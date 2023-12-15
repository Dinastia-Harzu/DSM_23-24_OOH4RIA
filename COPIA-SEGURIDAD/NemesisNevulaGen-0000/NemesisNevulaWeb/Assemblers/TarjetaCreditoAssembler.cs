using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace NemesisNevulaWeb.Assemblers
{
    public class TarjetaCreditoAssembler
    {
        public TarjetaCreditoVM ConvertirENToViewModel(TarjetaCreditoEN en)
        {
            TarjetaCreditoVM tc = new TarjetaCreditoVM();
            tc.TipoTarjeta = en.TipoTarjeta;
            tc.NombreEnTarjeta = en.NombreEnTarjeta;
            tc.Numero = en.Numero;
            tc.FechaExpedicion = en.FechaExpedicion == null ? System.DateTime.MinValue : (DateTime)en.FechaExpedicion;
            tc.CodigoSeguridad = en.CodigoSeguridad;
            return tc;
        }

        public IList<TarjetaCreditoVM> ConvertirListENToViewModel(IList<TarjetaCreditoEN> lis)
        {
            IList<TarjetaCreditoVM> tcl = new List<TarjetaCreditoVM>();
            foreach (TarjetaCreditoEN en in lis)
            {
                tcl.Add(ConvertirENToViewModel(en));
            }
            return tcl;
        }
    }
}
