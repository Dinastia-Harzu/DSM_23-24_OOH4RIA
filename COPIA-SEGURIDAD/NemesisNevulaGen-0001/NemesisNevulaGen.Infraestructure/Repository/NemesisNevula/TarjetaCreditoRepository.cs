
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
 * Clase TarjetaCredito:
 *
 */

namespace NemesisNevulaGen.Infraestructure.Repository.NemesisNevula
{
public partial class TarjetaCreditoRepository : BasicRepository, ITarjetaCreditoRepository
{
public TarjetaCreditoRepository() : base ()
{
}


public TarjetaCreditoRepository(GenericSessionCP sessionAux) : base (sessionAux)
{
}


public void setSessionCP (GenericSessionCP session)
{
        sessionInside = false;
        this.session = (ISession)session.CurrentSession;
}


public TarjetaCreditoEN ReadOIDDefault (int id
                                        )
{
        TarjetaCreditoEN tarjetaCreditoEN = null;

        try
        {
                SessionInitializeTransaction ();
                tarjetaCreditoEN = (TarjetaCreditoEN)session.Get (typeof(TarjetaCreditoNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return tarjetaCreditoEN;
}

public System.Collections.Generic.IList<TarjetaCreditoEN> ReadAllDefault (int first, int size)
{
        System.Collections.Generic.IList<TarjetaCreditoEN> result = null;
        try
        {
                using (ITransaction tx = session.BeginTransaction ())
                {
                        if (size > 0)
                                result = session.CreateCriteria (typeof(TarjetaCreditoNH)).
                                         SetFirstResult (first).SetMaxResults (size).List<TarjetaCreditoEN>();
                        else
                                result = session.CreateCriteria (typeof(TarjetaCreditoNH)).List<TarjetaCreditoEN>();
                }
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in TarjetaCreditoRepository.", ex);
        }

        return result;
}

// Modify default (Update all attributes of the class)

public void ModifyDefault (TarjetaCreditoEN tarjetaCredito)
{
        try
        {
                SessionInitializeTransaction ();
                TarjetaCreditoNH tarjetaCreditoNH = (TarjetaCreditoNH)session.Load (typeof(TarjetaCreditoNH), tarjetaCredito.Id);

                tarjetaCreditoNH.TipoTarjeta = tarjetaCredito.TipoTarjeta;


                tarjetaCreditoNH.NombreEnTarjeta = tarjetaCredito.NombreEnTarjeta;


                tarjetaCreditoNH.Numero = tarjetaCredito.Numero;


                tarjetaCreditoNH.FechaExpedicion = tarjetaCredito.FechaExpedicion;


                tarjetaCreditoNH.CodigoSeguridad = tarjetaCredito.CodigoSeguridad;

                session.Update (tarjetaCreditoNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in TarjetaCreditoRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}


public int CrearTarjetaCredito (TarjetaCreditoEN tarjetaCredito)
{
        TarjetaCreditoNH tarjetaCreditoNH = new TarjetaCreditoNH (tarjetaCredito);

        try
        {
                SessionInitializeTransaction ();
                if (tarjetaCredito.UsuarioPoseedor != null) {
                        for (int i = 0; i < tarjetaCredito.UsuarioPoseedor.Count; i++) {
                                tarjetaCredito.UsuarioPoseedor [i] = (NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN)session.Load (typeof(NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN), tarjetaCredito.UsuarioPoseedor [i].Id);
                                tarjetaCredito.UsuarioPoseedor [i].MetodoPago.Add (tarjetaCreditoNH);
                        }
                }

                session.Save (tarjetaCreditoNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in TarjetaCreditoRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return tarjetaCreditoNH.Id;
}

public void ModificarTarjetaCredito (TarjetaCreditoEN tarjetaCredito)
{
        try
        {
                SessionInitializeTransaction ();
                TarjetaCreditoNH tarjetaCreditoNH = (TarjetaCreditoNH)session.Load (typeof(TarjetaCreditoNH), tarjetaCredito.Id);

                tarjetaCreditoNH.TipoTarjeta = tarjetaCredito.TipoTarjeta;


                tarjetaCreditoNH.NombreEnTarjeta = tarjetaCredito.NombreEnTarjeta;


                tarjetaCreditoNH.Numero = tarjetaCredito.Numero;


                tarjetaCreditoNH.FechaExpedicion = tarjetaCredito.FechaExpedicion;


                tarjetaCreditoNH.CodigoSeguridad = tarjetaCredito.CodigoSeguridad;

                session.Update (tarjetaCreditoNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in TarjetaCreditoRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public void BorrarTarjetaCredito (int id
                                  )
{
        try
        {
                SessionInitializeTransaction ();
                TarjetaCreditoNH tarjetaCreditoNH = (TarjetaCreditoNH)session.Load (typeof(TarjetaCreditoNH), id);
                session.Delete (tarjetaCreditoNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in TarjetaCreditoRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

//Sin e: DamePorOID
//Con e: TarjetaCreditoEN
public TarjetaCreditoEN DamePorOID (int id
                                    )
{
        TarjetaCreditoEN tarjetaCreditoEN = null;

        try
        {
                SessionInitializeTransaction ();
                tarjetaCreditoEN = (TarjetaCreditoEN)session.Get (typeof(TarjetaCreditoNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return tarjetaCreditoEN;
}

public System.Collections.Generic.IList<TarjetaCreditoEN> DameTodos (int first, int size)
{
        System.Collections.Generic.IList<TarjetaCreditoEN> result = null;
        try
        {
                SessionInitializeTransaction ();
                if (size > 0)
                        result = session.CreateCriteria (typeof(TarjetaCreditoNH)).
                                 SetFirstResult (first).SetMaxResults (size).List<TarjetaCreditoEN>();
                else
                        result = session.CreateCriteria (typeof(TarjetaCreditoNH)).List<TarjetaCreditoEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in TarjetaCreditoRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}
}
}
