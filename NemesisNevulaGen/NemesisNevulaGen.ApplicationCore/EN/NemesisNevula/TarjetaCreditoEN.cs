
using System;
// Definición clase TarjetaCreditoEN
namespace NemesisNevulaGen.ApplicationCore.EN.NemesisNevula
{
public partial class TarjetaCreditoEN                                                                       : NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.MetodoPagoEN


{
/**
 *	Atributo tipoTarjeta
 */
private NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoTarjetaEnum tipoTarjeta;



/**
 *	Atributo nombreEnTarjeta
 */
private string nombreEnTarjeta;



/**
 *	Atributo numero
 */
private string numero;



/**
 *	Atributo fechaExpedicion
 */
private Nullable<DateTime> fechaExpedicion;



/**
 *	Atributo codigoSeguridad
 */
private string codigoSeguridad;






public virtual NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoTarjetaEnum TipoTarjeta {
        get { return tipoTarjeta; } set { tipoTarjeta = value;  }
}



public virtual string NombreEnTarjeta {
        get { return nombreEnTarjeta; } set { nombreEnTarjeta = value;  }
}



public virtual string Numero {
        get { return numero; } set { numero = value;  }
}



public virtual Nullable<DateTime> FechaExpedicion {
        get { return fechaExpedicion; } set { fechaExpedicion = value;  }
}



public virtual string CodigoSeguridad {
        get { return codigoSeguridad; } set { codigoSeguridad = value;  }
}





public TarjetaCreditoEN() : base ()
{
}



public TarjetaCreditoEN(int id, NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoTarjetaEnum tipoTarjeta, string nombreEnTarjeta, string numero, Nullable<DateTime> fechaExpedicion, string codigoSeguridad
                        , NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN usuarioPoseedor
                        )
{
        this.init (Id, tipoTarjeta, nombreEnTarjeta, numero, fechaExpedicion, codigoSeguridad, usuarioPoseedor);
}


public TarjetaCreditoEN(TarjetaCreditoEN tarjetaCredito)
{
        this.init (tarjetaCredito.Id, tarjetaCredito.TipoTarjeta, tarjetaCredito.NombreEnTarjeta, tarjetaCredito.Numero, tarjetaCredito.FechaExpedicion, tarjetaCredito.CodigoSeguridad, tarjetaCredito.UsuarioPoseedor);
}

private void init (int id
                   , NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoTarjetaEnum tipoTarjeta, string nombreEnTarjeta, string numero, Nullable<DateTime> fechaExpedicion, string codigoSeguridad, NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN usuarioPoseedor)
{
        this.Id = id;


        this.TipoTarjeta = tipoTarjeta;

        this.NombreEnTarjeta = nombreEnTarjeta;

        this.Numero = numero;

        this.FechaExpedicion = fechaExpedicion;

        this.CodigoSeguridad = codigoSeguridad;

        this.UsuarioPoseedor = usuarioPoseedor;
}

public override bool Equals (object obj)
{
        if (obj == null)
                return false;
        TarjetaCreditoEN t = obj as TarjetaCreditoEN;
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
