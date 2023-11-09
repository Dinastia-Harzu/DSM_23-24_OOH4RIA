
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
 * Clase ValoracionArticulo:
 *
 */

namespace NemesisNevulaGen.Infraestructure.Repository.NemesisNevula
{
public partial class ValoracionArticuloRepository : BasicRepository, IValoracionArticuloRepository
{
public ValoracionArticuloRepository() : base ()
{
}


public ValoracionArticuloRepository(GenericSessionCP sessionAux) : base (sessionAux)
{
}


public void setSessionCP (GenericSessionCP session)
{
        sessionInside = false;
        this.session = (ISession)session.CurrentSession;
}


public ValoracionArticuloEN ReadOIDDefault (int id
                                            )
{
        ValoracionArticuloEN valoracionArticuloEN = null;

        try
        {
                SessionInitializeTransaction ();
                valoracionArticuloEN = (ValoracionArticuloEN)session.Get (typeof(ValoracionArticuloNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return valoracionArticuloEN;
}

public System.Collections.Generic.IList<ValoracionArticuloEN> ReadAllDefault (int first, int size)
{
        System.Collections.Generic.IList<ValoracionArticuloEN> result = null;
        try
        {
                using (ITransaction tx = session.BeginTransaction ())
                {
                        if (size > 0)
                                result = session.CreateCriteria (typeof(ValoracionArticuloNH)).
                                         SetFirstResult (first).SetMaxResults (size).List<ValoracionArticuloEN>();
                        else
                                result = session.CreateCriteria (typeof(ValoracionArticuloNH)).List<ValoracionArticuloEN>();
                }
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in ValoracionArticuloRepository.", ex);
        }

        return result;
}

// Modify default (Update all attributes of the class)

public void ModifyDefault (ValoracionArticuloEN valoracionArticulo)
{
        try
        {
                SessionInitializeTransaction ();
                ValoracionArticuloNH valoracionArticuloNH = (ValoracionArticuloNH)session.Load (typeof(ValoracionArticuloNH), valoracionArticulo.Id);

                valoracionArticuloNH.Valoracion = valoracionArticulo.Valoracion;



                session.Update (valoracionArticuloNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in ValoracionArticuloRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}


public int CrearValoracion (ValoracionArticuloEN valoracionArticulo)
{
        ValoracionArticuloNH valoracionArticuloNH = new ValoracionArticuloNH (valoracionArticulo);

        try
        {
                SessionInitializeTransaction ();
                if (valoracionArticulo.Articulo != null) {
                        // Argumento OID y no colección.
                        valoracionArticuloNH
                        .Articulo = (NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN)session.Load (typeof(NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN), valoracionArticulo.Articulo.Id);

                        valoracionArticuloNH.Articulo.ValoracionArticulo
                        .Add (valoracionArticuloNH);
                }
                if (valoracionArticulo.Usuario != null) {
                        // Argumento OID y no colección.
                        valoracionArticuloNH
                        .Usuario = (NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN)session.Load (typeof(NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN), valoracionArticulo.Usuario.Id);

                        valoracionArticuloNH.Usuario.ValoracionArticulo
                        .Add (valoracionArticuloNH);
                }

                session.Save (valoracionArticuloNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in ValoracionArticuloRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return valoracionArticuloNH.Id;
}

public void ModificarValoracion (ValoracionArticuloEN valoracionArticulo)
{
        try
        {
                SessionInitializeTransaction ();
                ValoracionArticuloNH valoracionArticuloNH = (ValoracionArticuloNH)session.Load (typeof(ValoracionArticuloNH), valoracionArticulo.Id);

                valoracionArticuloNH.Valoracion = valoracionArticulo.Valoracion;

                session.Update (valoracionArticuloNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in ValoracionArticuloRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public void BorrarValoracion (int id
                              )
{
        try
        {
                SessionInitializeTransaction ();
                ValoracionArticuloNH valoracionArticuloNH = (ValoracionArticuloNH)session.Load (typeof(ValoracionArticuloNH), id);
                session.Delete (valoracionArticuloNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in ValoracionArticuloRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

//Sin e: DamePorOID
//Con e: ValoracionArticuloEN
public ValoracionArticuloEN DamePorOID (int id
                                        )
{
        ValoracionArticuloEN valoracionArticuloEN = null;

        try
        {
                SessionInitializeTransaction ();
                valoracionArticuloEN = (ValoracionArticuloEN)session.Get (typeof(ValoracionArticuloNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return valoracionArticuloEN;
}

public System.Collections.Generic.IList<ValoracionArticuloEN> DameTodos (int first, int size)
{
        System.Collections.Generic.IList<ValoracionArticuloEN> result = null;
        try
        {
                SessionInitializeTransaction ();
                if (size > 0)
                        result = session.CreateCriteria (typeof(ValoracionArticuloNH)).
                                 SetFirstResult (first).SetMaxResults (size).List<ValoracionArticuloEN>();
                else
                        result = session.CreateCriteria (typeof(ValoracionArticuloNH)).List<ValoracionArticuloEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in ValoracionArticuloRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}
}
}
