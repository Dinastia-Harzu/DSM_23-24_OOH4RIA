
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
 * Clase UsuarioPremium:
 *
 */

namespace NemesisNevulaGen.Infraestructure.Repository.NemesisNevula
{
public partial class UsuarioPremiumRepository : BasicRepository, IUsuarioPremiumRepository
{
public UsuarioPremiumRepository() : base ()
{
}


public UsuarioPremiumRepository(GenericSessionCP sessionAux) : base (sessionAux)
{
}


public void setSessionCP (GenericSessionCP session)
{
        sessionInside = false;
        this.session = (ISession)session.CurrentSession;
}


public UsuarioPremiumEN ReadOIDDefault (int id
                                        )
{
        UsuarioPremiumEN usuarioPremiumEN = null;

        try
        {
                SessionInitializeTransaction ();
                usuarioPremiumEN = (UsuarioPremiumEN)session.Get (typeof(UsuarioPremiumNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return usuarioPremiumEN;
}

public System.Collections.Generic.IList<UsuarioPremiumEN> ReadAllDefault (int first, int size)
{
        System.Collections.Generic.IList<UsuarioPremiumEN> result = null;
        try
        {
                using (ITransaction tx = session.BeginTransaction ())
                {
                        if (size > 0)
                                result = session.CreateCriteria (typeof(UsuarioPremiumNH)).
                                         SetFirstResult (first).SetMaxResults (size).List<UsuarioPremiumEN>();
                        else
                                result = session.CreateCriteria (typeof(UsuarioPremiumNH)).List<UsuarioPremiumEN>();
                }
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioPremiumRepository.", ex);
        }

        return result;
}

// Modify default (Update all attributes of the class)

public void ModifyDefault (UsuarioPremiumEN usuarioPremium)
{
        try
        {
                SessionInitializeTransaction ();
                UsuarioPremiumNH usuarioPremiumNH = (UsuarioPremiumNH)session.Load (typeof(UsuarioPremiumNH), usuarioPremium.Id);

                usuarioPremiumNH.FechaCaducidad = usuarioPremium.FechaCaducidad;

                session.Update (usuarioPremiumNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioPremiumRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}


public int CrearUsuarioPremium (UsuarioPremiumEN usuarioPremium)
{
        UsuarioPremiumNH usuarioPremiumNH = new UsuarioPremiumNH (usuarioPremium);

        try
        {
                SessionInitializeTransaction ();

                session.Save (usuarioPremiumNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioPremiumRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return usuarioPremiumNH.Id;
}

public void ModificarUsuarioPremium (UsuarioPremiumEN usuarioPremium)
{
        try
        {
                SessionInitializeTransaction ();
                UsuarioPremiumNH usuarioPremiumNH = (UsuarioPremiumNH)session.Load (typeof(UsuarioPremiumNH), usuarioPremium.Id);

                usuarioPremiumNH.Nombre = usuarioPremium.Nombre;


                usuarioPremiumNH.Correo = usuarioPremium.Correo;


                usuarioPremiumNH.ConGoogle = usuarioPremium.ConGoogle;


                usuarioPremiumNH.Foto_perfil = usuarioPremium.Foto_perfil;


                usuarioPremiumNH.PuntosNevula = usuarioPremium.PuntosNevula;


                usuarioPremiumNH.Cartera = usuarioPremium.Cartera;


                usuarioPremiumNH.Pass = usuarioPremium.Pass;


                usuarioPremiumNH.FechaCaducidad = usuarioPremium.FechaCaducidad;

                session.Update (usuarioPremiumNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioPremiumRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public void BorrarUsuarioPremium (int id
                                  )
{
        try
        {
                SessionInitializeTransaction ();
                UsuarioPremiumNH usuarioPremiumNH = (UsuarioPremiumNH)session.Load (typeof(UsuarioPremiumNH), id);
                session.Delete (usuarioPremiumNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioPremiumRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

//Sin e: DamePorOID
//Con e: UsuarioPremiumEN
public UsuarioPremiumEN DamePorOID (int id
                                    )
{
        UsuarioPremiumEN usuarioPremiumEN = null;

        try
        {
                SessionInitializeTransaction ();
                usuarioPremiumEN = (UsuarioPremiumEN)session.Get (typeof(UsuarioPremiumNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return usuarioPremiumEN;
}

public System.Collections.Generic.IList<UsuarioPremiumEN> DameTodos (int first, int size)
{
        System.Collections.Generic.IList<UsuarioPremiumEN> result = null;
        try
        {
                SessionInitializeTransaction ();
                if (size > 0)
                        result = session.CreateCriteria (typeof(UsuarioPremiumNH)).
                                 SetFirstResult (first).SetMaxResults (size).List<UsuarioPremiumEN>();
                else
                        result = session.CreateCriteria (typeof(UsuarioPremiumNH)).List<UsuarioPremiumEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioPremiumRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}
}
}
