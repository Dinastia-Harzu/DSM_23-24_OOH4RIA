

using System;
using System.Text;
using System.Collections.Generic;

using NemesisNevulaGen.ApplicationCore.Exceptions;

using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;


namespace NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula
{
/*
 *      Definition of the class AdministradorCEN
 *
 */
public partial class AdministradorCEN
{
private IAdministradorRepository _IAdministradorRepository;

public AdministradorCEN(IAdministradorRepository _IAdministradorRepository)
{
        this._IAdministradorRepository = _IAdministradorRepository;
}

public IAdministradorRepository get_IAdministradorRepository ()
{
        return this._IAdministradorRepository;
}

public int CrearAdmin (string p_nombre, string p_correo, bool p_conGoogle, string p_foto_perfil, int p_puntosNevula, float p_cartera, String p_pass)
{
        AdministradorEN administradorEN = null;
        int oid;

        //Initialized AdministradorEN
        administradorEN = new AdministradorEN ();
        administradorEN.Nombre = p_nombre;

        administradorEN.Correo = p_correo;

        administradorEN.ConGoogle = p_conGoogle;

        administradorEN.Foto_perfil = p_foto_perfil;

        administradorEN.PuntosNevula = p_puntosNevula;

        administradorEN.Cartera = p_cartera;

        administradorEN.Pass = Utils.Util.GetEncondeMD5 (p_pass);



        oid = _IAdministradorRepository.CrearAdmin (administradorEN);
        return oid;
}

public void ModificarAdmin (int p_Administrador_OID, string p_nombre, string p_correo, bool p_conGoogle, string p_foto_perfil, int p_puntosNevula, float p_cartera, String p_pass)
{
        AdministradorEN administradorEN = null;

        //Initialized AdministradorEN
        administradorEN = new AdministradorEN ();
        administradorEN.Id = p_Administrador_OID;
        administradorEN.Nombre = p_nombre;
        administradorEN.Correo = p_correo;
        administradorEN.ConGoogle = p_conGoogle;
        administradorEN.Foto_perfil = p_foto_perfil;
        administradorEN.PuntosNevula = p_puntosNevula;
        administradorEN.Cartera = p_cartera;
        administradorEN.Pass = Utils.Util.GetEncondeMD5 (p_pass);
        //Call to AdministradorRepository

        _IAdministradorRepository.ModificarAdmin (administradorEN);
}

public void BorrarAdmin (int id
                         )
{
        _IAdministradorRepository.BorrarAdmin (id);
}

public AdministradorEN DamePorOID (int id
                                   )
{
        AdministradorEN administradorEN = null;

        administradorEN = _IAdministradorRepository.DamePorOID (id);
        return administradorEN;
}

public System.Collections.Generic.IList<AdministradorEN> DameTodos (int first, int size)
{
        System.Collections.Generic.IList<AdministradorEN> list = null;

        list = _IAdministradorRepository.DameTodos (first, size);
        return list;
}
}
}
