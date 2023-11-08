

using System;
using System.Text;
using System.Collections.Generic;

using NemesisNevulaGen.ApplicationCore.Exceptions;

using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;


namespace NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula
{
/*
 *      Definition of the class UsuarioCEN
 *
 */
public partial class UsuarioCEN
{
private IUsuarioRepository _IUsuarioRepository;

public UsuarioCEN(IUsuarioRepository _IUsuarioRepository)
{
        this._IUsuarioRepository = _IUsuarioRepository;
}

public IUsuarioRepository get_IUsuarioRepository ()
{
        return this._IUsuarioRepository;
}

public int CrearUsuario (string p_nombre, string p_correo, bool p_conGoogle, string p_foto_perfil, int p_puntosNevula, float p_cartera)
{
        UsuarioEN usuarioEN = null;
        int oid;

        //Initialized UsuarioEN
        usuarioEN = new UsuarioEN ();
        usuarioEN.Nombre = p_nombre;

        usuarioEN.Correo = p_correo;

        usuarioEN.ConGoogle = p_conGoogle;

        usuarioEN.Foto_perfil = p_foto_perfil;

        usuarioEN.PuntosNevula = p_puntosNevula;

        usuarioEN.Cartera = p_cartera;



        oid = _IUsuarioRepository.CrearUsuario (usuarioEN);
        return oid;
}

public void ModificarUsuario (int p_Usuario_OID, string p_nombre, string p_correo, bool p_conGoogle, string p_foto_perfil, int p_puntosNevula, float p_cartera)
{
        UsuarioEN usuarioEN = null;

        //Initialized UsuarioEN
        usuarioEN = new UsuarioEN ();
        usuarioEN.Id = p_Usuario_OID;
        usuarioEN.Nombre = p_nombre;
        usuarioEN.Correo = p_correo;
        usuarioEN.ConGoogle = p_conGoogle;
        usuarioEN.Foto_perfil = p_foto_perfil;
        usuarioEN.PuntosNevula = p_puntosNevula;
        usuarioEN.Cartera = p_cartera;
        //Call to UsuarioRepository

        _IUsuarioRepository.ModificarUsuario (usuarioEN);
}

public void BorrarUsuario (int id
                           )
{
        _IUsuarioRepository.BorrarUsuario (id);
}

public void AnyadirFavorito (int p_Usuario_OID, System.Collections.Generic.IList<int> p_articulosFavs_OIDs)
{
        //Call to UsuarioRepository

        _IUsuarioRepository.AnyadirFavorito (p_Usuario_OID, p_articulosFavs_OIDs);
}
public void QuitarFavorito (int p_Usuario_OID, System.Collections.Generic.IList<int> p_articulosFavs_OIDs)
{
        //Call to UsuarioRepository

        _IUsuarioRepository.QuitarFavorito (p_Usuario_OID, p_articulosFavs_OIDs);
}
}
}
