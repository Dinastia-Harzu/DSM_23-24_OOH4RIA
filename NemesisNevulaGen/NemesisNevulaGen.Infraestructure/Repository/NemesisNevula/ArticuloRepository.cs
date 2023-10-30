
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
 * Clase Articulo:
 *
 */

namespace NemesisNevulaGen.Infraestructure.Repository.NemesisNevula
{
public partial class ArticuloRepository : BasicRepository, IArticuloRepository
{
public ArticuloRepository() : base ()
{
}


public ArticuloRepository(GenericSessionCP sessionAux) : base (sessionAux)
{
}


public void setSessionCP (GenericSessionCP session)
{
        sessionInside = false;
        this.session = (ISession)session.CurrentSession;
}


public ArticuloEN ReadOIDDefault (int id
                                  )
{
        ArticuloEN articuloEN = null;

        try
        {
                SessionInitializeTransaction ();
                articuloEN = (ArticuloEN)session.Get (typeof(ArticuloNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return articuloEN;
}

public System.Collections.Generic.IList<ArticuloEN> ReadAllDefault (int first, int size)
{
        System.Collections.Generic.IList<ArticuloEN> result = null;
        try
        {
                using (ITransaction tx = session.BeginTransaction ())
                {
                        if (size > 0)
                                result = session.CreateCriteria (typeof(ArticuloNH)).
                                         SetFirstResult (first).SetMaxResults (size).List<ArticuloEN>();
                        else
                                result = session.CreateCriteria (typeof(ArticuloNH)).List<ArticuloEN>();
                }
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in ArticuloRepository.", ex);
        }

        return result;
}

// Modify default (Update all attributes of the class)

public void ModifyDefault (ArticuloEN articulo)
{
        try
        {
                SessionInitializeTransaction ();
                ArticuloNH articuloNH = (ArticuloNH)session.Load (typeof(ArticuloNH), articulo.Id);

                articuloNH.Nombre = articulo.Nombre;


                articuloNH.Descripcion = articulo.Descripcion;


                articuloNH.Precio = articulo.Precio;


                articuloNH.Fotografia = articulo.Fotografia;


                articuloNH.Rareza = articulo.Rareza;


                articuloNH.Tipo = articulo.Tipo;


                articuloNH.Valoracion = articulo.Valoracion;



                articuloNH.EsPublicado = articulo.EsPublicado;



                session.Update (articuloNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in ArticuloRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}


public int CrearArticulo (ArticuloEN articulo)
{
        ArticuloNH articuloNH = new ArticuloNH (articulo);

        try
        {
                SessionInitializeTransaction ();

                session.Save (articuloNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in ArticuloRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return articuloNH.Id;
}

public void ModificarArticulo (ArticuloEN articulo)
{
        try
        {
                SessionInitializeTransaction ();
                ArticuloNH articuloNH = (ArticuloNH)session.Load (typeof(ArticuloNH), articulo.Id);

                articuloNH.Nombre = articulo.Nombre;


                articuloNH.Descripcion = articulo.Descripcion;


                articuloNH.Precio = articulo.Precio;


                articuloNH.Fotografia = articulo.Fotografia;


                articuloNH.Rareza = articulo.Rareza;


                articuloNH.Tipo = articulo.Tipo;


                articuloNH.Valoracion = articulo.Valoracion;


                articuloNH.EsPublicado = articulo.EsPublicado;

                session.Update (articuloNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in ArticuloRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public void BorrarArticulo (int id
                            )
{
        try
        {
                SessionInitializeTransaction ();
                ArticuloNH articuloNH = (ArticuloNH)session.Load (typeof(ArticuloNH), id);
                session.Delete (articuloNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in ArticuloRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

//Sin e: DamePorOID
//Con e: ArticuloEN
public ArticuloEN DamePorOID (int id
                              )
{
        ArticuloEN articuloEN = null;

        try
        {
                SessionInitializeTransaction ();
                articuloEN = (ArticuloEN)session.Get (typeof(ArticuloNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return articuloEN;
}

public System.Collections.Generic.IList<ArticuloEN> DameTodos (int first, int size)
{
        System.Collections.Generic.IList<ArticuloEN> result = null;
        try
        {
                SessionInitializeTransaction ();
                if (size > 0)
                        result = session.CreateCriteria (typeof(ArticuloNH)).
                                 SetFirstResult (first).SetMaxResults (size).List<ArticuloEN>();
                else
                        result = session.CreateCriteria (typeof(ArticuloNH)).List<ArticuloEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in ArticuloRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}
}
}
