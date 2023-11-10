
using System;
using System.Text;

using System.Collections.Generic;
using NemesisNevulaGen.ApplicationCore.Exceptions;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;



/*PROTECTED REGION ID(usingNemesisNevulaGen.ApplicationCore.CP.NemesisNevula_Usuario_valorarArticulo) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace NemesisNevulaGen.ApplicationCore.CP.NemesisNevula
{
public partial class UsuarioCP : GenericBasicCP
{
public void ValorarArticulo (int p_oid, int p_articulo)
{
        /*PROTECTED REGION ID(NemesisNevulaGen.ApplicationCore.CP.NemesisNevula_Usuario_valorarArticulo) ENABLED START*/

        UsuarioCEN usuarioCEN = null;



        try
        {
                CPSession.SessionInitializeTransaction ();

                usuarioCEN = new  UsuarioCEN (CPSession.UnitRepo.UsuarioRepository);
                ValoracionArticuloCEN valoracionArticuloCEN = new ValoracionArticuloCEN (CPSession.UnitRepo.ValoracionArticuloRepository);
                ArticuloCEN articuloCEN = new ArticuloCEN (CPSession.UnitRepo.ArticuloRepository);

                UsuarioEN usuario = usuarioCEN.DamePorOID (p_oid);
                ArticuloEN articulo = articuloCEN.DamePorOID (p_articulo);

                int valoracionUsuario = 5;

                int idNuevaValoracion = valoracionArticuloCEN.CrearValoracion (valoracionUsuario, p_articulo, p_oid);

                CPSession.Commit ();
        }
        catch (Exception ex)
        {
                CPSession.RollBack ();
                throw ex;
        }
        finally
        {
                CPSession.SessionClose ();
        }


        /*PROTECTED REGION END*/
}
}
}
