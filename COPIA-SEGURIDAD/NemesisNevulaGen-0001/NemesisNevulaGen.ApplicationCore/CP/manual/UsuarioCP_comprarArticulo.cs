
using System;
using System.Text;

using System.Collections.Generic;
using NemesisNevulaGen.ApplicationCore.Exceptions;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;



/*PROTECTED REGION ID(usingNemesisNevulaGen.ApplicationCore.CP.NemesisNevula_Usuario_comprarArticulo) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace NemesisNevulaGen.ApplicationCore.CP.NemesisNevula
{
public partial class UsuarioCP : GenericBasicCP
{
public int ComprarArticulo (int p_Usuario_OID, int p_articulo, bool p_aplicarDescuento)
{
        /*PROTECTED REGION ID(NemesisNevulaGen.ApplicationCore.CP.NemesisNevula_Usuario_comprarArticulo) ENABLED START*/

        int compra_OID = -1;

        try
        {
                CPSession.SessionInitializeTransaction ();

                UsuarioCEN usuarioCEN = new  UsuarioCEN (CPSession.UnitRepo.UsuarioRepository);
                CompraCEN compraCEN = new CompraCEN (CPSession.UnitRepo.CompraRepository);
                ArticuloCEN articuloCEN = new ArticuloCEN (CPSession.UnitRepo.ArticuloRepository);

                CompraCP compraCP = new CompraCP (CPSession);

                UsuarioEN usuario = usuarioCEN.DamePorOID (p_Usuario_OID);
                ArticuloEN articulo = articuloCEN.DamePorOID (p_articulo);

                // Calculamos el precio final del articulo
                float precioTotal = articulo.Precio;
                if (p_aplicarDescuento == true) {
                        precioTotal = compraCP.AplicarDescuento (p_aplicarDescuento, p_Usuario_OID, p_articulo, articulo.Precio);
                }

                if (usuario.Cartera < precioTotal)
                        Console.WriteLine ("Error: No tiene saldo suficiente en la cartera");
                else{
                        usuario.Cartera -= precioTotal;

                        IList<int> artsActualizados = new List<int>();
                        artsActualizados.Add (articulo.Id);
                        usuarioCEN.AnyadirArticulo (p_Usuario_OID, artsActualizados);

                        compra_OID = compraCEN.CrearCompra (DateTime.Now, p_Usuario_OID, p_articulo, precioTotal, DateTime.Now.AddHours (2), false);

                        // Actualizamos el usuario, ya que hemos modificado el valor en su cartera
                        usuarioCEN.get_IUsuarioRepository ().ModificarUsuario (usuario);
                }

                CPSession.Commit ();
        }
        catch (Exception ex)
        {
                CPSession.RollBack ();
                throw ex;
                return compra_OID;
        }
        finally
        {
                CPSession.SessionClose ();
        }

        return compra_OID;

        /*PROTECTED REGION END*/
}
}
}
