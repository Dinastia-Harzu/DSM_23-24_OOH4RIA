
using System;
// Definición clase UsuarioEN
namespace NemesisNevulaGen.ApplicationCore.EN.NemesisNevula
{
public partial class UsuarioEN
{
/**
 *	Atributo nombre
 */
private string nombre;



/**
 *	Atributo correo
 */
private string correo;



/**
 *	Atributo conGoogle
 */
private bool conGoogle;



/**
 *	Atributo foto_perfil
 */
private string foto_perfil;



/**
 *	Atributo articulosFavs
 */
private System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> articulosFavs;



/**
 *	Atributo puntosNevula
 */
private int puntosNevula;



/**
 *	Atributo compraUsuario
 */
private System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> compraUsuario;



/**
 *	Atributo metodoPago
 */
private System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.MetodoPagoEN> metodoPago;



/**
 *	Atributo articulo
 */
private System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> articulo;



/**
 *	Atributo cartera
 */
private float cartera;



/**
 *	Atributo valoracionArticulo
 */
private System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ValoracionArticuloEN> valoracionArticulo;



/**
 *	Atributo pass
 */
private String pass;



/**
 *	Atributo id
 */
private int id;






public virtual string Nombre {
        get { return nombre; } set { nombre = value;  }
}



public virtual string Correo {
        get { return correo; } set { correo = value;  }
}



public virtual bool ConGoogle {
        get { return conGoogle; } set { conGoogle = value;  }
}



public virtual string Foto_perfil {
        get { return foto_perfil; } set { foto_perfil = value;  }
}



public virtual System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> ArticulosFavs {
        get { return articulosFavs; } set { articulosFavs = value;  }
}



public virtual int PuntosNevula {
        get { return puntosNevula; } set { puntosNevula = value;  }
}



public virtual System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> CompraUsuario {
        get { return compraUsuario; } set { compraUsuario = value;  }
}



public virtual System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.MetodoPagoEN> MetodoPago {
        get { return metodoPago; } set { metodoPago = value;  }
}



public virtual System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> Articulo {
        get { return articulo; } set { articulo = value;  }
}



public virtual float Cartera {
        get { return cartera; } set { cartera = value;  }
}



public virtual System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ValoracionArticuloEN> ValoracionArticulo {
        get { return valoracionArticulo; } set { valoracionArticulo = value;  }
}



public virtual String Pass {
        get { return pass; } set { pass = value;  }
}



public virtual int Id {
        get { return id; } set { id = value;  }
}





public UsuarioEN()
{
        articulosFavs = new System.Collections.Generic.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN>();
        compraUsuario = new System.Collections.Generic.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN>();
        metodoPago = new System.Collections.Generic.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.MetodoPagoEN>();
        articulo = new System.Collections.Generic.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN>();
        valoracionArticulo = new System.Collections.Generic.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ValoracionArticuloEN>();
}



public UsuarioEN(int id, string nombre, string correo, bool conGoogle, string foto_perfil, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> articulosFavs, int puntosNevula, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> compraUsuario, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.MetodoPagoEN> metodoPago, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> articulo, float cartera, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ValoracionArticuloEN> valoracionArticulo, String pass
                 )
{
        this.init (Id, nombre, correo, conGoogle, foto_perfil, articulosFavs, puntosNevula, compraUsuario, metodoPago, articulo, cartera, valoracionArticulo, pass);
}


public UsuarioEN(UsuarioEN usuario)
{
        this.init (usuario.Id, usuario.Nombre, usuario.Correo, usuario.ConGoogle, usuario.Foto_perfil, usuario.ArticulosFavs, usuario.PuntosNevula, usuario.CompraUsuario, usuario.MetodoPago, usuario.Articulo, usuario.Cartera, usuario.ValoracionArticulo, usuario.Pass);
}

private void init (int id
                   , string nombre, string correo, bool conGoogle, string foto_perfil, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> articulosFavs, int puntosNevula, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> compraUsuario, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.MetodoPagoEN> metodoPago, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> articulo, float cartera, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ValoracionArticuloEN> valoracionArticulo, String pass)
{
        this.Id = id;


        this.Nombre = nombre;

        this.Correo = correo;

        this.ConGoogle = conGoogle;

        this.Foto_perfil = foto_perfil;

        this.ArticulosFavs = articulosFavs;

        this.PuntosNevula = puntosNevula;

        this.CompraUsuario = compraUsuario;

        this.MetodoPago = metodoPago;

        this.Articulo = articulo;

        this.Cartera = cartera;

        this.ValoracionArticulo = valoracionArticulo;

        this.Pass = pass;
}

public override bool Equals (object obj)
{
        if (obj == null)
                return false;
        UsuarioEN t = obj as UsuarioEN;
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
