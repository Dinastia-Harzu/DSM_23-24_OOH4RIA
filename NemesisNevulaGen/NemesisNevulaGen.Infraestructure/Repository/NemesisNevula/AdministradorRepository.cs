
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
 * Clase Administrador:
 *
 */

namespace NemesisNevulaGen.Infraestructure.Repository.NemesisNevula
{
public partial class AdministradorRepository : BasicRepository, IAdministradorRepository
{
public AdministradorRepository() : base ()
{
}


public AdministradorRepository(GenericSessionCP sessionAux) : base (sessionAux)
{
}


public void setSessionCP (GenericSessionCP session)
{
        sessionInside = false;
        this.session = (ISession)session.CurrentSession;
}


public AdministradorEN ReadOIDDefault (int id
                                       )
{
        AdministradorEN administradorEN = null;

        try
        {
                SessionInitializeTransaction ();
                administradorEN = (AdministradorEN)session.Get (typeof(AdministradorNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return administradorEN;
}

public System.Collections.Generic.IList<AdministradorEN> ReadAllDefault (int first, int size)
{
        System.Collections.Generic.IList<AdministradorEN> result = null;
        try
        {
                using (ITransaction tx = session.BeginTransaction ())
                {
                        if (size > 0)
                                result = session.CreateCriteria (typeof(AdministradorNH)).
                                         SetFirstResult (first).SetMaxResults (size).List<AdministradorEN>();
                        else
                                result = session.CreateCriteria (typeof(AdministradorNH)).List<AdministradorEN>();
                }
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in AdministradorRepository.", ex);
        }

        return result;
}

// Modify default (Update all attributes of the class)

public void ModifyDefault (AdministradorEN administrador)
{
        try
        {
                SessionInitializeTransaction ();
                AdministradorNH administradorNH = (AdministradorNH)session.Load (typeof(AdministradorNH), administrador.Id);


                session.Update (administradorNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in AdministradorRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}


public int CrearAdmin (AdministradorEN administrador)
{
        AdministradorNH administradorNH = new AdministradorNH (administrador);

        try
        {
                SessionInitializeTransaction ();

                session.Save (administradorNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in AdministradorRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return administradorNH.Id;
}

public void ModificarAdmin (AdministradorEN administrador)
{
        try
        {
                SessionInitializeTransaction ();
                AdministradorNH administradorNH = (AdministradorNH)session.Load (typeof(AdministradorNH), administrador.Id);

                administradorNH.Nombre = administrador.Nombre;


                administradorNH.Correo = administrador.Correo;


                administradorNH.ConGoogle = administrador.ConGoogle;


                administradorNH.Foto_perfil = administrador.Foto_perfil;


                administradorNH.PuntosNevula = administrador.PuntosNevula;


                administradorNH.Cartera = administrador.Cartera;


                administradorNH.Pass = administrador.Pass;

                session.Update (administradorNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in AdministradorRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public void BorrarAdmin (int id
                         )
{
        try
        {
                SessionInitializeTransaction ();
                AdministradorNH administradorNH = (AdministradorNH)session.Load (typeof(AdministradorNH), id);
                session.Delete (administradorNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in AdministradorRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
}
}
