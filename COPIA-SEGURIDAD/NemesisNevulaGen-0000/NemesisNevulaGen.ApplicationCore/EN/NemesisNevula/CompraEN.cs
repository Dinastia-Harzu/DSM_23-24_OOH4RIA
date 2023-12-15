
using System;
// Definición clase CompraEN
namespace NemesisNevulaGen.ApplicationCore.EN.NemesisNevula
{
public partial class CompraEN
{
/**
 *	Atributo id
 */
private int id;



/**
 *	Atributo fecha
 */
private Nullable<DateTime> fecha;



/**
 *	Atributo usuarioComprador
 */
private NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN usuarioComprador;



/**
 *	Atributo articulo
 */
private NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN articulo;



/**
 *	Atributo precioTotal
 */
private float precioTotal;



/**
 *	Atributo fechaCaducidad
 */
private Nullable<DateTime> fechaCaducidad;



/**
 *	Atributo regalado
 */
private bool regalado;



/**
 *	Atributo usuarioRegalado
 */
private NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN usuarioRegalado;



/**
 *	Atributo usuarioReponedor
 */
private NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN usuarioReponedor;






public virtual int Id {
        get { return id; } set { id = value;  }
}



public virtual Nullable<DateTime> Fecha {
        get { return fecha; } set { fecha = value;  }
}



public virtual NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN UsuarioComprador {
        get { return usuarioComprador; } set { usuarioComprador = value;  }
}



public virtual NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN Articulo {
        get { return articulo; } set { articulo = value;  }
}



public virtual float PrecioTotal {
        get { return precioTotal; } set { precioTotal = value;  }
}



public virtual Nullable<DateTime> FechaCaducidad {
        get { return fechaCaducidad; } set { fechaCaducidad = value;  }
}



public virtual bool Regalado {
        get { return regalado; } set { regalado = value;  }
}



public virtual NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN UsuarioRegalado {
        get { return usuarioRegalado; } set { usuarioRegalado = value;  }
}



public virtual NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN UsuarioReponedor {
        get { return usuarioReponedor; } set { usuarioReponedor = value;  }
}





public CompraEN()
{
}



public CompraEN(int id, Nullable<DateTime> fecha, NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN usuarioComprador, NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN articulo, float precioTotal, Nullable<DateTime> fechaCaducidad, bool regalado, NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN usuarioRegalado, NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN usuarioReponedor
                )
{
        this.init (Id, fecha, usuarioComprador, articulo, precioTotal, fechaCaducidad, regalado, usuarioRegalado, usuarioReponedor);
}


public CompraEN(CompraEN compra)
{
        this.init (compra.Id, compra.Fecha, compra.UsuarioComprador, compra.Articulo, compra.PrecioTotal, compra.FechaCaducidad, compra.Regalado, compra.UsuarioRegalado, compra.UsuarioReponedor);
}

private void init (int id
                   , Nullable<DateTime> fecha, NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN usuarioComprador, NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN articulo, float precioTotal, Nullable<DateTime> fechaCaducidad, bool regalado, NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN usuarioRegalado, NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN usuarioReponedor)
{
        this.Id = id;


        this.Fecha = fecha;

        this.UsuarioComprador = usuarioComprador;

        this.Articulo = articulo;

        this.PrecioTotal = precioTotal;

        this.FechaCaducidad = fechaCaducidad;

        this.Regalado = regalado;

        this.UsuarioRegalado = usuarioRegalado;

        this.UsuarioReponedor = usuarioReponedor;
}

public override bool Equals (object obj)
{
        if (obj == null)
                return false;
        CompraEN t = obj as CompraEN;
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
