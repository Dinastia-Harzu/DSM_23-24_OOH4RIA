
using System;
using System.Text;
using System.Collections.Generic;
using NemesisNevulaGen.ApplicationCore.Exceptions;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;


/*PROTECTED REGION ID(usingNemesisNevulaGen.ApplicationCore.CEN.NemesisNevula_Usuario_comprarArticulo) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula
{
public partial class UsuarioCEN
{
public void ComprarArticulo (int p_Usuario_OID, System.Collections.Generic.IList<int> p_compraUsuario_OIDs)
{
        /*PROTECTED REGION ID(NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula_Usuario_comprarArticulo_customized) START*/


        //Call to UsuarioRepository

        _IUsuarioRepository.ComprarArticulo (p_Usuario_OID, p_compraUsuario_OIDs);

        /*PROTECTED REGION END*/
}
}
}
