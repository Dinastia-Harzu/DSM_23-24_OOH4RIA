

using System;
using System.Text;
using System.Collections.Generic;

using NemesisNevulaGen.ApplicationCore.Exceptions;

using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;


namespace NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula
{
/*
 *      Definition of the class TarjetaCreditoCEN
 *
 */
public partial class TarjetaCreditoCEN
{
private ITarjetaCreditoRepository _ITarjetaCreditoRepository;

public TarjetaCreditoCEN(ITarjetaCreditoRepository _ITarjetaCreditoRepository)
{
        this._ITarjetaCreditoRepository = _ITarjetaCreditoRepository;
}

public ITarjetaCreditoRepository get_ITarjetaCreditoRepository ()
{
        return this._ITarjetaCreditoRepository;
}

public int CrearTarjetaCredito (int p_usuarioPoseedor, NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoTarjetaEnum p_tipoTarjeta, string p_nombreEnTarjeta, string p_numero, Nullable<DateTime> p_fechaExpedicion, string p_codigoSeguridad)
{
        TarjetaCreditoEN tarjetaCreditoEN = null;
        int oid;

        //Initialized TarjetaCreditoEN
        tarjetaCreditoEN = new TarjetaCreditoEN ();

        if (p_usuarioPoseedor != -1) {
                // El argumento p_usuarioPoseedor -> Property usuarioPoseedor es oid = false
                // Lista de oids id
                tarjetaCreditoEN.UsuarioPoseedor = new NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN ();
                tarjetaCreditoEN.UsuarioPoseedor.Id = p_usuarioPoseedor;
        }

        tarjetaCreditoEN.TipoTarjeta = p_tipoTarjeta;

        tarjetaCreditoEN.NombreEnTarjeta = p_nombreEnTarjeta;

        tarjetaCreditoEN.Numero = p_numero;

        tarjetaCreditoEN.FechaExpedicion = p_fechaExpedicion;

        tarjetaCreditoEN.CodigoSeguridad = p_codigoSeguridad;



        oid = _ITarjetaCreditoRepository.CrearTarjetaCredito (tarjetaCreditoEN);
        return oid;
}

public void ModificarTarjetaCredito (int p_TarjetaCredito_OID, NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoTarjetaEnum p_tipoTarjeta, string p_nombreEnTarjeta, string p_numero, Nullable<DateTime> p_fechaExpedicion, string p_codigoSeguridad)
{
        TarjetaCreditoEN tarjetaCreditoEN = null;

        //Initialized TarjetaCreditoEN
        tarjetaCreditoEN = new TarjetaCreditoEN ();
        tarjetaCreditoEN.Id = p_TarjetaCredito_OID;
        tarjetaCreditoEN.TipoTarjeta = p_tipoTarjeta;
        tarjetaCreditoEN.NombreEnTarjeta = p_nombreEnTarjeta;
        tarjetaCreditoEN.Numero = p_numero;
        tarjetaCreditoEN.FechaExpedicion = p_fechaExpedicion;
        tarjetaCreditoEN.CodigoSeguridad = p_codigoSeguridad;
        //Call to TarjetaCreditoRepository

        _ITarjetaCreditoRepository.ModificarTarjetaCredito (tarjetaCreditoEN);
}

public void BorrarTarjetaCredito (int id
                                  )
{
        _ITarjetaCreditoRepository.BorrarTarjetaCredito (id);
}

public TarjetaCreditoEN DamePorOID (int id
                                    )
{
        TarjetaCreditoEN tarjetaCreditoEN = null;

        tarjetaCreditoEN = _ITarjetaCreditoRepository.DamePorOID (id);
        return tarjetaCreditoEN;
}

public System.Collections.Generic.IList<TarjetaCreditoEN> DameTodos (int first, int size)
{
        System.Collections.Generic.IList<TarjetaCreditoEN> list = null;

        list = _ITarjetaCreditoRepository.DameTodos (first, size);
        return list;
}
}
}
