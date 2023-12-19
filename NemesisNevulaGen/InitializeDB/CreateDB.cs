
/*PROTECTED REGION ID(CreateDB_imports) ENABLED START*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;
using NemesisNevulaGen.Infraestructure.Repository.NemesisNevula;
using NemesisNevulaGen.Infraestructure.CP;
using NemesisNevulaGen.ApplicationCore.Exceptions;

using NemesisNevulaGen.ApplicationCore.CP.NemesisNevula;
using NemesisNevulaGen.Infraestructure.Repository;

/*PROTECTED REGION END*/
namespace InitializeDB
{
public class CreateDB
{
public static void Create (string databaseArg, string userArg, string passArg)
{
        String database = databaseArg;
        String user = userArg;
        String pass = passArg;

        // Conex DB
        SqlConnection cnn = new SqlConnection (@"Server=(local)\sqlexpress; database=master; integrated security=yes");

        // Order T-SQL create user
        String createUser = @"IF NOT EXISTS(SELECT name FROM master.dbo.syslogins WHERE name = '" + user + @"')
            BEGIN
                CREATE LOGIN ["                                                                                                                                     + user + @"] WITH PASSWORD=N'" + pass + @"', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
            END"                                                                                                                                                                                                                                                                                    ;

        //Order delete user if exist
        String deleteDataBase = @"if exists(select * from sys.databases where name = '" + database + "') DROP DATABASE [" + database + "]";
        //Order create databas
        string createBD = "CREATE DATABASE " + database;
        //Order associate user with database
        String associatedUser = @"USE [" + database + "];CREATE USER [" + user + "] FOR LOGIN [" + user + "];USE [" + database + "];EXEC sp_addrolemember N'db_owner', N'" + user + "'";
        SqlCommand cmd = null;

        try
        {
                // Open conex
                cnn.Open ();

                //Create user in SQLSERVER
                cmd = new SqlCommand (createUser, cnn);
                cmd.ExecuteNonQuery ();

                //DELETE database if exist
                cmd = new SqlCommand (deleteDataBase, cnn);
                cmd.ExecuteNonQuery ();

                //CREATE DB
                cmd = new SqlCommand (createBD, cnn);
                cmd.ExecuteNonQuery ();

                //Associate user with db
                cmd = new SqlCommand (associatedUser, cnn);
                cmd.ExecuteNonQuery ();

                System.Console.WriteLine ("DataBase create sucessfully..");
        }
        catch (Exception)
        {
                throw;
        }
        finally
        {
                if (cnn.State == ConnectionState.Open) {
                        cnn.Close ();
                }
        }
}

public static void InitializeData ()
{
        try
        {
                // Initialising  CENs
                UsuarioRepository usuariorepository = new UsuarioRepository ();
                UsuarioCEN usuariocen = new UsuarioCEN (usuariorepository);
                UsuarioPremiumRepository usuariopremiumrepository = new UsuarioPremiumRepository ();
                UsuarioPremiumCEN usuariopremiumcen = new UsuarioPremiumCEN (usuariopremiumrepository);
                AdministradorRepository administradorrepository = new AdministradorRepository ();
                AdministradorCEN administradorcen = new AdministradorCEN (administradorrepository);
                ArticuloRepository articulorepository = new ArticuloRepository ();
                ArticuloCEN articulocen = new ArticuloCEN (articulorepository);
                CompraRepository comprarepository = new CompraRepository ();
                CompraCEN compracen = new CompraCEN (comprarepository);
                MetodoPagoRepository metodopagorepository = new MetodoPagoRepository ();
                MetodoPagoCEN metodopagocen = new MetodoPagoCEN (metodopagorepository);
                TarjetaCreditoRepository tarjetacreditorepository = new TarjetaCreditoRepository ();
                TarjetaCreditoCEN tarjetacreditocen = new TarjetaCreditoCEN (tarjetacreditorepository);
                PaypalRepository paypalrepository = new PaypalRepository ();
                PaypalCEN paypalcen = new PaypalCEN (paypalrepository);
                ValoracionArticuloRepository valoracionarticulorepository = new ValoracionArticuloRepository ();
                ValoracionArticuloCEN valoracionarticulocen = new ValoracionArticuloCEN (valoracionarticulorepository);
                NoticiaRepository noticiarepository = new NoticiaRepository ();
                NoticiaCEN noticiacen = new NoticiaCEN (noticiarepository);



                /*PROTECTED REGION ID(initializeDataMethod) ENABLED START*/

                int idAdmin = administradorcen.CrearAdmin ("Federico Alfredo", "fnaf1@gmail.com", false, "pfp1.png", 12, (float)1.25, "pwd123");
                int idUsuarioPremium = usuariopremiumcen.CrearUsuarioPremium ("Bonifacio Conejal", "fnaf2@gmail.com", true, "pfp2.png", 2, (float)0.0, "pwd123", DateTime.Now.AddMonths (3));
                int idUsuario = usuariocen.CrearUsuario ("Carmen Gallina", "fnaf3@gmail.com", true, "pfp3.png", 2, (float)12.5, "pwd123");

                int idNoticia1 = noticiacen.CrearNoticia ("noticia 2332", false, "titulo", "contactanos.jpeg");
                int idNoticia2 = noticiacen.CrearNoticia ("noticia 32442432", true, "titulo2", "fotoart.jpeg");

                String descripcion = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";

                int idArticulo1 = articulocen.CrearArticulo ("Guitarra espacial", descripcion, (float)2.5, "ge.jpeg", NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.RarezaArticuloEnum.Premium, NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoArticuloEnum.Traje, 0, false, DateTime.Now, "fnaf pelicula", "prevart.jpeg");
                int idArticulo2 = articulocen.CrearArticulo ("Pistola estelar", descripcion, (float)3.9, "pe.jpeg", NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.RarezaArticuloEnum.Comun, NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoArticuloEnum.Arma, 0, true, DateTime.Now.AddMonths (94), "fnaf pelicula", "prevart.jpeg");
                int idArticulo3 = articulocen.CrearArticulo ("Traje cósmico", descripcion, (float)3.9, "tc.jpeg", NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.RarezaArticuloEnum.Raro, NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoArticuloEnum.Traje, 0, true, DateTime.Now.AddMonths (94), "fnaf pelicula", "prevart.jpeg");
                int idArticulo4 = articulocen.CrearArticulo ("Pintada planetaria", descripcion, (float)3.9, "pp.jpeg", NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.RarezaArticuloEnum.Comun, NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoArticuloEnum.Grafitti, 0, true, DateTime.Now.AddMonths (94), "fnaf pelicula", "prevart.jpeg");
                int idArticulo5 = articulocen.CrearArticulo ("Nave alfa", descripcion, (float)3.9, "na.jpeg", NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.RarezaArticuloEnum.Legendario, NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoArticuloEnum.Nave, 0, true, DateTime.Now.AddMonths (94), "fnaf pelicula", "prevart.jpeg");

                int idCompra1 = compracen.CrearCompra (DateTime.Now, idUsuario, idArticulo1, (float)0.0, DateTime.Now.AddDays (1), false);

                // Consultas

                // Encontrar usuario

                IList<UsuarioEN> usu = usuariocen.DamePorNombre ("Chica Gallina");
                foreach (UsuarioEN ayuda in usu) {
                        Console.WriteLine ("\n\nUsuario es: " + ayuda.Nombre);
                }

                // FILTRANDO ARTICULOS

                //Read filter 1. Mostrar Articulos por Precio ascendente
                Console.WriteLine ("\n\nLa consulta de Articulos ordenados por Precio (Ascendente): ");
                IList<ArticuloEN> listaArticulosOrdenadosAsc = articulocen.OrdenarPorPrecioAsc ();

                foreach (ArticuloEN art in listaArticulosOrdenadosAsc) {
                        Console.WriteLine ("\n\nArticulo es: " + art.Nombre);
                }

                //Read filter 2. Mostrar Articulos por Precio descendente
                Console.WriteLine ("\n\nLa consulta de Articulos ordenados por Precio (Descendente): ");
                IList<ArticuloEN> listaArticulosOrdenadosDesc = articulocen.OrdenarPorPrecioDesc ();

                foreach (ArticuloEN art in listaArticulosOrdenadosDesc) {
                        Console.WriteLine ("\n\nArticulo es: " + art.Nombre);
                }

                //Read filter 3. Mostrar Articulos por Fecha ascendente
                Console.WriteLine ("\n\nLa consulta de Articulos ordenados por Fecha (Ascendente): ");
                IList<ArticuloEN> listaArticulosOrdenadosAsc2 = articulocen.OrdenarPorFechaAsc ();

                foreach (ArticuloEN art in listaArticulosOrdenadosAsc2) {
                        Console.WriteLine ("\n\nArticulo es: " + art.Nombre);
                }

                //Read filter 4. Mostrar Articulos por Fecha descendente
                Console.WriteLine ("\n\nLa consulta de Articulos ordenados por Fecha (Descendente): ");
                IList<ArticuloEN> listaArticulosOrdenadosDesc2 = articulocen.OrdenarPorFechaDesc ();

                foreach (ArticuloEN art in listaArticulosOrdenadosDesc2) {
                        Console.WriteLine ("\n\nArticulo es: " + art.Nombre);
                }

                //Read filter 5. Mostrar Articulos Publicados
                IList<ArticuloEN> listaArticulosPublicados = articulocen.MostrarArticulosPublicados ();

                Console.WriteLine ("\n\nLa consulta de Articulos Publicados: ");

                foreach (ArticuloEN art in listaArticulosPublicados) {
                        Console.WriteLine ("\n\nArticulo es: " + art.Nombre);
                }


                //Read filter 6. Mostrar Articulos Premium
                IList<ArticuloEN> articulos = articulocen.FiltrarPorRareza (NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.RarezaArticuloEnum.Comun);
                Console.WriteLine ("\n\nLa consulta de Articulos Premium: ");
                foreach (ArticuloEN art in articulos) {
                        Console.WriteLine (art.Nombre);
                }

                // Read filter 6. Mostrar Articulos Por Nombre -> Errores de implementación
                articulos = articulocen.FiltrarPorNombre ("gu");
                Console.WriteLine ("\n\nArticulos nombre: ");
                foreach (ArticuloEN art in articulos) {
                        Console.WriteLine (art.Nombre);
                }

                //Read filter 7. Mostrar Articulos Por Fecha
                articulos = articulocen.FiltrarPorFecha (DateTime.Now, DateTime.Now.AddMonths (1));
                Console.WriteLine ("\n\nLa consulta de Articulos Filtrados por fecha:: ");
                foreach (ArticuloEN art in articulos) {
                        Console.WriteLine (art.Nombre);
                }

                //Read filter 8. Mostrar Articulos Por Temporada
                articulos = articulocen.FiltrarPorTemporada ("fnaf pelicula");
                Console.WriteLine ("\n\nArticulos temporada: ");
                foreach (ArticuloEN art in articulos) {
                        Console.WriteLine (art.Nombre);
                }

                //Read filter 9. Mostrar Articulos Por Tipo
                articulos = articulocen.FiltrarPorTipo (NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoArticuloEnum.Arma);
                Console.WriteLine ("\n\nArticulos tipo: \n");
                foreach (ArticuloEN art in articulos) {
                        Console.WriteLine (art.Nombre);
                }

                // Custom 1. Publicar Articulo
                Console.Write ("\n\nPrueba a publicar artículo:" + "\n");
                articulocen.PublicarArticulo (idArticulo1);

                // Custom 2. Previsualizar Articulo
                Console.Write ("\n\nSe previsualiza el artículo..." + "\n");
                articulocen.Previsualizar (idArticulo1);

                // Custom 3. Publicar Noticia
                Console.Write ("\n\nPrueba a publicar noticia:" + "\n");
                noticiacen.PublicarNoticia (idNoticia1);

                // Custom transaction 1. Aplicar Descuento
                Console.WriteLine ("\n\nSe realiza una compra sin descuento:" + "\n");
                CompraCP compraCP = new CompraCP (new SessionCPNHibernate ());
                compraCP.AplicarDescuento (false, idUsuario, idArticulo1, (float)5.5);
                Console.WriteLine ("\n\nSe realiza una compra con descuento:" + "\n");
                compraCP.AplicarDescuento (true, idUsuario, idArticulo1, (float)5.5);

                // Custom transaction 2. Agregar fondos
                float cantidad_af = 10.00f;
                int usuario_af = usuariocen.CrearUsuario ("Arturo", "agrg11@alu.ua.es", false, "https://picsum.photos/200", 100, 25.00f, "password");
                int compra_af = compracen.CrearCompra (
                        DateTime.Now,
                        usuario_af,
                        -1,
                        cantidad_af,
                        DateTime.Now,
                        false
                        );
                UsuarioCP usuarioCP = new(new SessionCPNHibernate ());
                Console.WriteLine ("\n\nAgregando " + cantidad_af + "€ a la cuenta:\n");
                usuarioCP.AgregarFondos (usuario_af, compra_af, cantidad_af);

                // Valorar Articulo
                UsuarioCP usuarioCP2 = new(new SessionCPNHibernate ());
                Console.WriteLine ("\n\nValoracion actual del articulo " + articulocen.DamePorOID (idArticulo1).Nombre + ": " + articulocen.DamePorOID (idArticulo1).Valoracion + "\n");
                usuarioCP2.ValorarArticulo (idUsuario, idArticulo1, 3);
                usuarioCP2.ValorarArticulo (idUsuario, idArticulo1, 5);
                usuarioCP2.ValorarArticulo (idUsuario, idArticulo1, 5);
                Console.WriteLine ("\n\nValoracion nueva del articulo: " + articulocen.DamePorOID (idArticulo1).Valoracion);

                // Comprar Articulo

                UsuarioEN usuario = usuariocen.DamePorOID (idUsuario);

                Console.WriteLine ("********************");
                Console.WriteLine ("* PRUEBA DE COMPRA *");
                Console.WriteLine ("********************");


                Console.WriteLine ("\nMonedero actual del usuario: " + usuario.Cartera + "\n");

                IList<ArticuloEN> articulosUser = usuariocen.DameArticulosComprados (usuario.Id);
                Console.WriteLine ("\nLista de articulos del usuario comprador");
                Console.WriteLine ("------------------------------------------");
                foreach (ArticuloEN art in articulosUser) {
                        Console.WriteLine (art.Nombre);
                }

                Console.WriteLine ("\nComprando articulo 1...\n");
                int idCompra1a = usuarioCP.ComprarArticulo (idUsuario, idArticulo1, false);
                int idCompra2a = usuarioCP.ComprarArticulo (idUsuario, idArticulo2, false);
                Console.WriteLine ("EL ID DE LA COMPRA ES: " + idCompra1a);

                Console.WriteLine ("\nMonedero final del usuario: " + usuario.Cartera + "\n");

                articulosUser = usuariocen.DameArticulosComprados (usuario.Id);
                Console.WriteLine ("\nLista de articulos del usuario comprador");
                Console.WriteLine ("------------------------------------------");
                foreach (ArticuloEN art in articulosUser) {
                        Console.WriteLine ("Articulo: " + art.Nombre + "\n");
                }

                // Pruebas de devolución de articulos
                Console.WriteLine ("************************");
                Console.WriteLine ("* PRUEBA DE DEVOLUCION *");
                Console.WriteLine ("************************");


                Console.WriteLine ("\nMonedero actual del usuario: " + usuario.Cartera + "\n");

                articulosUser = usuariocen.DameArticulosComprados (usuario.Id);
                Console.WriteLine ("\nLista de articulos del usuario comprador");
                Console.WriteLine ("------------------------------------------");
                foreach (ArticuloEN art in articulosUser) {
                        Console.WriteLine (art.Nombre);
                }

                Console.WriteLine ("\nDevolviendo articulo 1...\n");
                Console.WriteLine ("OID de la Compra: " + idCompra1a);
                usuarioCP.DevolverArticulo (idUsuario, idCompra1a);

                Console.WriteLine ("\nMonedero final del usuario: " + usuario.Cartera + "\n");

                articulosUser = usuariocen.DameArticulosComprados (usuario.Id);
                Console.WriteLine ("\nLista de articulos del usuario comprador");
                Console.WriteLine ("------------------------------------------");
                foreach (ArticuloEN art in articulosUser) {
                        Console.WriteLine ("Articulo: " + art.Nombre + "\n");
                }

                // Prueba de regalo de articulo
                Console.WriteLine ("******************************");
                Console.WriteLine ("* PRUEBA DE REGALAR ARTICULO *");
                Console.WriteLine ("******************************");

                int idCompra2b = usuarioCP.ComprarArticulo (idUsuario, idArticulo1, false);
                int idCompra1b = usuarioCP.ComprarArticulo (idUsuario, idArticulo2, false); // El usuario 1 compra un articulo

                if (idCompra1b != -1) {
                        // Mostramos los articulos de cada uno antes de regalar nada
                        articulosUser = usuariocen.DameArticulosComprados (idUsuario);
                        Console.WriteLine ("\nLista de articulos del usuario comprador");
                        Console.WriteLine ("------------------------------------------");
                        foreach (ArticuloEN art in articulosUser) {
                                Console.WriteLine (art.Nombre);
                        }
                        Console.WriteLine ("\n");

                        articulosUser = usuariocen.DameArticulosComprados (idUsuarioPremium);
                        Console.WriteLine ("\nLista de articulos del usuario recibidor");
                        Console.WriteLine ("------------------------------------------");
                        foreach (ArticuloEN art in articulosUser) {
                                Console.WriteLine (art.Nombre);
                        }
                        Console.WriteLine ("\n");

                        // Procedemos a regalar el articulo
                        Console.WriteLine ("\nREGALANDO ARTICULO USUARIO -> USUARIO PREMIUM...\n");
                        compraCP.Regalar (idCompra1b, idUsuarioPremium);

                        // Mostramos los articulos tras el regalo
                        articulosUser = usuariocen.DameArticulosComprados (idUsuario);
                        Console.WriteLine ("\nLista de articulos del usuario comprador");
                        Console.WriteLine ("------------------------------------------");
                        foreach (ArticuloEN art in articulosUser) {
                                Console.WriteLine (art.Nombre);
                        }
                        Console.WriteLine ("\n");

                        articulosUser = usuariocen.DameArticulosComprados (idUsuarioPremium);
                        Console.WriteLine ("\nLista de articulos del usuario recibidor");
                        Console.WriteLine ("------------------------------------------");
                        foreach (ArticuloEN art in articulosUser) {
                                Console.WriteLine (art.Nombre);
                        }
                        Console.WriteLine ("\n");
                        usuario = usuariocen.DamePorOID (idUsuario);
                        Console.WriteLine ("\nMonedero actual del usuario: " + usuario.Cartera + "\n");
                }
                else{
                        Console.WriteLine ("La compra no se ha realizado correctamente");
                }

                /*PROTECTED REGION END*/
        }
        catch (Exception ex)
        {
                System.Console.WriteLine (ex.InnerException);
                throw;
        }
}
}
}
