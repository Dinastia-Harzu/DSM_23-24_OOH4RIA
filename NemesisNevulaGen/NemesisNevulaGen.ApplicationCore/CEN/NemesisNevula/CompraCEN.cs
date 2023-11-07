

using System;
using System.Text;
using System.Collections.Generic;

using NemesisNevulaGen.ApplicationCore.Exceptions;

using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;


namespace NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula
{
/*
 *      Definition of the class CompraCEN
 *
 */
public partial class CompraCEN
{
private ICompraRepository _ICompraRepository;

public CompraCEN(ICompraRepository _ICompraRepository)
{
        this._ICompraRepository = _ICompraRepository;
}

public ICompraRepository get_ICompraRepository ()
{
        return this._ICompraRepository;
}

public int CrearCompra (Nullable<DateTime> p_fecha, NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoPagoEnum p_tipoPago, int p_usuarioComprador, int p_articulo, NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoTarjetaEnum p_tipoTarjeta, float p_precioTotal, Nullable<DateTime> p_fechaCaducidad, int p_metodoPagoCompra)
{
        CompraEN compraEN = null;
        int oid;

        //Initialized CompraEN
        compraEN = new CompraEN ();
        compraEN.Fecha = p_fecha;

        compraEN.TipoPago = p_tipoPago;


        if (p_usuarioComprador != -1) {
                // El argumento p_usuarioComprador -> Property usuarioComprador es oid = false
                // Lista de oids id
                compraEN.UsuarioComprador = new NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN ();
                compraEN.UsuarioComprador.Id = p_usuarioComprador;
        }


        if (p_articulo != -1) {
                // El argumento p_articulo -> Property articulo es oid = false
                // Lista de oids id
                compraEN.Articulo = new NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN ();
                compraEN.Articulo.Id = p_articulo;
        }

        compraEN.TipoTarjeta = p_tipoTarjeta;

        compraEN.PrecioTotal = p_precioTotal;

        compraEN.FechaCaducidad = p_fechaCaducidad;


        if (p_metodoPagoCompra != -1) {
                // El argumento p_metodoPagoCompra -> Property metodoPagoCompra es oid = false
                // Lista de oids id
                compraEN.MetodoPagoCompra = new NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.MetodoPagoEN ();
                compraEN.MetodoPagoCompra.Id = p_metodoPagoCompra;
        }



        oid = _ICompraRepository.CrearCompra (compraEN);
        return oid;
}

public void ModificarCompra (int p_Compra_OID, Nullable<DateTime> p_fecha, NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoPagoEnum p_tipoPago, NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoTarjetaEnum p_tipoTarjeta, float p_precioTotal, Nullable<DateTime> p_fechaCaducidad)
{
        CompraEN compraEN = null;

        //Initialized CompraEN
        compraEN = new CompraEN ();
        compraEN.Id = p_Compra_OID;
        compraEN.Fecha = p_fecha;
        compraEN.TipoPago = p_tipoPago;
        compraEN.TipoTarjeta = p_tipoTarjeta;
        compraEN.PrecioTotal = p_precioTotal;
        compraEN.FechaCaducidad = p_fechaCaducidad;
        //Call to CompraRepository

        _ICompraRepository.ModificarCompra (compraEN);
}

public void BorrarCompra (int id
                          )
{
        _ICompraRepository.BorrarCompra (id);
}
}
}