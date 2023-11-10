
using System;
using System.Text;

using System.Collections.Generic;
using NemesisNevulaGen.ApplicationCore.Exceptions;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;



/*PROTECTED REGION ID(usingNemesisNevulaGen.ApplicationCore.CP.NemesisNevula_Compra_regalar) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace NemesisNevulaGen.ApplicationCore.CP.NemesisNevula
{
public partial class CompraCP : GenericBasicCP
{
public void Regalar (int p_oid, int p_usuario_r, int p_articulo)
{
        /*PROTECTED REGION ID(NemesisNevulaGen.ApplicationCore.CP.NemesisNevula_Compra_regalar) ENABLED START*/

        CompraCEN compraCEN = null;

        try
        {
                CPSession.SessionInitializeTransaction ();


                compraCEN = new  CompraCEN (CPSession.UnitRepo.CompraRepository);
                UsuarioCEN usuarioCEN = new UsuarioCEN (CPSession.UnitRepo.UsuarioRepository);
                ArticuloCEN articuloCEN = new ArticuloCEN (CPSession.UnitRepo.ArticuloRepository);

                CompraEN compra = compraCEN.DamePorOID (p_oid);
                ArticuloEN articulo = articuloCEN.DamePorOID (p_articulo);
                UsuarioEN usuarioObjetivo = usuarioCEN.DamePorOID (p_usuario_r);
                UsuarioEN usuarioCompra = compra.UsuarioComprador;

                usuarioCompra.Articulo.Remove (articulo); // Le quitamos el articulo comprado al usuario

                usuarioObjetivo.Articulo.Add (articulo); // Se lo añadimos al usuario objetivo

                compra.Regalado = true; // Actualizamos a regalado

                // Actualizamos la compra y los usuarios modificados
                usuarioCEN.get_IUsuarioRepository ().ModificarUsuario (usuarioCompra);
                usuarioCEN.get_IUsuarioRepository ().ModificarUsuario (usuarioObjetivo);
                compraCEN.get_ICompraRepository ().ModificarCompra (compra);

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
