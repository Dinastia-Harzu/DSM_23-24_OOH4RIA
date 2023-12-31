
using System;
using System.Text;

using System.Collections.Generic;
using NemesisNevulaGen.ApplicationCore.Exceptions;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;



/*PROTECTED REGION ID(usingNemesisNevulaGen.ApplicationCore.CP.NemesisNevula_Compra_aplicarDescuento) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace NemesisNevulaGen.ApplicationCore.CP.NemesisNevula
{
public partial class CompraCP : GenericBasicCP
{
public float AplicarDescuento (bool p_aplicado, int p_usuario, int p_articulo, float p_precioFinal)
{
        /*PROTECTED REGION ID(NemesisNevulaGen.ApplicationCore.CP.NemesisNevula_Compra_aplicarDescuento) ENABLED START*/
        CPSession.SessionInitializeTransaction ();

        UsuarioCEN usuarioCEN = null;
        CompraCEN compraCEN = null;
        ArticuloCEN articuloCEN = null;
        try
        {
                if (p_aplicado == true) {
                        usuarioCEN = new  UsuarioCEN (CPSession.UnitRepo.UsuarioRepository);
                        articuloCEN = new  ArticuloCEN (CPSession.UnitRepo.ArticuloRepository);

                        ArticuloEN articulo = articuloCEN.DamePorOID (p_articulo);
                        UsuarioEN usuario = usuarioCEN.DamePorOID (p_usuario);

                        int puntos = usuario.PuntosNevula;

                        Console.Write ("\n\nPuntos del usuario:" + puntos + "\n");

                        float puntosTotal = (float)puntos / 100;
                        float precio = articulo.Precio;

                        Console.Write ("\n\nPrecio del artículo:" + precio + "\n");

                        p_precioFinal = precio - puntosTotal;
                        usuario.PuntosNevula = 0;

                        Console.Write ("\n\nPuntos actualizados del usuario:" + usuario.PuntosNevula + "\n");
                        Console.Write ("\n\nPrecio final de la compra:" + p_precioFinal + "\n");

                        usuarioCEN.get_IUsuarioRepository ().ModificarUsuario (usuario);

                        return p_precioFinal;
                }
                else {
                        articuloCEN = new ArticuloCEN (CPSession.UnitRepo.ArticuloRepository);
                        ArticuloEN articulo = articuloCEN.DamePorOID (p_articulo);

                        p_precioFinal = articulo.Precio;

                        Console.Write ("\n\nNo hay ningún descuento, precio final de la compra:" + p_precioFinal + "\n");
                        return p_precioFinal;
                }

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
