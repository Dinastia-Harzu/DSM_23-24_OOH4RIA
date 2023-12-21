
using System;
using System.Text;

using System.Collections.Generic;
using NemesisNevulaGen.ApplicationCore.Exceptions;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;



/*PROTECTED REGION ID(usingNemesisNevulaGen.ApplicationCore.CP.NemesisNevula_Usuario_agregarFondos) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace NemesisNevulaGen.ApplicationCore.CP.NemesisNevula
{
public partial class UsuarioCP : GenericBasicCP
{
public bool AgregarFondos (int p_oid, int p_metodoPago, float p_cantidad)
{
        /*PROTECTED REGION ID(NemesisNevulaGen.ApplicationCore.CP.NemesisNevula_Usuario_agregarFondos) ENABLED START*/

        UsuarioCEN usuarioCEN = null;
        MetodoPagoCEN metodoPagoCEN = null;
        bool vuelta = false;

        try
        {
                CPSession.SessionInitializeTransaction ();
                usuarioCEN = new  UsuarioCEN (CPSession.UnitRepo.UsuarioRepository);
                metodoPagoCEN = new MetodoPagoCEN (CPSession.UnitRepo.MetodoPagoRepository);


                UsuarioEN usuario = usuarioCEN.DamePorOID (p_oid);
                MetodoPagoEN compra = metodoPagoCEN.DamePorOID (p_metodoPago);

                usuario.Cartera += p_cantidad;

                Console.WriteLine ("\n\nSe ha añadido a la cartera " + p_cantidad + "€.");
                Console.WriteLine ("\n\nAhora el usuario cuenta con " + usuario.Cartera + "€ en la cartera.");
                vuelta = true;

                usuarioCEN.get_IUsuarioRepository ().ModificarUsuario (usuario);

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
        return vuelta;

        /*PROTECTED REGION END*/
}
}
}
