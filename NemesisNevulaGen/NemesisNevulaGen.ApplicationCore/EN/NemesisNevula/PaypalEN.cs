
using System;
// Definición clase PaypalEN
namespace NemesisNevulaGen.ApplicationCore.EN.NemesisNevula
{
public partial class PaypalEN                                                                       : NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.MetodoPagoEN


{
/**
 *	Atributo email
 */
private string email;



/**
 *	Atributo pass
 */
private String pass;






public virtual string Email {
        get { return email; } set { email = value;  }
}



public virtual String Pass {
        get { return pass; } set { pass = value;  }
}





public PaypalEN() : base ()
{
}



public PaypalEN(int id, string email, String pass
                , System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN> usuarioPoseedor, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> compraMetodoPago
                )
{
        this.init (Id, email, pass, usuarioPoseedor, compraMetodoPago);
}


public PaypalEN(PaypalEN paypal)
{
        this.init (paypal.Id, paypal.Email, paypal.Pass, paypal.UsuarioPoseedor, paypal.CompraMetodoPago);
}

private void init (int id
                   , string email, String pass, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN> usuarioPoseedor, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> compraMetodoPago)
{
        this.Id = id;


        this.Email = email;

        this.Pass = pass;

        this.UsuarioPoseedor = usuarioPoseedor;

        this.CompraMetodoPago = compraMetodoPago;
}

public override bool Equals (object obj)
{
        if (obj == null)
                return false;
        PaypalEN t = obj as PaypalEN;
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
