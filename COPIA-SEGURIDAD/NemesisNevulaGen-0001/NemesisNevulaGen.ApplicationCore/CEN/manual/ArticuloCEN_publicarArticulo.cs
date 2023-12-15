
using System;
using System.Text;
using System.Collections.Generic;
using NemesisNevulaGen.ApplicationCore.Exceptions;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;


/*PROTECTED REGION ID(usingNemesisNevulaGen.ApplicationCore.CEN.NemesisNevula_Articulo_publicarArticulo) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula
{
public partial class ArticuloCEN
{
public void PublicarArticulo (int p_oid)
{
        /*PROTECTED REGION ID(NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula_Articulo_publicarArticulo) ENABLED START*/


        ArticuloEN articulo = _IArticuloRepository.ReadOIDDefault (p_oid);

        try{
                if (articulo.EsPublicado == false) articulo.EsPublicado = true;
                Console.WriteLine ("\n" + "El articulo es publicado: " + articulo.EsPublicado + "\n");
                _IArticuloRepository.ModificarArticulo (articulo);
        }
        catch (Exception ex) {
                throw ex;
        }


        /*PROTECTED REGION END*/
}
}
}
