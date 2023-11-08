

using System;
using System.Text;
using System.Collections.Generic;

using NemesisNevulaGen.ApplicationCore.Exceptions;

using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;


namespace NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula
{
/*
 *      Definition of the class ArticuloCEN
 *
 */
public partial class ArticuloCEN
{
private IArticuloRepository _IArticuloRepository;

public ArticuloCEN(IArticuloRepository _IArticuloRepository)
{
        this._IArticuloRepository = _IArticuloRepository;
}

public IArticuloRepository get_IArticuloRepository ()
{
        return this._IArticuloRepository;
}

public int CrearArticulo (string p_nombre, string p_descripcion, float p_precio, string p_fotografia, NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.RarezaArticuloEnum p_rareza, NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoArticuloEnum p_tipo, int p_valoracion, bool p_esPublicado, Nullable<DateTime> p_fechaPublicacion, string p_temporada, string p_previsualizacion)
{
        ArticuloEN articuloEN = null;
        int oid;

        //Initialized ArticuloEN
        articuloEN = new ArticuloEN ();
        articuloEN.Nombre = p_nombre;

        articuloEN.Descripcion = p_descripcion;

        articuloEN.Precio = p_precio;

        articuloEN.Fotografia = p_fotografia;

        articuloEN.Rareza = p_rareza;

        articuloEN.Tipo = p_tipo;

        articuloEN.Valoracion = p_valoracion;

        articuloEN.EsPublicado = p_esPublicado;

        articuloEN.FechaPublicacion = p_fechaPublicacion;

        articuloEN.Temporada = p_temporada;

        articuloEN.Previsualizacion = p_previsualizacion;



        oid = _IArticuloRepository.CrearArticulo (articuloEN);
        return oid;
}

public void ModificarArticulo (int p_Articulo_OID, string p_nombre, string p_descripcion, float p_precio, string p_fotografia, NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.RarezaArticuloEnum p_rareza, NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoArticuloEnum p_tipo, int p_valoracion, bool p_esPublicado, Nullable<DateTime> p_fechaPublicacion, string p_temporada, string p_previsualizacion)
{
        ArticuloEN articuloEN = null;

        //Initialized ArticuloEN
        articuloEN = new ArticuloEN ();
        articuloEN.Id = p_Articulo_OID;
        articuloEN.Nombre = p_nombre;
        articuloEN.Descripcion = p_descripcion;
        articuloEN.Precio = p_precio;
        articuloEN.Fotografia = p_fotografia;
        articuloEN.Rareza = p_rareza;
        articuloEN.Tipo = p_tipo;
        articuloEN.Valoracion = p_valoracion;
        articuloEN.EsPublicado = p_esPublicado;
        articuloEN.FechaPublicacion = p_fechaPublicacion;
        articuloEN.Temporada = p_temporada;
        articuloEN.Previsualizacion = p_previsualizacion;
        //Call to ArticuloRepository

        _IArticuloRepository.ModificarArticulo (articuloEN);
}

public void BorrarArticulo (int id
                            )
{
        _IArticuloRepository.BorrarArticulo (id);
}

public ArticuloEN DamePorOID (int id
                              )
{
        ArticuloEN articuloEN = null;

        articuloEN = _IArticuloRepository.DamePorOID (id);
        return articuloEN;
}

public System.Collections.Generic.IList<ArticuloEN> DameTodos (int first, int size)
{
        System.Collections.Generic.IList<ArticuloEN> list = null;

        list = _IArticuloRepository.DameTodos (first, size);
        return list;
}
public System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> FiltrarPorRango (NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.RarezaArticuloEnum ? p_rango)
{
        return _IArticuloRepository.FiltrarPorRango (p_rango);
}
public System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> FiltrarPorFecha (Nullable<DateTime> p_fecha_ini, Nullable<DateTime> p_fecha_fin)
{
        return _IArticuloRepository.FiltrarPorFecha (p_fecha_ini, p_fecha_fin);
}
public System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> FiltrarPorTemporada (string p_temp)
{
        return _IArticuloRepository.FiltrarPorTemporada (p_temp);
}
public System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> FiltrarPorTipo (NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoArticuloEnum ? p_tipo)
{
        return _IArticuloRepository.FiltrarPorTipo (p_tipo);
}
public System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> OrdenarPorPrecioDesc ()
{
        return _IArticuloRepository.OrdenarPorPrecioDesc ();
}
public System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> OrdenarPorFechaDesc ()
{
        return _IArticuloRepository.OrdenarPorFechaDesc ();
}
public System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> MostrarArticulosPublicados ()
{
        return _IArticuloRepository.MostrarArticulosPublicados ();
}
public System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> FiltrarPorNombre (string p_nombre)
{
        return _IArticuloRepository.FiltrarPorNombre (p_nombre);
}
public System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> OrdenarPorPrecioAsc ()
{
        return _IArticuloRepository.OrdenarPorPrecioAsc ();
}
public System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> OrdenarPorFechaAsc ()
{
        return _IArticuloRepository.OrdenarPorFechaAsc ();
}
}
}
