
using System;
using System.Text;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Exceptions;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.Exceptions;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CP.NemesisNevula;
using NemesisNevulaGen.Infraestructure.EN.NemesisNevula;


/*
 * Clase Compra:
 *
 */

namespace NemesisNevulaGen.Infraestructure.Repository.NemesisNevula
{
public partial class CompraRepository : BasicRepository, ICompraRepository
{
public CompraRepository() : base ()
{
}


public CompraRepository(GenericSessionCP sessionAux) : base (sessionAux)
{
}


public void setSessionCP (GenericSessionCP session)
{
        sessionInside = false;
        this.session = (ISession)session.CurrentSession;
}


public CompraEN ReadOIDDefault (int id
                                )
{
        CompraEN compraEN = null;

        try
        {
                SessionInitializeTransaction ();
                compraEN = (CompraEN)session.Get (typeof(CompraNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return compraEN;
}

public System.Collections.Generic.IList<CompraEN> ReadAllDefault (int first, int size)
{
        System.Collections.Generic.IList<CompraEN> result = null;
        try
        {
                using (ITransaction tx = session.BeginTransaction ())
                {
                        if (size > 0)
                                result = session.CreateCriteria (typeof(CompraNH)).
                                         SetFirstResult (first).SetMaxResults (size).List<CompraEN>();
                        else
                                result = session.CreateCriteria (typeof(CompraNH)).List<CompraEN>();
                }
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in CompraRepository.", ex);
        }

        return result;
}

// Modify default (Update all attributes of the class)

public void ModifyDefault (CompraEN compra)
{
        try
        {
                SessionInitializeTransaction ();
                CompraNH compraNH = (CompraNH)session.Load (typeof(CompraNH), compra.Id);

                compraNH.Fecha = compra.Fecha;


                compraNH.TipoPago = compra.TipoPago;




                compraNH.TipoTarjeta = compra.TipoTarjeta;


                compraNH.PrecioTotal = compra.PrecioTotal;


                compraNH.FechaCaducidad = compra.FechaCaducidad;



                compraNH.Regalado = compra.Regalado;


                session.Update (compraNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in CompraRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}


public int CrearCompra (CompraEN compra)
{
        CompraNH compraNH = new CompraNH (compra);

        try
        {
                SessionInitializeTransaction ();
                if (compra.UsuarioComprador != null) {
                        // Argumento OID y no colección.
                        compraNH
                        .UsuarioComprador = (NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN)session.Load (typeof(NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN), compra.UsuarioComprador.Id);

                        compraNH.UsuarioComprador.CompraUsuario
                        .Add (compraNH);
                }
                if (compra.Articulo != null) {
                        // Argumento OID y no colección.
                        compraNH
                        .Articulo = (NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN)session.Load (typeof(NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN), compra.Articulo.Id);

                        compraNH.Articulo.CompraArticulo
                        .Add (compraNH);
                }
                if (compra.MetodoPagoCompra != null) {
                        // Argumento OID y no colección.
                        compraNH
                        .MetodoPagoCompra = (NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.MetodoPagoEN)session.Load (typeof(NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.MetodoPagoEN), compra.MetodoPagoCompra.Id);

                        compraNH.MetodoPagoCompra.CompraMetodoPago
                        .Add (compraNH);
                }

                session.Save (compraNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in CompraRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return compraNH.Id;
}

public void ModificarCompra (CompraEN compra)
{
        try
        {
                SessionInitializeTransaction ();
                CompraNH compraNH = (CompraNH)session.Load (typeof(CompraNH), compra.Id);

                compraNH.Fecha = compra.Fecha;


                compraNH.TipoPago = compra.TipoPago;


                compraNH.TipoTarjeta = compra.TipoTarjeta;


                compraNH.PrecioTotal = compra.PrecioTotal;


                compraNH.FechaCaducidad = compra.FechaCaducidad;


                compraNH.Regalado = compra.Regalado;

                session.Update (compraNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in CompraRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public void BorrarCompra (int id
                          )
{
        try
        {
                SessionInitializeTransaction ();
                CompraNH compraNH = (CompraNH)session.Load (typeof(CompraNH), id);
                session.Delete (compraNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in CompraRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

//Sin e: DamePorOID
//Con e: CompraEN
public CompraEN DamePorOID (int id
                            )
{
        CompraEN compraEN = null;

        try
        {
                SessionInitializeTransaction ();
                compraEN = (CompraEN)session.Get (typeof(CompraNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return compraEN;
}

public System.Collections.Generic.IList<CompraEN> DameTodos (int first, int size)
{
        System.Collections.Generic.IList<CompraEN> result = null;
        try
        {
                SessionInitializeTransaction ();
                if (size > 0)
                        result = session.CreateCriteria (typeof(CompraNH)).
                                 SetFirstResult (first).SetMaxResults (size).List<CompraEN>();
                else
                        result = session.CreateCriteria (typeof(CompraNH)).List<CompraEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in CompraRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}
}
}
