

using System;
using System.Text;
using System.Collections.Generic;

using NemesisNevulaGen.ApplicationCore.Exceptions;

using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;


namespace NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula
{
/*
 *      Definition of the class PaypalCEN
 *
 */
public partial class PaypalCEN
{
private IPaypalRepository _IPaypalRepository;

public PaypalCEN(IPaypalRepository _IPaypalRepository)
{
        this._IPaypalRepository = _IPaypalRepository;
}

public IPaypalRepository get_IPaypalRepository ()
{
        return this._IPaypalRepository;
}

public int CrearPaypal (string p_email, String p_pass)
{
        PaypalEN paypalEN = null;
        int oid;

        //Initialized PaypalEN
        paypalEN = new PaypalEN ();
        paypalEN.Email = p_email;

        paypalEN.Pass = Utils.Util.GetEncondeMD5 (p_pass);



        oid = _IPaypalRepository.CrearPaypal (paypalEN);
        return oid;
}

public void ModificarPaypal (int p_Paypal_OID, string p_email, String p_pass)
{
        PaypalEN paypalEN = null;

        //Initialized PaypalEN
        paypalEN = new PaypalEN ();
        paypalEN.Id = p_Paypal_OID;
        paypalEN.Email = p_email;
        paypalEN.Pass = Utils.Util.GetEncondeMD5 (p_pass);
        //Call to PaypalRepository

        _IPaypalRepository.ModificarPaypal (paypalEN);
}

public void BorrarPaypal (int id
                          )
{
        _IPaypalRepository.BorrarPaypal (id);
}

public PaypalEN DamePorOID (int id
                            )
{
        PaypalEN paypalEN = null;

        paypalEN = _IPaypalRepository.DamePorOID (id);
        return paypalEN;
}

public System.Collections.Generic.IList<PaypalEN> DameTodos (int first, int size)
{
        System.Collections.Generic.IList<PaypalEN> list = null;

        list = _IPaypalRepository.DameTodos (first, size);
        return list;
}
}
}
