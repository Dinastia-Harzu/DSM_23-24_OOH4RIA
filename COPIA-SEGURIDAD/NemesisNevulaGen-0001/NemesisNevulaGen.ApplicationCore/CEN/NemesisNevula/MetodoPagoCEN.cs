

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

public int CrearMetodoPago (System.Collections.Generic.IList<int> p_usuarioPoseedor)
{
        MetodoPagoEN metodoPagoEN = null;
        int oid;

        //Initialized MetodoPagoEN
        metodoPagoEN = new MetodoPagoEN ();

        metodoPagoEN.UsuarioPoseedor = new System.Collections.Generic.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN>();
        if (p_usuarioPoseedor != null) {
                foreach (int item in p_usuarioPoseedor) {
                        NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN en = new NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN ();
                        en.Id = item;
                        metodoPagoEN.UsuarioPoseedor.Add (en);
                }
        }

        else{
                metodoPagoEN.UsuarioPoseedor = new System.Collections.Generic.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN>();
        }



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

public MetodoPagoEN DamePorOID (int id
                                )
{
        MetodoPagoEN metodoPagoEN = null;

        metodoPagoEN = _IMetodoPagoRepository.DamePorOID (id);
        return metodoPagoEN;
}

public System.Collections.Generic.IList<MetodoPagoEN> DameTodos (int first, int size)
{
        System.Collections.Generic.IList<MetodoPagoEN> list = null;

        list = _IMetodoPagoRepository.DameTodos (first, size);
        return list;
}
}
}
