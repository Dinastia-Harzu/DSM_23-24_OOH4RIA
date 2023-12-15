
using System;
// Definición clase AdministradorEN
namespace NemesisNevulaGen.ApplicationCore.EN.NemesisNevula
{
public partial class AdministradorEN                                                                        : NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN


{
/**
 *	Atributo articuloGestionado
 */
private System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> articuloGestionado;



/**
 *	Atributo noticia
 */
private System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.NoticiaEN> noticia;






public virtual System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> ArticuloGestionado {
        get { return articuloGestionado; } set { articuloGestionado = value;  }
}



public virtual System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.NoticiaEN> Noticia {
        get { return noticia; } set { noticia = value;  }
}





public AdministradorEN() : base ()
{
        articuloGestionado = new System.Collections.Generic.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN>();
        noticia = new System.Collections.Generic.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.NoticiaEN>();
}



public AdministradorEN(int id, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> articuloGestionado, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.NoticiaEN> noticia
                       , string nombre, string correo, bool conGoogle, string foto_perfil, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> articulosFavs, int puntosNevula, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> compraUsuario, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.MetodoPagoEN> metodoPago, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> articulo, float cartera, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ValoracionArticuloEN> valoracionArticulo, String pass, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> compraRegalo, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> compraMonedero
                       )
{
        this.init (Id, articuloGestionado, noticia, nombre, correo, conGoogle, foto_perfil, articulosFavs, puntosNevula, compraUsuario, metodoPago, articulo, cartera, valoracionArticulo, pass, compraRegalo, compraMonedero);
}


public AdministradorEN(AdministradorEN administrador)
{
        this.init (administrador.Id, administrador.ArticuloGestionado, administrador.Noticia, administrador.Nombre, administrador.Correo, administrador.ConGoogle, administrador.Foto_perfil, administrador.ArticulosFavs, administrador.PuntosNevula, administrador.CompraUsuario, administrador.MetodoPago, administrador.Articulo, administrador.Cartera, administrador.ValoracionArticulo, administrador.Pass, administrador.CompraRegalo, administrador.CompraMonedero);
}

private void init (int id
                   , System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> articuloGestionado, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.NoticiaEN> noticia, string nombre, string correo, bool conGoogle, string foto_perfil, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> articulosFavs, int puntosNevula, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> compraUsuario, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.MetodoPagoEN> metodoPago, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> articulo, float cartera, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ValoracionArticuloEN> valoracionArticulo, String pass, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> compraRegalo, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> compraMonedero)
{
        this.Id = id;


        this.ArticuloGestionado = articuloGestionado;

        this.Noticia = noticia;

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

        this.CompraRegalo = compraRegalo;

        this.CompraMonedero = compraMonedero;
}

public override bool Equals (object obj)
{
        if (obj == null)
                return false;
        AdministradorEN t = obj as AdministradorEN;
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
