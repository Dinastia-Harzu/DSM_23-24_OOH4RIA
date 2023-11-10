
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


        try
        {
                CPSession.SessionInitializeTransaction ();

                UsuarioCEN usuarioCEN = new  UsuarioCEN (CPSession.UnitRepo.UsuarioRepository);
                CompraCEN compraCEN = new CompraCEN (CPSession.UnitRepo.CompraRepository);
                ArticuloCEN articuloCEN = new ArticuloCEN (CPSession.UnitRepo.ArticuloRepository);
                PaypalCEN paypalCEN = new PaypalCEN (CPSession.UnitRepo.PaypalRepository);
                TarjetaCreditoCEN tarjetaCreditoCEN = new TarjetaCreditoCEN (CPSession.UnitRepo.TarjetaCreditoRepository);

                CompraCP compraCP = new CompraCP (CPSession);

                UsuarioEN usuario = usuarioCEN.DamePorOID (p_Usuario_OID);
                ArticuloEN articulo = articuloCEN.DamePorOID (p_articulo);

                // Calculamos el precio final del articulo
                float precioTotal = articulo.Precio;
                if (p_aplicarDescuento == true) {
                        precioTotal = compraCP.AplicarDescuento (p_aplicarDescuento, p_Usuario_OID, p_articulo, articulo.Precio);
                }

                Enumerated.NemesisNevula.TipoPagoEnum tipoPagoEnum = Enumerated.NemesisNevula.TipoPagoEnum.cartera;
                Enumerated.NemesisNevula.TipoTarjetaEnum tipoTarjetaEnum = Enumerated.NemesisNevula.TipoTarjetaEnum.ninguna;
                TarjetaCreditoEN tarjetaUsuario;

                // Recogemos los datos del metodoPago
                if (p_metodoPago != -1) {
                        if (paypalCEN.DamePorOID (p_metodoPago) != null) {
                                tipoPagoEnum = Enumerated.NemesisNevula.TipoPagoEnum.paypal;
                                tipoTarjetaEnum = Enumerated.NemesisNevula.TipoTarjetaEnum.ninguna;
                        }
                        else if ((tarjetaUsuario = tarjetaCreditoCEN.DamePorOID (p_metodoPago)) != null) {
                                tipoPagoEnum = Enumerated.NemesisNevula.TipoPagoEnum.tarjeta;
                                tipoTarjetaEnum = tarjetaUsuario.TipoTarjeta;
                        }
                }
                else{
                        tipoPagoEnum = Enumerated.NemesisNevula.TipoPagoEnum.cartera;
                        tipoTarjetaEnum = Enumerated.NemesisNevula.TipoTarjetaEnum.ninguna;

                        if (usuario.Cartera < precioTotal)
                                Console.WriteLine ("Error: No tiene saldo suficiente en la cartera");
                        else{
                                usuario.Cartera -= precioTotal;
                        }
                }

                int idNuevaCompra = compraCEN.CrearCompra (DateTime.Now, tipoPagoEnum, p_Usuario_OID, p_articulo, tipoTarjetaEnum, precioTotal, DateTime.Now.AddHours (2), 1, false, p_Usuario_OID);

                // Actualizamos la lista de articulos del usuario
                usuario.Articulo.Add (articulo);
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

        /*PROTECTED REGION END*/
}
}
}
