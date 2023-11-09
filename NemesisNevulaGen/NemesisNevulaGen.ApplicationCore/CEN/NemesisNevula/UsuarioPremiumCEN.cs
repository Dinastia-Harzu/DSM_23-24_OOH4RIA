

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

public int CrearUsuarioPremium (string p_nombre, string p_correo, bool p_conGoogle, string p_foto_perfil, int p_puntosNevula, float p_cartera, String p_pass, Nullable<DateTime> p_fechaCaducidad)
{
        UsuarioPremiumEN usuarioPremiumEN = null;
        int oid;

        //Initialized UsuarioPremiumEN
        usuarioPremiumEN = new UsuarioPremiumEN ();
        usuarioPremiumEN.Nombre = p_nombre;

        usuarioPremiumEN.Correo = p_correo;

        usuarioPremiumEN.ConGoogle = p_conGoogle;

        usuarioPremiumEN.Foto_perfil = p_foto_perfil;

        usuarioPremiumEN.PuntosNevula = p_puntosNevula;

        usuarioPremiumEN.Cartera = p_cartera;

        usuarioPremiumEN.Pass = Utils.Util.GetEncondeMD5 (p_pass);

        usuarioPremiumEN.FechaCaducidad = p_fechaCaducidad;



        oid = _IUsuarioPremiumRepository.CrearUsuarioPremium (usuarioPremiumEN);
        return oid;
}

public void ModificarUsuarioPremium (int p_UsuarioPremium_OID, string p_nombre, string p_correo, bool p_conGoogle, string p_foto_perfil, int p_puntosNevula, float p_cartera, String p_pass, Nullable<DateTime> p_fechaCaducidad)
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
        usuarioPremiumEN.FechaCaducidad = p_fechaCaducidad;
        //Call to UsuarioPremiumRepository

        _IUsuarioPremiumRepository.ModificarUsuarioPremium (usuarioPremiumEN);
}

public void BorrarUsuarioPremium (int id
                                  )
{
        _IUsuarioPremiumRepository.BorrarUsuarioPremium (id);
}
}
}
