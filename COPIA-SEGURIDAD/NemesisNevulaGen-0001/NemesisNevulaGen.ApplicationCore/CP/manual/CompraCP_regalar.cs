
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
public void Regalar (int p_oid, int p_usuario_r)
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
                UsuarioEN usuarioObjetivo = usuarioCEN.DamePorOID (p_usuario_r);
                UsuarioEN usuarioCompra = compra.UsuarioComprador;

                // Le quitamos el regalo al usuario de la compra y se lo anyadimos al usuario objetivo
                IList<int> artsRegalados = new List<int>
                {
                        compra.Articulo.Id
                };
                usuarioCEN.QuitarArticulo (usuarioCompra.Id, artsRegalados);
                usuarioCEN.AnyadirArticulo (usuarioObjetivo.Id, artsRegalados);

                compra.Regalado = true;

                // Actualizamos la compra
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
