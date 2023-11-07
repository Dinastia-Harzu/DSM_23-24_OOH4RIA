
using System;
// Definición clase ArticuloEN
namespace NemesisNevulaGen.ApplicationCore.EN.NemesisNevula
{
public partial class ArticuloEN
{
/**
 *	Atributo id
 */
private int id;



/**
 *	Atributo nombre
 */
private string nombre;



/**
 *	Atributo descripcion
 */
private string descripcion;



/**
 *	Atributo precio
 */
private float precio;



/**
 *	Atributo fotografia
 */
private string fotografia;



/**
 *	Atributo rareza
 */
private NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.RarezaArticuloEnum rareza;



/**
 *	Atributo tipo
 */
private NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoArticuloEnum tipo;



/**
 *	Atributo valoracion
 */
private int valoracion;



/**
 *	Atributo usuarioFavs
 */
private System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN> usuarioFavs;



/**
 *	Atributo esPublicado
 */
private bool esPublicado;



/**
 *	Atributo compraArticulo
 */
private System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> compraArticulo;



/**
 *	Atributo usuarioPropietario
 */
private System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN> usuarioPropietario;



/**
 *	Atributo administrador
 */
private System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.AdministradorEN> administrador;



/**
 *	Atributo fechaPublicacion
 */
private Nullable<DateTime> fechaPublicacion;



/**
 *	Atributo valoracionArticulo
 */
private System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ValoracionArticuloEN> valoracionArticulo;






public virtual int Id {
        get { return id; } set { id = value;  }
}



public virtual string Nombre {
        get { return nombre; } set { nombre = value;  }
}



public virtual string Descripcion {
        get { return descripcion; } set { descripcion = value;  }
}



public virtual float Precio {
        get { return precio; } set { precio = value;  }
}



public virtual string Fotografia {
        get { return fotografia; } set { fotografia = value;  }
}



public virtual NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.RarezaArticuloEnum Rareza {
        get { return rareza; } set { rareza = value;  }
}



public virtual NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoArticuloEnum Tipo {
        get { return tipo; } set { tipo = value;  }
}



public virtual int Valoracion {
        get { return valoracion; } set { valoracion = value;  }
}



public virtual System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN> UsuarioFavs {
        get { return usuarioFavs; } set { usuarioFavs = value;  }
}



public virtual bool EsPublicado {
        get { return esPublicado; } set { esPublicado = value;  }
}



public virtual System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> CompraArticulo {
        get { return compraArticulo; } set { compraArticulo = value;  }
}



public virtual System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN> UsuarioPropietario {
        get { return usuarioPropietario; } set { usuarioPropietario = value;  }
}



public virtual System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.AdministradorEN> Administrador {
        get { return administrador; } set { administrador = value;  }
}



public virtual Nullable<DateTime> FechaPublicacion {
        get { return fechaPublicacion; } set { fechaPublicacion = value;  }
}



public virtual System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ValoracionArticuloEN> ValoracionArticulo {
        get { return valoracionArticulo; } set { valoracionArticulo = value;  }
}





public ArticuloEN()
{
        usuarioFavs = new System.Collections.Generic.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN>();
        compraArticulo = new System.Collections.Generic.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN>();
        usuarioPropietario = new System.Collections.Generic.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN>();
        administrador = new System.Collections.Generic.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.AdministradorEN>();
        valoracionArticulo = new System.Collections.Generic.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ValoracionArticuloEN>();
}



public ArticuloEN(int id, string nombre, string descripcion, float precio, string fotografia, NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.RarezaArticuloEnum rareza, NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoArticuloEnum tipo, int valoracion, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN> usuarioFavs, bool esPublicado, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> compraArticulo, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN> usuarioPropietario, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.AdministradorEN> administrador, Nullable<DateTime> fechaPublicacion, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ValoracionArticuloEN> valoracionArticulo
                  )
{
        this.init (Id, nombre, descripcion, precio, fotografia, rareza, tipo, valoracion, usuarioFavs, esPublicado, compraArticulo, usuarioPropietario, administrador, fechaPublicacion, valoracionArticulo);
}


public ArticuloEN(ArticuloEN articulo)
{
        this.init (articulo.Id, articulo.Nombre, articulo.Descripcion, articulo.Precio, articulo.Fotografia, articulo.Rareza, articulo.Tipo, articulo.Valoracion, articulo.UsuarioFavs, articulo.EsPublicado, articulo.CompraArticulo, articulo.UsuarioPropietario, articulo.Administrador, articulo.FechaPublicacion, articulo.ValoracionArticulo);
}

private void init (int id
                   , string nombre, string descripcion, float precio, string fotografia, NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.RarezaArticuloEnum rareza, NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoArticuloEnum tipo, int valoracion, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN> usuarioFavs, bool esPublicado, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> compraArticulo, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN> usuarioPropietario, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.AdministradorEN> administrador, Nullable<DateTime> fechaPublicacion, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ValoracionArticuloEN> valoracionArticulo)
{
        this.Id = id;


        this.Nombre = nombre;

        this.Descripcion = descripcion;

        this.Precio = precio;

        this.Fotografia = fotografia;

        this.Rareza = rareza;

        this.Tipo = tipo;

        this.Valoracion = valoracion;

        this.UsuarioFavs = usuarioFavs;

        this.EsPublicado = esPublicado;

        this.CompraArticulo = compraArticulo;

        this.UsuarioPropietario = usuarioPropietario;

        this.Administrador = administrador;

        this.FechaPublicacion = fechaPublicacion;

        this.ValoracionArticulo = valoracionArticulo;
}

public override bool Equals (object obj)
{
        if (obj == null)
                return false;
        ArticuloEN t = obj as ArticuloEN;
        if (t == null)
                return false;
        if (Id.Equals (t.Id))
                return true;
        else
                return false;
}

public override int GetHashCode ()
{
        int hash = 13;

        hash += this.Id.GetHashCode ();
        return hash;
}
}
}
