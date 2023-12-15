using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaWeb.Models;

namespace NemesisNevulaWeb.Assemblers
{
    public class UsuarioAssembler
    {
        public UsuarioVM ConvertirENToViewModel (UsuarioEN en)
        {
            UsuarioVM user = new();

            user.Id = en.Id;
            user.Nombre = en.Nombre;
            user.Correo = en.Correo;
            user.ConGoogle = en.ConGoogle;
            user.Foto_perfil = en.Foto_perfil;
            user.PuntosNevula = en.PuntosNevula;
            user.Cartera = en.Cartera;
            user.Pass = en.Pass;

            return user;
        }

        public IList<UsuarioVM> ConvertirListENToViewModel (IList<UsuarioEN> ens)
        {
            IList<UsuarioVM> users = new List<UsuarioVM>();

            foreach (UsuarioEN en in ens)
            {
                users.Add(ConvertirENToViewModel(en));
            }

            return users;
        }
    }
}
