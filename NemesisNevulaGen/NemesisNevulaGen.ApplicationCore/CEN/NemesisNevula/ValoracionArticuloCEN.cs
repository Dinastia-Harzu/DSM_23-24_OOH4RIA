

using System;
using System.Text;
using System.Collections.Generic;

using NemesisNevulaGen.ApplicationCore.Exceptions;

using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;


namespace NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula
{
/*
 *      Definition of the class ValoracionArticuloCEN
 *
 */
public partial class ValoracionArticuloCEN
{
private IValoracionArticuloRepository _IValoracionArticuloRepository;

public ValoracionArticuloCEN(IValoracionArticuloRepository _IValoracionArticuloRepository)
{
        this._IValoracionArticuloRepository = _IValoracionArticuloRepository;
}

public IValoracionArticuloRepository get_IValoracionArticuloRepository ()
{
        return this._IValoracionArticuloRepository;
}

public int CrearValoracion (int p_valoracion, int p_articulo, int p_usuario)
{
        ValoracionArticuloEN valoracionArticuloEN = null;
        int oid;

        //Initialized ValoracionArticuloEN
        valoracionArticuloEN = new ValoracionArticuloEN ();
        valoracionArticuloEN.Valoracion = p_valoracion;


        if (p_articulo != -1) {
                // El argumento p_articulo -> Property articulo es oid = false
                // Lista de oids id
                valoracionArticuloEN.Articulo = new NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN ();
                valoracionArticuloEN.Articulo.Id = p_articulo;
        }


        if (p_usuario != -1) {
                // El argumento p_usuario -> Property usuario es oid = false
                // Lista de oids id
                valoracionArticuloEN.Usuario = new NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN ();
                valoracionArticuloEN.Usuario.Id = p_usuario;
        }



        oid = _IValoracionArticuloRepository.CrearValoracion (valoracionArticuloEN);
        return oid;
}

public void ModificarValoracion (int p_ValoracionArticulo_OID, int p_valoracion)
{
        ValoracionArticuloEN valoracionArticuloEN = null;

        //Initialized ValoracionArticuloEN
        valoracionArticuloEN = new ValoracionArticuloEN ();
        valoracionArticuloEN.Id = p_ValoracionArticulo_OID;
        valoracionArticuloEN.Valoracion = p_valoracion;
        //Call to ValoracionArticuloRepository

        _IValoracionArticuloRepository.ModificarValoracion (valoracionArticuloEN);
}

public void BorrarValoracion (int id
                              )
{
        _IValoracionArticuloRepository.BorrarValoracion (id);
}

public ValoracionArticuloEN DamePorOID (int id
                                        )
{
        ValoracionArticuloEN valoracionArticuloEN = null;

        valoracionArticuloEN = _IValoracionArticuloRepository.DamePorOID (id);
        return valoracionArticuloEN;
}

public System.Collections.Generic.IList<ValoracionArticuloEN> DameTodos (int first, int size)
{
        System.Collections.Generic.IList<ValoracionArticuloEN> list = null;

        list = _IValoracionArticuloRepository.DameTodos (first, size);
        return list;
}
}
}
