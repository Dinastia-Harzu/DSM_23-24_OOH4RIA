
using System;
using System.Text;
using System.Collections.Generic;
using NemesisNevulaGen.ApplicationCore.Exceptions;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;


/*PROTECTED REGION ID(usingNemesisNevulaGen.ApplicationCore.CEN.NemesisNevula_Compra_devolucionPermitida) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula
{
public partial class CompraCEN
{
public bool DevolucionPermitida (int p_oid)
{
        /*PROTECTED REGION ID(NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula_Compra_devolucionPermitida) ENABLED START*/

        bool p_devolver = false;

        CompraEN compra = _ICompraRepository.DamePorOID (p_oid);
        DateTime dateTime = DateTime.Now;

        if (((DateTime)compra.Fecha).Day == dateTime.Day) {
                p_devolver = true;
        }

        _ICompraRepository.ModificarCompra (compra);
        return p_devolver;

        /*PROTECTED REGION END*/
}
}
}
