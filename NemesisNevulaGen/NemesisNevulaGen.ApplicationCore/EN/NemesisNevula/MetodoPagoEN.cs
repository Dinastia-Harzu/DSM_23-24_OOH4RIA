
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
private NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN usuarioPoseedor;






public virtual int Id {
        get { return id; } set { id = value;  }
}



public virtual NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN UsuarioPoseedor {
        get { return usuarioPoseedor; } set { usuarioPoseedor = value;  }
}





public MetodoPagoEN()
{
}



public MetodoPagoEN(int id, NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN usuarioPoseedor
                    )
{
        this.init (Id, usuarioPoseedor);
}


public MetodoPagoEN(MetodoPagoEN metodoPago)
{
        this.init (metodoPago.Id, metodoPago.UsuarioPoseedor);
}

private void init (int id
                   , NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN usuarioPoseedor)
{
        this.Id = id;


        this.UsuarioPoseedor = usuarioPoseedor;
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
