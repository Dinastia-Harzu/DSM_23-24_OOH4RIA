
using System;
// Definición clase MetodoPagoEN
namespace NemesisNevulaGen.ApplicationCore.EN.NemesisNevula
{
public partial class MetodoPagoEN
{
/**
 *	Atributo id
 */
private int id;



/**
 *	Atributo usuarioPoseedor
 */
private System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN> usuarioPoseedor;



/**
 *	Atributo compraMetodoPago
 */
private System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> compraMetodoPago;






public virtual int Id {
        get { return id; } set { id = value;  }
}



public virtual System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN> UsuarioPoseedor {
        get { return usuarioPoseedor; } set { usuarioPoseedor = value;  }
}



public virtual System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> CompraMetodoPago {
        get { return compraMetodoPago; } set { compraMetodoPago = value;  }
}





public MetodoPagoEN()
{
        usuarioPoseedor = new System.Collections.Generic.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN>();
        compraMetodoPago = new System.Collections.Generic.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN>();
}



public MetodoPagoEN(int id, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN> usuarioPoseedor, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> compraMetodoPago
                    )
{
        this.init (Id, usuarioPoseedor, compraMetodoPago);
}


public MetodoPagoEN(MetodoPagoEN metodoPago)
{
        this.init (metodoPago.Id, metodoPago.UsuarioPoseedor, metodoPago.CompraMetodoPago);
}

private void init (int id
                   , System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN> usuarioPoseedor, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN> compraMetodoPago)
{
        this.Id = id;


        this.UsuarioPoseedor = usuarioPoseedor;

        this.CompraMetodoPago = compraMetodoPago;
}

public override bool Equals (object obj)
{
        if (obj == null)
                return false;
        MetodoPagoEN t = obj as MetodoPagoEN;
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
