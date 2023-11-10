
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
public void AgregarFondos (int p_oid, int p_compra, float p_cantidad)
{
        /*PROTECTED REGION ID(NemesisNevulaGen.ApplicationCore.CP.NemesisNevula_Usuario_agregarFondos) ENABLED START*/

        UsuarioCEN usuarioCEN = null;
        CompraCEN compraCEN = null;

        try
        {
                CPSession.SessionInitializeTransaction ();
                usuarioCEN = new  UsuarioCEN (CPSession.UnitRepo.UsuarioRepository);
                compraCEN = new CompraCEN (CPSession.UnitRepo.CompraRepository);


                UsuarioEN usuario = usuarioCEN.DamePorOID (p_oid);
                CompraEN compra = compraCEN.DamePorOID (p_compra);

                usuario.Cartera += p_cantidad;
                compra.PrecioTotal = p_cantidad;

                Console.WriteLine ("\n\nSe ha añadido a la cartera " + compra.PrecioTotal + "€.");
                Console.WriteLine ("\n\nAhora el usuario cuenta con " + usuario.Cartera + "€ en la cartera.");

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
