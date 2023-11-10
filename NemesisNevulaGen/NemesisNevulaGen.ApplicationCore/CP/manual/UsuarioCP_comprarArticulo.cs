
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
public void ComprarArticulo (int p_Usuario_OID, int p_articulo, int p_metodoPago, bool p_aplicarDescuento)
{
        /*PROTECTED REGION ID(NemesisNevulaGen.ApplicationCore.CP.NemesisNevula_Usuario_comprarArticulo) ENABLED START*/

        // UsuarioCEN usuarioCEN = null;
        // CompraCEN compraCEN = null;
        // ArticuloCEN articuloCEN = null;
        // PaypalCEN paypalCEN = null;
        // TarjetaCreditoCEN tarjetaCreditoCEN = null;

        // try
        // {
        //         CPSession.SessionInitializeTransaction ();

        //         usuarioCEN = new  UsuarioCEN (CPSession.UnitRepo.UsuarioRepository);
        //         CompraCEN compraCEN = new CompraCEN (CPSession.UnitRepo.CompraRepository);
        //         ArticuloCEN articuloCEN = new ArticuloCEN (CPSession.UnitRepo.ArticuloRepository);
        //         PaypalCEN paypalCEN = new PaypalCEN (CPSession.UnitRepo.PaypalRepository);
        //         TarjetaCreditoCEN tarjetaCreditoCEN = new TarjetaCreditoCEN (CPSession.UnitRepo.TarjetaCreditoRepository);

        //         //COMENTAR
        //         //bool aplicaDescuento = true;
        //         //int idPagoUsuario = paypalCEN.CrearPaypal ("jrh15@alu.ua.es", "1234");

        //         UsuarioEN usuario = usuarioCEN.DamePorOID (p_Usuario_OID);
        //         ArticuloEN articulo = articuloCEN.DamePorOID (p_articulo);

        //         // Calculamos el precio final del articulo
        //         float precioTotal = articulo.Precio;
        //         if(aplicaDescuento == true)
        //                 precioTotal = compraCEN.aplicarDescuento(p_Usuario_OID, p_articulo);

        //         // Recogemos los datos del metodoPago
        //         TipoPagoEnum tipoPagoEnum;
        //         TipoTarjetaEnum tipoTarjetaEnum;
        //         TarjetaCreditoEN tarjetaUsuario;

        //         if (idPagoUsuario != -1) {
        //                 if (paypalCEN.DamePorOID (idPagoUsuario) != null) {
        //                         tipoPagoEnum = TipoPagoEnum.paypal;
        //                         tipoTarjetaEnum = TipoTarjetaEnum.ninguna;
        //                 }
        //                 else if (tarjetaUsuario = tarjetaCreditoCEN.DamePorOID (idPagoUsuario) != null) {
        //                         tipoPagoEnum = TipoPagoEnum.tarjeta;
        //                         tipoTarjetaEnum = tarjetaUsuario.TipoTarjeta;
        //                 }
        //         }
        //         else{
        //                 tipoPagoEnum = TipoPagoEnum.cartera;
        //                 tipoTarjetaEnum = TipoTarjetaEnum.ninguna;

        //                 if(usuario.Cartera < precioTotal)
        //                         Console.WriteLine("Error: No tiene saldo suficiente en la cartera");
        //                 else{
        //                         usuario.Cartera -= precioTotal;
        //                 }
        //         }

        //         int idNuevaCompra = compraCEN.CrearCompra (DateTime.Now, tipoPagoEnum, p_Usuario_OID, p_articulo, tipoTarjetaEnum, precioTotal, DateTime.Now.AddHours (2), 1, false);

        //         // Actualizamos la lista de articulos del usuario
        //         usuario.Articulo.Add(articulo);
        //         usuarioCEN.get_IUsuarioRepository().ModificarUsuario(usuario);

        //         CPSession.Commit ();
        // }
        // catch (Exception ex)
        // {
        //         CPSession.RollBack ();
        //         throw ex;
        // }
        // finally
        // {
        //         CPSession.SessionClose ();
        // }

        /*PROTECTED REGION END*/
}
}
}
