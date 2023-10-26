
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
 * Clase Usuario:
 *
 */

namespace NemesisNevulaGen.Infraestructure.Repository.NemesisNevula
{
public partial class UsuarioRepository : BasicRepository, IUsuarioRepository
{
public UsuarioRepository() : base ()
{
}


public UsuarioRepository(GenericSessionCP sessionAux) : base (sessionAux)
{
}


public void setSessionCP (GenericSessionCP session)
{
        sessionInside = false;
        this.session = (ISession)session.CurrentSession;
}


public UsuarioEN ReadOIDDefault (int id
                                 )
{
        UsuarioEN usuarioEN = null;

        try
        {
                SessionInitializeTransaction ();
                usuarioEN = (UsuarioEN)session.Get (typeof(UsuarioNH), id);
                SessionCommit ();
        }

        catch (Exception) {
        }


        finally
        {
                SessionClose ();
        }

        return usuarioEN;
}

public System.Collections.Generic.IList<UsuarioEN> ReadAllDefault (int first, int size)
{
        System.Collections.Generic.IList<UsuarioEN> result = null;
        try
        {
                using (ITransaction tx = session.BeginTransaction ())
                {
                        if (size > 0)
                                result = session.CreateCriteria (typeof(UsuarioNH)).
                                         SetFirstResult (first).SetMaxResults (size).List<UsuarioEN>();
                        else
                                result = session.CreateCriteria (typeof(UsuarioNH)).List<UsuarioEN>();
                }
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioRepository.", ex);
        }

        return result;
}

// Modify default (Update all attributes of the class)

public void ModifyDefault (UsuarioEN usuario)
{
        try
        {
                SessionInitializeTransaction ();
                UsuarioNH usuarioNH = (UsuarioNH)session.Load (typeof(UsuarioNH), usuario.Id);

                usuarioNH.Nombre = usuario.Nombre;


                usuarioNH.Correo = usuario.Correo;


                usuarioNH.Password = usuario.Password;


                usuarioNH.ConGoogle = usuario.ConGoogle;


                usuarioNH.Foto_perfil = usuario.Foto_perfil;



                usuarioNH.PuntosNevula = usuario.PuntosNevula;



                session.Update (usuarioNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}


public int CrearUsuario (UsuarioEN usuario)
{
        UsuarioNH usuarioNH = new UsuarioNH (usuario);

        try
        {
                SessionInitializeTransaction ();

                session.Save (usuarioNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return usuarioNH.Id;
}

public void ModificarUsuario (UsuarioEN usuario)
{
        try
        {
                SessionInitializeTransaction ();
                UsuarioNH usuarioNH = (UsuarioNH)session.Load (typeof(UsuarioNH), usuario.Id);

                usuarioNH.Nombre = usuario.Nombre;


                usuarioNH.Correo = usuario.Correo;


                usuarioNH.Password = usuario.Password;


                usuarioNH.ConGoogle = usuario.ConGoogle;


                usuarioNH.Foto_perfil = usuario.Foto_perfil;


                usuarioNH.PuntosNevula = usuario.PuntosNevula;

                session.Update (usuarioNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public void BorrarUsuario (int id
                           )
{
        try
        {
                SessionInitializeTransaction ();
                UsuarioNH usuarioNH = (UsuarioNH)session.Load (typeof(UsuarioNH), id);
                session.Delete (usuarioNH);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

public void ComprarArticulo (int p_Usuario_OID, System.Collections.Generic.IList<int> p_compraUsuario_OIDs)
{
        NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN usuarioEN = null;
        try
        {
                SessionInitializeTransaction ();
                usuarioEN = (UsuarioEN)session.Load (typeof(UsuarioNH), p_Usuario_OID);
                NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN compraUsuarioENAux = null;
                if (usuarioEN.CompraUsuario == null) {
                        usuarioEN.CompraUsuario = new System.Collections.Generic.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN>();
                }

                foreach (int item in p_compraUsuario_OIDs) {
                        compraUsuarioENAux = new NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN ();
                        compraUsuarioENAux = (NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN)session.Load (typeof(NemesisNevulaGen.Infraestructure.EN.NemesisNevula.CompraNH), item);
                        compraUsuarioENAux.UsuarioComprador = usuarioEN;

                        usuarioEN.CompraUsuario.Add (compraUsuarioENAux);
                }


                session.Update (usuarioEN);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

public void DevolverArticulo (int p_Usuario_OID, System.Collections.Generic.IList<int> p_compraUsuario_OIDs)
{
        try
        {
                SessionInitializeTransaction ();
                NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN usuarioEN = null;
                usuarioEN = (UsuarioEN)session.Load (typeof(UsuarioNH), p_Usuario_OID);

                NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN compraUsuarioENAux = null;
                if (usuarioEN.CompraUsuario != null) {
                        foreach (int item in p_compraUsuario_OIDs) {
                                compraUsuarioENAux = (NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.CompraEN)session.Load (typeof(NemesisNevulaGen.Infraestructure.EN.NemesisNevula.CompraNH), item);
                                if (usuarioEN.CompraUsuario.Contains (compraUsuarioENAux) == true) {
                                        usuarioEN.CompraUsuario.Remove (compraUsuarioENAux);
                                        compraUsuarioENAux.UsuarioComprador = null;
                                }
                                else
                                        throw new ModelException ("The identifier " + item + " in p_compraUsuario_OIDs you are trying to unrelationer, doesn't exist in UsuarioEN");
                        }
                }

                session.Update (usuarioEN);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public void AnyadirFavorito (int p_Usuario_OID, System.Collections.Generic.IList<int> p_articulosFavs_OIDs)
{
        NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN usuarioEN = null;
        try
        {
                SessionInitializeTransaction ();
                usuarioEN = (UsuarioEN)session.Load (typeof(UsuarioNH), p_Usuario_OID);
                NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN articulosFavsENAux = null;
                if (usuarioEN.ArticulosFavs == null) {
                        usuarioEN.ArticulosFavs = new System.Collections.Generic.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN>();
                }

                foreach (int item in p_articulosFavs_OIDs) {
                        articulosFavsENAux = new NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN ();
                        articulosFavsENAux = (NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN)session.Load (typeof(NemesisNevulaGen.Infraestructure.EN.NemesisNevula.ArticuloNH), item);
                        articulosFavsENAux.UsuarioFavs.Add (usuarioEN);

                        usuarioEN.ArticulosFavs.Add (articulosFavsENAux);
                }


                session.Update (usuarioEN);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

public void QuitarFavorito (int p_Usuario_OID, System.Collections.Generic.IList<int> p_articulosFavs_OIDs)
{
        try
        {
                SessionInitializeTransaction ();
                NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN usuarioEN = null;
                usuarioEN = (UsuarioEN)session.Load (typeof(UsuarioNH), p_Usuario_OID);

                NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN articulosFavsENAux = null;
                if (usuarioEN.ArticulosFavs != null) {
                        foreach (int item in p_articulosFavs_OIDs) {
                                articulosFavsENAux = (NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN)session.Load (typeof(NemesisNevulaGen.Infraestructure.EN.NemesisNevula.ArticuloNH), item);
                                if (usuarioEN.ArticulosFavs.Contains (articulosFavsENAux) == true) {
                                        usuarioEN.ArticulosFavs.Remove (articulosFavsENAux);
                                        articulosFavsENAux.UsuarioFavs.Remove (usuarioEN);
                                }
                                else
                                        throw new ModelException ("The identifier " + item + " in p_articulosFavs_OIDs you are trying to unrelationer, doesn't exist in UsuarioEN");
                        }
                }

                session.Update (usuarioEN);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
                        throw;
                else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException ("Error in UsuarioRepository.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
}
}
