
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
public void Regalar (int p_oid, int p_usuario, int p_articulo)
{
        /*PROTECTED REGION ID(NemesisNevulaGen.ApplicationCore.CP.NemesisNevula_Compra_regalar) ENABLED START*/

        CompraCEN compraCEN = null;



        try
        {
                CPSession.SessionInitializeTransaction ();
                compraCEN = new  CompraCEN (CPSession.UnitRepo.CompraRepository);



                // Write here your custom transaction ...

                throw new NotImplementedException ("Method Regalar() not yet implemented.");



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
