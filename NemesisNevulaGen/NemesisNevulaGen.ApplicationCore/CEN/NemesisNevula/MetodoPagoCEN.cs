

using System;
using System.Text;
using System.Collections.Generic;

using NemesisNevulaGen.ApplicationCore.Exceptions;

using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;


namespace NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula
{
/*
 *      Definition of the class MetodoPagoCEN
 *
 */
public partial class MetodoPagoCEN
{
private IMetodoPagoRepository _IMetodoPagoRepository;

public MetodoPagoCEN(IMetodoPagoRepository _IMetodoPagoRepository)
{
        this._IMetodoPagoRepository = _IMetodoPagoRepository;
}

public IMetodoPagoRepository get_IMetodoPagoRepository ()
{
        return this._IMetodoPagoRepository;
}

public int CrearMetodoPago ()
{
        MetodoPagoEN metodoPagoEN = null;
        int oid;

        //Initialized MetodoPagoEN
        metodoPagoEN = new MetodoPagoEN ();


        oid = _IMetodoPagoRepository.CrearMetodoPago (metodoPagoEN);
        return oid;
}

public void ModificarMetodoPago (int p_MetodoPago_OID)
{
        MetodoPagoEN metodoPagoEN = null;

        //Initialized MetodoPagoEN
        metodoPagoEN = new MetodoPagoEN ();
        metodoPagoEN.Id = p_MetodoPago_OID;
        //Call to MetodoPagoRepository

        _IMetodoPagoRepository.ModificarMetodoPago (metodoPagoEN);
}

public void BorrarMetodoPago (int id
                              )
{
        _IMetodoPagoRepository.BorrarMetodoPago (id);
}
}
}
