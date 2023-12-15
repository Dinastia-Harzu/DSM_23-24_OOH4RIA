
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
public void ValorarArticulo (int p_oid, int p_articulo, int p_valoracion)
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

                int idNuevaValoracion = valoracionArticuloCEN.CrearValoracion (p_valoracion, p_articulo, p_oid);
                ValoracionArticuloEN nuevaValoracion = valoracionArticuloCEN.DamePorOID (idNuevaValoracion);

                int cont = 0;
                int totalValoracion = 0;

                foreach (ValoracionArticuloEN vArt in articulo.ValoracionArticulo) {
                        Console.WriteLine ("vArt: " + vArt.Valoracion);
                        totalValoracion += vArt.Valoracion;
                        cont++;
                }

                int media = totalValoracion / cont;

                Console.WriteLine ("Operacion ha salido:" + totalValoracion + "/" + cont);
                Console.WriteLine ("La media ha salido: " + media);

                if (media >= 5) media = 5;

                articulo.Valoracion = media;

                nuevaValoracion.Articulo = articulo;
                articulo.ValoracionArticulo.Add (nuevaValoracion);

                valoracionArticuloCEN.get_IValoracionArticuloRepository ().ModificarValoracion (nuevaValoracion);
                articuloCEN.get_IArticuloRepository ().ModificarArticulo (articulo);

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
