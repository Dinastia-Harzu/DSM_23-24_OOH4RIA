using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaWeb.Models;

namespace NemesisNevulaWeb.Assemblers
{
    public class NoticiaAssembler
    {
        public NoticiaVM EN2VM(NoticiaEN en) => new()
        {
            Id = en.Id,
            Descripcion = en.Descripcion,
            EsPublicada = en.EsPublicada
        };

        public IList<NoticiaVM> ListEN2VM(IList<NoticiaEN> ens)
        {
            IList<NoticiaVM> noticias = new List<NoticiaVM>();
            foreach(NoticiaEN en in ens)
            {
                noticias.Add(EN2VM(en));
            }
            return noticias;
        }
    }
}
