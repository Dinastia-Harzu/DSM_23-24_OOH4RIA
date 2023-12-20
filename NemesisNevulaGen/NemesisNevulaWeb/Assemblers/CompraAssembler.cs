using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace NemesisNevulaWeb.Assemblers
{
    public class CompraAssembler
    {
        public CompraVM ConvertirENToViewModel(CompraEN en)
        {
            CompraVM comp = new CompraVM();

            comp.Id = en.Id;
            comp.Fecha = en.Fecha == null ? System.DateTime.MinValue : (DateTime)en.Fecha;
            comp.PrecioTotal = en.PrecioTotal;
            comp.IdComprador = en.UsuarioComprador.Id;
            comp.IdArticulo = en.Articulo == null ? 0 : en.Articulo.Id;
            return comp;
        }
        public IList<CompraVM> ConvertirListENToViewModel(IList<CompraEN> list)
        {
            IList<CompraVM> arts = new List<CompraVM>();
            foreach (CompraEN item in list)
            {
                arts.Add(ConvertirENToViewModel(item));
            }
            return arts;
        }
    }
}
