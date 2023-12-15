
using System;
using System.Text;
using System.Collections.Generic;
using NemesisNevulaGen.ApplicationCore.Exceptions;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;


/*PROTECTED REGION ID(usingNemesisNevulaGen.ApplicationCore.CEN.NemesisNevula_Articulo_actualizarValoracion) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula
{
public partial class ArticuloCEN
{
public void ActualizarValoracion (int p_oid, int valoracion)
{
        /*PROTECTED REGION ID(NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula_Articulo_actualizarValoracion) ENABLED START*/

        ArticuloEN articulo = _IArticuloRepository.ReadOIDDefault (p_oid);

        Console.WriteLine (articulo.Nombre);
        Console.WriteLine ("\nHa entrado. Media: " + valoracion);
        articulo.Valoracion = valoracion;
        _IArticuloRepository.ModificarArticulo (articulo);
        Console.WriteLine ("\nMedia establecida: " + articulo.Valoracion);

        /*PROTECTED REGION END*/
}
}
}
