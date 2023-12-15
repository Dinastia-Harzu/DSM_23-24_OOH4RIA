
using System;
using System.Text;

using System.Collections.Generic;
using NemesisNevulaGen.ApplicationCore.Exceptions;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;



/*PROTECTED REGION ID(usingNemesisNevulaGen.ApplicationCore.CP.NemesisNevula_Articulo_actualizarValoracion) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace NemesisNevulaGen.ApplicationCore.CP.NemesisNevula
{
public partial class ArticuloCP : GenericBasicCP
{
public void ActualizarValoracion (int p_oid)
{
        /*PROTECTED REGION ID(NemesisNevulaGen.ApplicationCore.CP.NemesisNevula_Articulo_actualizarValoracion) ENABLED START*/

        ArticuloCEN articuloCEN = null;


        try
        {
                CPSession.SessionInitializeTransaction ();
                articuloCEN = new  ArticuloCEN (CPSession.UnitRepo.ArticuloRepository);

                ValoracionArticuloCEN valoracionArticuloCEN = new ValoracionArticuloCEN (CPSession.UnitRepo.ValoracionArticuloRepository);

                ArticuloEN articulo = articuloCEN.DamePorOID (p_oid);
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
                // articuloCEN.get_IArticuloRepository ().ModificarArticulo (articulo);

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
