
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
 * Clase Paypal:
 *
 */

namespace NemesisNevulaGen.Infraestructure.Repository.NemesisNevula
{
public partial class PaypalRepository : BasicRepository, IPaypalRepository
{
public PaypalRepository() : base ()
{
}


public PaypalRepository(GenericSessionCP sessionAux) : base (sessionAux)
{
}


public void setSessionCP (GenericSessionCP session)
{
        sessionInside = false;
        this.session = (ISession)session.CurrentSession;
}


public PaypalEN ReadOIDDefault (int id
                                )
{
        PaypalEN paypalEN = null;

        try
        {
                SessionInitializeTransaction ();
                paypalEN = (PaypalEN)session.Get (typeof(PaypalNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return paypalEN;
}

public System.Collections.Generic.IList<PaypalEN> ReadAllDefault (int first, int size)
{
        System.Collections.Generic.IList<PaypalEN> result = null;
        try
        {
                using (ITransaction tx = session.BeginTransaction ())
                {
                        if (size > 0)
                                result = session.CreateCriteria (typeof(PaypalNH)).
                                         SetFirstResult (first).SetMaxResults (size).List<PaypalEN>();
                        else
                                result = session.CreateCriteria (typeof(PaypalNH)).List<PaypalEN>();
                }
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in PaypalRepository.", ex);
        }

        return result;
}

// Modify default (Update all attributes of the class)

public void ModifyDefault (PaypalEN paypal)
{
        try
        {
                SessionInitializeTransaction ();
                PaypalNH paypalNH = (PaypalNH)session.Load (typeof(PaypalNH), paypal.Id);

                paypalNH.Email = paypal.Email;


                paypalNH.Pass = paypal.Pass;

                session.Update (paypalNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in PaypalRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}


public int CrearPaypal (PaypalEN paypal)
{
        PaypalNH paypalNH = new PaypalNH (paypal);

        try
        {
                SessionInitializeTransaction ();
                if (paypal.UsuarioPoseedor != null) {
                        // Argumento OID y no colección.
                        paypalNH
                        .UsuarioPoseedor = (NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN)session.Load (typeof(NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN), paypal.UsuarioPoseedor.Id);

                        paypalNH.UsuarioPoseedor.MetodoPago
                        .Add (paypalNH);
                }

                session.Save (paypalNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in PaypalRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return paypalNH.Id;
}

public void ModificarPaypal (PaypalEN paypal)
{
        try
        {
                SessionInitializeTransaction ();
                PaypalNH paypalNH = (PaypalNH)session.Load (typeof(PaypalNH), paypal.Id);

                paypalNH.Email = paypal.Email;


                paypalNH.Pass = paypal.Pass;

                session.Update (paypalNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in PaypalRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public void BorrarPaypal (int id
                          )
{
        try
        {
                SessionInitializeTransaction ();
                PaypalNH paypalNH = (PaypalNH)session.Load (typeof(PaypalNH), id);
                session.Delete (paypalNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in PaypalRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

//Sin e: DamePorOID
//Con e: PaypalEN
public PaypalEN DamePorOID (int id
                            )
{
        PaypalEN paypalEN = null;

        try
        {
                SessionInitializeTransaction ();
                paypalEN = (PaypalEN)session.Get (typeof(PaypalNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return paypalEN;
}

public System.Collections.Generic.IList<PaypalEN> DameTodos (int first, int size)
{
        System.Collections.Generic.IList<PaypalEN> result = null;
        try
        {
                SessionInitializeTransaction ();
                if (size > 0)
                        result = session.CreateCriteria (typeof(PaypalNH)).
                                 SetFirstResult (first).SetMaxResults (size).List<PaypalEN>();
                else
                        result = session.CreateCriteria (typeof(PaypalNH)).List<PaypalEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in PaypalRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}
}
}
