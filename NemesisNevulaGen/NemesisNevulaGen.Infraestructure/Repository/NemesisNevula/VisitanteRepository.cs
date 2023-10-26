
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
 * Clase Visitante:
 *
 */

namespace NemesisNevulaGen.Infraestructure.Repository.NemesisNevula
{
public partial class VisitanteRepository : BasicRepository, IVisitanteRepository
{
public VisitanteRepository() : base ()
{
}


public VisitanteRepository(GenericSessionCP sessionAux) : base (sessionAux)
{
}


public void setSessionCP (GenericSessionCP session)
{
        sessionInside = false;
        this.session = (ISession)session.CurrentSession;
}


public VisitanteEN ReadOIDDefault (int id
                                   )
{
        VisitanteEN visitanteEN = null;

        try
        {
                SessionInitializeTransaction ();
                visitanteEN = (VisitanteEN)session.Get (typeof(VisitanteNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return visitanteEN;
}

public System.Collections.Generic.IList<VisitanteEN> ReadAllDefault (int first, int size)
{
        System.Collections.Generic.IList<VisitanteEN> result = null;
        try
        {
                using (ITransaction tx = session.BeginTransaction ())
                {
                        if (size > 0)
                                result = session.CreateCriteria (typeof(VisitanteNH)).
                                         SetFirstResult (first).SetMaxResults (size).List<VisitanteEN>();
                        else
                                result = session.CreateCriteria (typeof(VisitanteNH)).List<VisitanteEN>();
                }
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in VisitanteRepository.", ex);
        }

        return result;
}

// Modify default (Update all attributes of the class)

public void ModifyDefault (VisitanteEN visitante)
{
        try
        {
                SessionInitializeTransaction ();
                VisitanteNH visitanteNH = (VisitanteNH)session.Load (typeof(VisitanteNH), visitante.Id);
                session.Update (visitanteNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in VisitanteRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}


public int CrearVisitante (VisitanteEN visitante)
{
        VisitanteNH visitanteNH = new VisitanteNH (visitante);

        try
        {
                SessionInitializeTransaction ();

                session.Save (visitanteNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in VisitanteRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return visitanteNH.Id;
}

public void ModificarVisitante (VisitanteEN visitante)
{
        try
        {
                SessionInitializeTransaction ();
                VisitanteNH visitanteNH = (VisitanteNH)session.Load (typeof(VisitanteNH), visitante.Id);
                session.Update (visitanteNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in VisitanteRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public void BorrarVisitante (int id
                             )
{
        try
        {
                SessionInitializeTransaction ();
                VisitanteNH visitanteNH = (VisitanteNH)session.Load (typeof(VisitanteNH), id);
                session.Delete (visitanteNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in VisitanteRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

//Sin e: DamePorOID
//Con e: VisitanteEN
public VisitanteEN DamePorOID (int id
                               )
{
        VisitanteEN visitanteEN = null;

        try
        {
                SessionInitializeTransaction ();
                visitanteEN = (VisitanteEN)session.Get (typeof(VisitanteNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return visitanteEN;
}

public System.Collections.Generic.IList<VisitanteEN> DameTodos (int first, int size)
{
        System.Collections.Generic.IList<VisitanteEN> result = null;
        try
        {
                SessionInitializeTransaction ();
                if (size > 0)
                        result = session.CreateCriteria (typeof(VisitanteNH)).
                                 SetFirstResult (first).SetMaxResults (size).List<VisitanteEN>();
                else
                        result = session.CreateCriteria (typeof(VisitanteNH)).List<VisitanteEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in VisitanteRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}
}
}
