using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace NemesisNevulaWeb.Assemblers
{
    public class PaypalAssembler
    {
        public PaypalVM ConvertirENToViewModel(PaypalEN en)
        {
            PaypalVM pp = new PaypalVM();
            pp.Email = en.Email;
            pp.Pass = en.Pass;
            return pp;
        }

        public IList<PaypalVM> ConvertirListENToViewModel(IList<PaypalEN> lis)
        {
            IList<PaypalVM> ppl = new List<PaypalVM>();
            foreach (PaypalEN en in lis)
            {
                ppl.Add(ConvertirENToViewModel(en));
            }
            return ppl;
        }
    }
}
