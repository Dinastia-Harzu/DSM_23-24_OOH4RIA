
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





                        articuloNH.FechaPublicacion = articulo.FechaPublicacion;



                        articuloNH.Temporada = articulo.Temporada;


                        articuloNH.Previsualizacion = articulo.Previsualizacion;

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


                        articuloNH.FechaPublicacion = articulo.FechaPublicacion;


                        articuloNH.Temporada = articulo.Temporada;


                        articuloNH.Previsualizacion = articulo.Previsualizacion;

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

        public System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> FiltrarPorRareza (NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.RarezaArticuloEnum ? p_rango)
        {
                System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> result;
                try
                {
                        SessionInitializeTransaction ();
                        //String sql = @"FROM ArticuloNH self where FROM ArticuloNH AS art WHERE art.Rareza=:p_rango AND art.EsPublicado=true";
                        //IQuery query = session.CreateQuery(sql);
                        IQuery query = (IQuery)session.GetNamedQuery ("ArticuloNHfiltrarPorRarezaHQL");
                        query.SetParameter ("p_rango", p_rango);

                        result = query.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN>();
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
        public System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> FiltrarPorFecha (Nullable<DateTime> p_fecha_ini, Nullable<DateTime> p_fecha_fin)
        {
                System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> result;
                try
                {
                        SessionInitializeTransaction ();
                        //String sql = @"FROM ArticuloNH self where FROM ArticuloNH AS art WHERE art.FechaPublicacion >= :p_fecha_ini AND art.FechaPublicacion <= :p_fecha_fin AND art.EsPublicado=true";
                        //IQuery query = session.CreateQuery(sql);
                        IQuery query = (IQuery)session.GetNamedQuery ("ArticuloNHfiltrarPorFechaHQL");
                        query.SetParameter ("p_fecha_ini", p_fecha_ini);
                        query.SetParameter ("p_fecha_fin", p_fecha_fin);

                        result = query.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN>();
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
        public System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> FiltrarPorTemporada (string p_temp)
        {
                System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> result;
                try
                {
                        SessionInitializeTransaction ();
                        //String sql = @"FROM ArticuloNH self where FROM ArticuloNH AS art WHERE art.Temporada LIKE :p_temp AND art.EsPublicado=true";
                        //IQuery query = session.CreateQuery(sql);
                        IQuery query = (IQuery)session.GetNamedQuery ("ArticuloNHfiltrarPorTemporadaHQL");
                        query.SetParameter ("p_temp", p_temp);

                        result = query.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN>();
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
        public System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> FiltrarPorTipo (NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoArticuloEnum ? p_tipo)
        {
                System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> result;
                try
                {
                        SessionInitializeTransaction ();
                        //String sql = @"FROM ArticuloNH self where FROM ArticuloNH AS art WHERE art.Tipo = :p_tipo";
                        //IQuery query = session.CreateQuery(sql);
                        IQuery query = (IQuery)session.GetNamedQuery ("ArticuloNHfiltrarPorTipoHQL");
                        query.SetParameter ("p_tipo", p_tipo);

                        result = query.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN>();
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
        public System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> OrdenarPorPrecioDesc ()
        {
                System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> result;
                try
                {
                        SessionInitializeTransaction ();
                        //String sql = @"FROM ArticuloNH self where FROM ArticuloNH AS art ORDER BY art.Precio DESC";
	                                        //IQuery query = session.CreateQuery(sql);
	                                        IQuery query = (IQuery)session.GetNamedQuery("ArticuloNHordenarPorPrecioDescHQL");
	
	                                        result= query.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN>();
							SessionCommit();
					}
					
catch (Exception ex) {
SessionRollBack();
if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
	throw;
else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException("Error in ArticuloRepository.",ex);
}

					
finally
{
	SessionClose();
}

					return result;
					}
					public System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> OrdenarPorFechaDesc()
					{
					System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> result;
					try
					{
							SessionInitializeTransaction();		
							//String sql = @"FROM ArticuloNH self where FROM ArticuloNH AS art ORDER BY art.FechaPublicacion DESC ";
	                                        //IQuery query = session.CreateQuery(sql);
	                                        IQuery query = (IQuery)session.GetNamedQuery("ArticuloNHordenarPorFechaDescHQL");
	
	                                        result= query.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN>();
							SessionCommit();
					}
					
catch (Exception ex) {
SessionRollBack();
if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
	throw;
else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException("Error in ArticuloRepository.",ex);
}

					
finally
{
	SessionClose();
}

					return result;
					}
					public System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> MostrarArticulosPublicados()
					{
					System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> result;
					try
					{
							SessionInitializeTransaction();		
							//String sql = @"FROM ArticuloNH self where FROM ArticuloNH AS art WHERE art.EsPublicado = true ";
	                                        //IQuery query = session.CreateQuery(sql);
	                                        IQuery query = (IQuery)session.GetNamedQuery("ArticuloNHmostrarArticulosPublicadosHQL");
	
	                                        result= query.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN>();
							SessionCommit();
					}
					
catch (Exception ex) {
SessionRollBack();
if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
	throw;
else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException("Error in ArticuloRepository.",ex);
}

					
finally
{
	SessionClose();
}

					return result;
					}
					public System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> FiltrarPorNombre(						string p_nombre					)
					{
					System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> result;
					try
					{
							SessionInitializeTransaction();		
							//String sql = @"FROM ArticuloNH self where FROM ArticuloNH AS art WHERE art.Nombre LIKE '%' + : p_nombre+ '%' AND art.EsPublicado = true ";
	                                        //IQuery query = session.CreateQuery(sql);
	                                        IQuery query = (IQuery)session.GetNamedQuery("ArticuloNHfiltrarPorNombreHQL");
	                                                query.SetParameter("p_nombre",p_nombre);
	
	                                        result= query.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN>();
							SessionCommit();
					}
					
catch (Exception ex) {
SessionRollBack();
if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
	throw;
else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException("Error in ArticuloRepository.",ex);
}

					
finally
{
	SessionClose();
}

					return result;
					}
					public System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> OrdenarPorPrecioAsc()
					{
					System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> result;
					try
					{
							SessionInitializeTransaction();		
							//String sql = @"FROM ArticuloNH self where FROM ArticuloNH AS art ORDER BY art.Precio ASC ";
	                                        //IQuery query = session.CreateQuery(sql);
	                                        IQuery query = (IQuery)session.GetNamedQuery("ArticuloNHordenarPorPrecioAscHQL");
	
	                                        result= query.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN>();
							SessionCommit();
					}
					
catch (Exception ex) {
SessionRollBack();
if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
	throw;
else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException("Error in ArticuloRepository.",ex);
}

					
finally
{
	SessionClose();
}

					return result;
					}
					public System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> OrdenarPorFechaAsc()
					{
					System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> result;
					try
					{
							SessionInitializeTransaction();		
							//String sql = @"FROM ArticuloNH self where FROM ArticuloNH AS art WHERE art.EsPublicado = true ORDER BY art.FechaPublicacion ASC ";
	                                        //IQuery query = session.CreateQuery(sql);
	                                        IQuery query = (IQuery)session.GetNamedQuery("ArticuloNHordenarPorFechaAscHQL");
	
	                                        result= query.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN>();
							SessionCommit();
					}
					
catch (Exception ex) {
SessionRollBack();
if (ex is NemesisNevulaGen.ApplicationCore.Exceptions.ModelException)
	throw;
else throw new NemesisNevulaGen.ApplicationCore.Exceptions.DataLayerException("Error in ArticuloRepository.",ex);
}

					
finally
{
	SessionClose();
}

					return result;
					}
				
			
		    }
		}
