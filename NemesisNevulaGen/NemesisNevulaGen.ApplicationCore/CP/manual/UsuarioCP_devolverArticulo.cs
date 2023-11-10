
using System;
using System.Text;

using System.Collections.Generic;
using NemesisNevulaGen.ApplicationCore.Exceptions;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;



/*PROTECTED REGION ID(usingNemesisNevulaGen.ApplicationCore.CP.NemesisNevula_Usuario_devolverArticulo) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace NemesisNevulaGen.ApplicationCore.CP.NemesisNevula
{
public partial class UsuarioCP : GenericBasicCP
{
public void DevolverArticulo (int p_Usuario_OID, int p_compraUsuario_OIDs)
{
        /*PROTECTED REGION ID(NemesisNevulaGen.ApplicationCore.CP.NemesisNevula_Usuario_devolverArticulo) ENABLED START*/

        try
        {
                CPSession.SessionInitializeTransaction ();

                UsuarioCEN usuarioCEN = new  UsuarioCEN (CPSession.UnitRepo.UsuarioRepository);
                CompraCEN compraCEN = new CompraCEN (CPSession.UnitRepo.CompraRepository);
                ArticuloCEN articuloCEN = new ArticuloCEN (CPSession.UnitRepo.ArticuloRepository);

                UsuarioEN usuario = usuarioCEN.DamePorOID (p_Usuario_OID);
                CompraEN compra = compraCEN.DamePorOID (p_compraUsuario_OIDs);

                usuario.Cartera += compra.PrecioTotal;  // Devolemos el precio a la cartera del cliente

                usuario.Articulo.Remove (compra.Articulo);  // Eliminamos el articulo del usuario

                compraCEN.get_ICompraRepository ().BorrarCompra (p_compraUsuario_OIDs);   // Borramos la compra

                usuarioCEN.get_IUsuarioRepository ().ModificarUsuario (usuario);  // Modificamos el usuario con el dinero reembolsado y sin el articulo previamente comprado

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
