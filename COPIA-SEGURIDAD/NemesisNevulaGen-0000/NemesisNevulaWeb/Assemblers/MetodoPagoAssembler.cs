using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace NemesisNevulaWeb.Assemblers
{
    public class MetodoPagoAssembler
    {
        public MetodoPagoVM ConvertirENToViewModel(MetodoPagoEN en)
        {
            MetodoPagoVM mp = new MetodoPagoVM();
            mp.Id = en.Id;
            return mp;
        }

        public IList<MetodoPagoVM> ConvertirListENToViewModel(IList<MetodoPagoEN> lis)
        {
            IList<MetodoPagoVM> mpl = new List<MetodoPagoVM>();
            foreach (MetodoPagoEN en in lis)
            {
                mpl.Add(ConvertirENToViewModel(en));
            }
            return mpl;
        }

    }
}

