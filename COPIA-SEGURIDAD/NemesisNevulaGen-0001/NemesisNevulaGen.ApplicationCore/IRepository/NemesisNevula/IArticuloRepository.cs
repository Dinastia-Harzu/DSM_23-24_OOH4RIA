
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




System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> FiltrarPorRareza (NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.RarezaArticuloEnum ? p_rango);


System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> FiltrarPorFecha (Nullable<DateTime> p_fecha_ini, Nullable<DateTime> p_fecha_fin);


System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> FiltrarPorTemporada (string p_temp);


System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> FiltrarPorTipo (NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoArticuloEnum ? p_tipo);


System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> OrdenarPorPrecioDesc ();


System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> OrdenarPorFechaDesc ();


System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> MostrarArticulosPublicados ();


System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> FiltrarPorNombre (string p_nombre);


System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> OrdenarPorPrecioAsc ();


System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> OrdenarPorFechaAsc ();
}
}
