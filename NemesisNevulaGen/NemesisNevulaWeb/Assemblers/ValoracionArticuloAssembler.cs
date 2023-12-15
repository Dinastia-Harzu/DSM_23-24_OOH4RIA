using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaWeb.Models;

namespace NemesisNevulaWeb.Assemblers
{
    public class ValoracionArticuloAssembler
    {
        public ValoracionArticuloVM EN2VM(ValoracionArticuloEN en) => new()
        {
            Id = en.Id,
            Valoracion = en.Valoracion,
            Articulo = en.Articulo == null ? -1 : en.Articulo.Id,
            Usuario = en.Usuario == null ? -1 : en.Usuario.Id,
        };

        public IList<ValoracionArticuloVM> ListaEN2ListaVM(IList<ValoracionArticuloEN> listaEN)
        {
            IList<ValoracionArticuloVM> listaVM = new List<ValoracionArticuloVM>();
            foreach(ValoracionArticuloEN en in listaEN)
            {
                listaVM.Add(EN2VM(en));
            }
            return listaVM;
        }
    }
}
