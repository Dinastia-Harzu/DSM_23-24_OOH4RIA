using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace NemesisNevulaWeb.Assemblers
{
    public class UsuarioPremiumAssembler
    {
        public UsuarioPremiumVM ConvertirENToViewModel(UsuarioPremiumEN en)
        {
            UsuarioPremiumVM usup = new UsuarioPremiumVM();

            usup.Id = en.Id;
            usup.Nombre = en.Nombre;
            usup.Correo = en.Correo;
            usup.ConGoogle = en.ConGoogle;
            usup.Cartera = en.Cartera;
            usup.Foto_perfil = en.Foto_perfil;
            usup.PuntosNevula = en.PuntosNevula;
            usup.Pass = en.Pass;
            return usup;
        }
        public IList<UsuarioPremiumVM> ConvertirListENToViewModel(IList<UsuarioPremiumEN> list)
        {
            IList<UsuarioPremiumVM> usups = new List<UsuarioPremiumVM>();
            foreach (UsuarioPremiumEN item in list)
            {
                usups.Add(ConvertirENToViewModel(item));
            }
            return usups;
        }
    }
}
