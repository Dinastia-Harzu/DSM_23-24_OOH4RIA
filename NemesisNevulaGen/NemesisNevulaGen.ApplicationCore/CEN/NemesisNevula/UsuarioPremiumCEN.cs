

using System;
using System.Text;
using System.Collections.Generic;

using NemesisNevulaGen.ApplicationCore.Exceptions;

using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;


namespace NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula
{
/*
 *      Definition of the class UsuarioPremiumCEN
 *
 */
public partial class UsuarioPremiumCEN
{
private IUsuarioPremiumRepository _IUsuarioPremiumRepository;

public UsuarioPremiumCEN(IUsuarioPremiumRepository _IUsuarioPremiumRepository)
{
        this._IUsuarioPremiumRepository = _IUsuarioPremiumRepository;
}

public IUsuarioPremiumRepository get_IUsuarioPremiumRepository ()
{
        return this._IUsuarioPremiumRepository;
}

public void ModificarUsuarioPremium (int p_UsuarioPremium_OID, string p_nombre, string p_correo, bool p_conGoogle, string p_foto_perfil, int p_puntosNevula, float p_cartera, String p_pass)
{
        UsuarioPremiumEN usuarioPremiumEN = null;

        //Initialized UsuarioPremiumEN
        usuarioPremiumEN = new UsuarioPremiumEN ();
        usuarioPremiumEN.Id = p_UsuarioPremium_OID;
        usuarioPremiumEN.Nombre = p_nombre;
        usuarioPremiumEN.Correo = p_correo;
        usuarioPremiumEN.ConGoogle = p_conGoogle;
        usuarioPremiumEN.Foto_perfil = p_foto_perfil;
        usuarioPremiumEN.PuntosNevula = p_puntosNevula;
        usuarioPremiumEN.Cartera = p_cartera;
        usuarioPremiumEN.Pass = Utils.Util.GetEncondeMD5 (p_pass);
        //Call to UsuarioPremiumRepository

        _IUsuarioPremiumRepository.ModificarUsuarioPremium (usuarioPremiumEN);
}

public void BorrarUsuarioPremium (int id
                                  )
{
        _IUsuarioPremiumRepository.BorrarUsuarioPremium (id);
}

public UsuarioPremiumEN DamePorOID (int id
                                    )
{
        UsuarioPremiumEN usuarioPremiumEN = null;

        usuarioPremiumEN = _IUsuarioPremiumRepository.DamePorOID (id);
        return usuarioPremiumEN;
}

public System.Collections.Generic.IList<UsuarioPremiumEN> DameTodos (int first, int size)
{
        System.Collections.Generic.IList<UsuarioPremiumEN> list = null;

        list = _IUsuarioPremiumRepository.DameTodos (first, size);
        return list;
}
}
}
