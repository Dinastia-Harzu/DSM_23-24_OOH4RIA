
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

                int idAdmin = administradorcen.CrearAdmin ("Federico Alfredo", "fnaf1@gmail.com", false, "pfp.png", 12, (float)1.25, "contrasenya");
                int idUsuarioPremium = usuariopremiumcen.CrearUsuarioPremium ("Bonifacio Conejal", "fnaf2@gmail.com", true, "pfp.png", 2, (float)0.0, "pwd123", DateTime.Now.AddMonths (3));
                int idUsuario = usuariocen.CrearUsuario ("Chica Gallina", "fnaf3@gmail.com", true, "pfp.png", 2, (float)12.5, "pwd123");

                int idNoticia1 = noticiacen.CrearNoticia ("noticia 2332", false);
                int idNoticia2 = noticiacen.CrearNoticia ("noticia 32442432", true);

                int idArticulo1 = articulocen.CrearArticulo ("Guitarra espacial", "Es una guitarra", (float)5.5, "fotoart.png", NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.RarezaArticuloEnum.premium, NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoArticuloEnum.traje, 3, false, DateTime.Now, "fnaf pelicula", "prevart.png");
                int idArticulo2 = articulocen.CrearArticulo ("Pistola estelar", "Es una pistola", (float)6.9, "fotoart.png", NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.RarezaArticuloEnum.comun, NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoArticuloEnum.arma, 3, true, DateTime.Now.AddDays (1), "fnaf pelicula", "prevart.png");

                // Consultas

                IList<ArticuloEN> articulos = articulocen.FiltrarPorRareza (NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.RarezaArticuloEnum.comun);
                Console.WriteLine ("\n\nArticulos premium: ");
                foreach (ArticuloEN art in articulos) {
                        Console.WriteLine (art.Nombre);
                }

                // articulos = articulocen.FiltrarPorNombre ("gu");
                // Console.WriteLine ("\n\nArticulos nombre: ");
                // foreach (ArticuloEN art in articulos) {
                //         Console.WriteLine (art.Nombre);
                // }

                // DateTime f1 = new DateTime();
                // f1 = DateTime.Now;
                // Console.WriteLine(f1.toString());

                // articulos = articulocen.FiltrarPorFecha (DateTime.Now.toString(), DateTime.Now.AddMonths (1).toString());
                // Console.WriteLine ("\n\nArticulos fecha: ");
                // foreach (ArticuloEN art in articulos) {
                //         Console.WriteLine (art.Nombre);
                // }

                articulos = articulocen.FiltrarPorTemporada ("fnaf pelicula");
                Console.WriteLine ("\n\nArticulos temporada: ");
                foreach (ArticuloEN art in articulos) {
                        Console.WriteLine (art.Nombre);
                }

                articulos = articulocen.FiltrarPorTipo (NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula.TipoArticuloEnum.arma);
                Console.WriteLine ("\n\nArticulos tipo: ");
                foreach (ArticuloEN art in articulos) {
                        Console.WriteLine (art.Nombre);
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
