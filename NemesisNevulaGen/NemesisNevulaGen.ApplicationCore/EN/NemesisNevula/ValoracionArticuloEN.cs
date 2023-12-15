
using System;
// Definición clase ValoracionArticuloEN
namespace NemesisNevulaGen.ApplicationCore.EN.NemesisNevula
{
public partial class ValoracionArticuloEN
{
/**
 *	Atributo id
 */
private int id;



/**
 *	Atributo valoracion
 */
private int valoracion;



/**
 *	Atributo articulo
 */
private NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN articulo;



/**
 *	Atributo usuario
 */
private NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN usuario;






public virtual int Id {
        get { return id; } set { id = value;  }
}



public virtual int Valoracion {
        get { return valoracion; } set { valoracion = value;  }
}



public virtual NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN Articulo {
        get { return articulo; } set { articulo = value;  }
}



public virtual NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN Usuario {
        get { return usuario; } set { usuario = value;  }
}





public ValoracionArticuloEN()
{
}



public ValoracionArticuloEN(int id, int valoracion, NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN articulo, NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN usuario
                            )
{
        this.init (Id, valoracion, articulo, usuario);
}


public ValoracionArticuloEN(ValoracionArticuloEN valoracionArticulo)
{
        this.init (valoracionArticulo.Id, valoracionArticulo.Valoracion, valoracionArticulo.Articulo, valoracionArticulo.Usuario);
}

private void init (int id
                   , int valoracion, NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN articulo, NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN usuario)
{
        this.Id = id;


        this.Valoracion = valoracion;

        this.Articulo = articulo;

        this.Usuario = usuario;
}

public override bool Equals (object obj)
{
        if (obj == null)
                return false;
        ValoracionArticuloEN t = obj as ValoracionArticuloEN;
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
