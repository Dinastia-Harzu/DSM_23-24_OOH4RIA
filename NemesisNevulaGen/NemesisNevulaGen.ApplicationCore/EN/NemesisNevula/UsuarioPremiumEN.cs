
using System;
// Definición clase UsuarioPremiumEN
namespace NemesisNevulaGen.ApplicationCore.EN.NemesisNevula
{
public partial class UsuarioPremiumEN                                                                       : NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN


{
/**
 *	Atributo fechaCaducidad
 */
private Nullable<DateTime> fechaCaducidad;






public virtual Nullable<DateTime> FechaCaducidad {
        get { return fechaCaducidad; } set { fechaCaducidad = value;  }
}





public UsuarioPremiumEN() : base ()
{
}



public UsuarioPremiumEN(int id, Nullable<DateTime> fechaCaducidad
                        , string nombre, string correo, bool conGoogle, string foto_perfil, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> articulosFavs, int puntosNevula, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> compraUsuario, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.MetodoPagoEN> metodoPago, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> articulo, float cartera, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ValoracionArticuloEN> valoracionArticulo, String pass, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> compraRegalo
                        )
{
        this.init (Id, fechaCaducidad, nombre, correo, conGoogle, foto_perfil, articulosFavs, puntosNevula, compraUsuario, metodoPago, articulo, cartera, valoracionArticulo, pass, compraRegalo);
}


public UsuarioPremiumEN(UsuarioPremiumEN usuarioPremium)
{
        this.init (usuarioPremium.Id, usuarioPremium.FechaCaducidad, usuarioPremium.Nombre, usuarioPremium.Correo, usuarioPremium.ConGoogle, usuarioPremium.Foto_perfil, usuarioPremium.ArticulosFavs, usuarioPremium.PuntosNevula, usuarioPremium.CompraUsuario, usuarioPremium.MetodoPago, usuarioPremium.Articulo, usuarioPremium.Cartera, usuarioPremium.ValoracionArticulo, usuarioPremium.Pass, usuarioPremium.CompraRegalo);
}

private void init (int id
                   , Nullable<DateTime> fechaCaducidad, string nombre, string correo, bool conGoogle, string foto_perfil, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> articulosFavs, int puntosNevula, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> compraUsuario, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.MetodoPagoEN> metodoPago, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> articulo, float cartera, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ValoracionArticuloEN> valoracionArticulo, String pass, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> compraRegalo)
{
        this.Id = id;


        this.FechaCaducidad = fechaCaducidad;

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
}

public override bool Equals (object obj)
{
        if (obj == null)
                return false;
        UsuarioPremiumEN t = obj as UsuarioPremiumEN;
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
