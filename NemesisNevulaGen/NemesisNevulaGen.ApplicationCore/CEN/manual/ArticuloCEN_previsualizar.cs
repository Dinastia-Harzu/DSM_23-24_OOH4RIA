
using System;
using System.Text;
using System.Collections.Generic;
using NemesisNevulaGen.ApplicationCore.Exceptions;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;


/*PROTECTED REGION ID(usingNemesisNevulaGen.ApplicationCore.CEN.NemesisNevula_Articulo_previsualizar) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula
{
public partial class ArticuloCEN
{
public void Previsualizar (int p_oid)
{
        /*PROTECTED REGION ID(NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula_Articulo_previsualizar) ENABLED START*/
        ArticuloEN articulo = _IArticuloRepository.ReadOIDDefault (p_oid);

        Console.WriteLine ("\n\n" + "Previsualización del artículo: " + articulo.Previsualizacion + "\n");

        /*PROTECTED REGION END*/
}
}
}
