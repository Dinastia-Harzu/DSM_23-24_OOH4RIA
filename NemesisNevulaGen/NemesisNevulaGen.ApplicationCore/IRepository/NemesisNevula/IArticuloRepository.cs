
using System;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CP.NemesisNevula;

namespace NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula
{
public partial interface IArticuloRepository
{
void setSessionCP (GenericSessionCP session);

ArticuloEN ReadOIDDefault (int id
                           );

void ModifyDefault (ArticuloEN articulo);

System.Collections.Generic.IList<ArticuloEN> ReadAllDefault (int first, int size);



int CrearArticulo (ArticuloEN articulo);

void ModificarArticulo (ArticuloEN articulo);


void BorrarArticulo (int id
                     );


ArticuloEN DamePorOID (int id
                       );


System.Collections.Generic.IList<ArticuloEN> DameTodos (int first, int size);




System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> FiltrarPorRango ();


System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> FiltrarPorFecha ();


System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> FiltrarPorTemporada ();


System.Collections.Generic.IList<ArticuloEN> FiltrarPorTipo (int first, int size);


System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> FiltrarPorPrecio ();


System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> OrdenarPorRelevancia ();


System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> OrdenarPorPrecio ();


System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> OrdenarPorFecha ();


System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> MostrarArticulosPublicados ();


System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> FiltrarPorNombre ();
}
}
