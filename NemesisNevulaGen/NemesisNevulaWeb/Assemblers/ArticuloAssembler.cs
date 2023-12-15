using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NemesisNevulaWeb.Assemblers
{
    public class ArticuloAssembler
    {
        public ArticuloVM ConvertirENToViewModel(ArticuloEN en)
        {
            ArticuloVM art = new ArticuloVM ();

            art.Id =                en.Id;
            art.Nombre =            en.Nombre == null ? "Default" : en.Nombre;
            art.Descripcion =       en.Descripcion == null ? "Default" : en.Descripcion;
            art.EsPublicado =       en.EsPublicado;
            art.Previsualizacion =  en.Previsualizacion == null ? "Default" : en.Previsualizacion;
            art.Fotografia =        en.Fotografia == null ? "Default" : en.Fotografia;
            art.Rareza =            en.Rareza;
            art.Temporada =         en.Temporada == null ? "Default" : en.Temporada;
            art.Tipo    =           en.Tipo;
            art.Precio =            en.Precio;
            art.Valoracion =        en.Valoracion;
            art.FechaPublicacion =  en.FechaPublicacion == null ? System.DateTime.MinValue : (DateTime)en.FechaPublicacion;
            return art;
        }

        public IList<ArticuloVM> ConvertirListENToViewModel (IList<ArticuloEN> list) {
            IList<ArticuloVM> arts = new List<ArticuloVM> ();
            foreach (ArticuloEN item in list)
            {
                arts.Add(ConvertirENToViewModel(item));
            }
            return arts;
        }
    }
}
