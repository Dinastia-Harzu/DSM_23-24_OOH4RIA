

using System;
using System.Text;
using System.Collections.Generic;

using NemesisNevulaGen.ApplicationCore.Exceptions;

using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;
using Newtonsoft.Json;


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

public int CrearUsuario (string p_nombre, string p_correo, bool p_conGoogle, string p_foto_perfil, int p_puntosNevula, float p_cartera, String p_pass)
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

        usuarioEN.Pass = Utils.Util.GetEncondeMD5 (p_pass);



        oid = _IUsuarioRepository.CrearUsuario (usuarioEN);
        return oid;
}

public void ModificarUsuario (int p_Usuario_OID, string p_nombre, string p_correo, bool p_conGoogle, string p_foto_perfil, int p_puntosNevula, float p_cartera, String p_pass)
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
        usuarioEN.Pass = Utils.Util.GetEncondeMD5 (p_pass);
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
public string Login (int p_Usuario_OID, string p_pass)
{
        string result = null;
        UsuarioEN en = _IUsuarioRepository.ReadOIDDefault (p_Usuario_OID);

        if (en != null && en.Pass.Equals (Utils.Util.GetEncondeMD5 (p_pass)))
                result = this.GetToken (en.Id);

        return result;
}

public System.Collections.Generic.IList<UsuarioEN> DameTodos (int first, int size)
{
        System.Collections.Generic.IList<UsuarioEN> list = null;

        list = _IUsuarioRepository.DameTodos (first, size);
        return list;
}
public UsuarioEN DamePorOID (int id
                             )
{
        UsuarioEN usuarioEN = null;

        usuarioEN = _IUsuarioRepository.DamePorOID (id);
        return usuarioEN;
}

public System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> DameArticulosComprados (int p_idUser)
{
        return _IUsuarioRepository.DameArticulosComprados (p_idUser);
}
public void AnyadirArticulo (int p_Usuario_OID, System.Collections.Generic.IList<int> p_articulo_OIDs)
{
        //Call to UsuarioRepository

        _IUsuarioRepository.AnyadirArticulo (p_Usuario_OID, p_articulo_OIDs);
}
public void QuitarArticulo (int p_Usuario_OID, System.Collections.Generic.IList<int> p_articulo_OIDs)
{
        //Call to UsuarioRepository

        _IUsuarioRepository.QuitarArticulo (p_Usuario_OID, p_articulo_OIDs);
}
public System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN> DamePorNombre (string p_nombre)
{
        return _IUsuarioRepository.DamePorNombre (p_nombre);
}
public System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.MetodoPagoEN> DameMetodosDePago (int p_idUser)
{
        return _IUsuarioRepository.DameMetodosDePago (p_idUser);
}



private string Encode (string nombre, int id)
{
        var payload = new Dictionary<string, object>(){
                { "nombre", nombre }, { "id", id }
        };
        string token = Jose.JWT.Encode (payload, Utils.Util.getKey (), Jose.JwsAlgorithm.HS256);

        return token;
}

public string GetToken (int id)
{
        UsuarioEN en = _IUsuarioRepository.ReadOIDDefault (id);
        string token = Encode (en.Nombre, en.Id);

        return token;
}
public int CheckToken (string token)
{
        int result = -1;

        try
        {
                string decodedToken = Utils.Util.Decode (token);



                int id = (int)ObtenerID (decodedToken);

                UsuarioEN en = _IUsuarioRepository.ReadOIDDefault (id);

                if (en != null && ((long)en.Id).Equals (ObtenerID (decodedToken))
                    && ((string)en.Nombre).Equals (ObtenerNOMBRE (decodedToken))) {
                        result = id;
                }
                else throw new ModelException ("El token es incorrecto");
        } catch (Exception)
        {
                throw new ModelException ("El token es incorrecto");
        }

        return result;
}


public string ObtenerNOMBRE (string decodedToken)
{
        try
        {
                Dictionary<string, object> results = JsonConvert.DeserializeObject<Dictionary<string, object> >(decodedToken);
                string nombre = (string)results ["nombre"];
                return nombre;
        }
        catch
        {
                throw new Exception ("El token enviado no es correcto");
        }
}

public long ObtenerID (string decodedToken)
{
        try
        {
                Dictionary<string, object> results = JsonConvert.DeserializeObject<Dictionary<string, object> >(decodedToken);
                long id = (long)results ["id"];
                return id;
        }
        catch
        {
                throw new Exception ("El token enviado no es correcto");
        }
}
}
}
