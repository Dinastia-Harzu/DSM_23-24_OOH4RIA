
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
 * Clase Noticia:
 *
 */

namespace NemesisNevulaGen.Infraestructure.Repository.NemesisNevula
{
public partial class NoticiaRepository : BasicRepository, INoticiaRepository
{
public NoticiaRepository() : base ()
{
}


public NoticiaRepository(GenericSessionCP sessionAux) : base (sessionAux)
{
}


public void setSessionCP (GenericSessionCP session)
{
        sessionInside = false;
        this.session = (ISession)session.CurrentSession;
}


public NoticiaEN ReadOIDDefault (int id
                                 )
{
        NoticiaEN noticiaEN = null;

        try
        {
                SessionInitializeTransaction ();
                noticiaEN = (NoticiaEN)session.Get (typeof(NoticiaNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return noticiaEN;
}

public System.Collections.Generic.IList<NoticiaEN> ReadAllDefault (int first, int size)
{
        System.Collections.Generic.IList<NoticiaEN> result = null;
        try
        {
                using (ITransaction tx = session.BeginTransaction ())
                {
                        if (size > 0)
                                result = session.CreateCriteria (typeof(NoticiaNH)).
                                         SetFirstResult (first).SetMaxResults (size).List<NoticiaEN>();
                        else
                                result = session.CreateCriteria (typeof(NoticiaNH)).List<NoticiaEN>();
                }
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in NoticiaRepository.", ex);
        }

        return result;
}

// Modify default (Update all attributes of the class)

public void ModifyDefault (NoticiaEN noticia)
{
        try
        {
                SessionInitializeTransaction ();
                NoticiaNH noticiaNH = (NoticiaNH)session.Load (typeof(NoticiaNH), noticia.Id);

                noticiaNH.Descripcion = noticia.Descripcion;


                noticiaNH.EsPublicada = noticia.EsPublicada;

                session.Update (noticiaNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in NoticiaRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}


public int CrearNoticia (NoticiaEN noticia)
{
        NoticiaNH noticiaNH = new NoticiaNH (noticia);

        try
        {
                SessionInitializeTransaction ();

                session.Save (noticiaNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in NoticiaRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return noticiaNH.Id;
}

public void ModificarNoticia (NoticiaEN noticia)
{
        try
        {
                SessionInitializeTransaction ();
                NoticiaNH noticiaNH = (NoticiaNH)session.Load (typeof(NoticiaNH), noticia.Id);

                noticiaNH.Descripcion = noticia.Descripcion;


                noticiaNH.EsPublicada = noticia.EsPublicada;

                session.Update (noticiaNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in NoticiaRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public void BorrarNoticia (int id
                           )
{
        try
        {
                SessionInitializeTransaction ();
                NoticiaNH noticiaNH = (NoticiaNH)session.Load (typeof(NoticiaNH), id);
                session.Delete (noticiaNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in NoticiaRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

//Sin e: DamePorOID
//Con e: NoticiaEN
public NoticiaEN DamePorOID (int id
                             )
{
        NoticiaEN noticiaEN = null;

        try
        {
                SessionInitializeTransaction ();
                noticiaEN = (NoticiaEN)session.Get (typeof(NoticiaNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return noticiaEN;
}

public System.Collections.Generic.IList<NoticiaEN> DameTodos (int first, int size)
{
        System.Collections.Generic.IList<NoticiaEN> result = null;
        try
        {
                SessionInitializeTransaction ();
                if (size > 0)
                        result = session.CreateCriteria (typeof(NoticiaNH)).
                                 SetFirstResult (first).SetMaxResults (size).List<NoticiaEN>();
                else
                        result = session.CreateCriteria (typeof(NoticiaNH)).List<NoticiaEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in NoticiaRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}
}
}
