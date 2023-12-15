using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace NemesisNevulaWeb.Assemblers
{
    public class AdministradorAssembler
    {
        public AdministradorVM ConvertirENToViewModel(AdministradorEN en)
        {
            AdministradorVM admin = new AdministradorVM();
            admin.Id = en.Id;
            admin.Nombre = en.Nombre;
            admin.Correo = en.Correo;
            admin.ConGoogle = en.ConGoogle;
            admin.Cartera = en.Cartera;
            admin.Foto_perfil = en.Foto_perfil;
            admin.PuntosNevula = en.PuntosNevula;
            admin.Pass = en.Pass;
            return admin;
        }
        public IList<AdministradorVM> ConvertirListENToViewModel(IList<AdministradorEN> list)
        {
            IList<AdministradorVM> admins = new List<AdministradorVM>();
            foreach (AdministradorEN item in list)
            {
                admins.Add(ConvertirENToViewModel(item));
            }
            return admins;
        }
    }
}
