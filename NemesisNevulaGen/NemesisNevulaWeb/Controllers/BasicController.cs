using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NemesisNevulaGen.Infraestructure.CP;
using NemesisNevulaGen.Infraestructure.Repository.NemesisNevula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISession = NHibernate.ISession;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula;
using System.Security.Claims;
using System.Configuration;

namespace NemesisNevulaWeb.Controllers
{
    public class BasicController:Controller
    {
        private ISession sessionInside;

        protected SessionCPNHibernate session;

        protected BasicController()
        {
        }

        protected void SessionInitialize()
        {
            if (session == null)
            {
                sessionInside = NHibernateHelper.OpenSession();
                session = new SessionCPNHibernate(sessionInside);
            }
        }


        protected void SessionClose()
        {
            if (session != null && sessionInside.IsOpen)
            {
                sessionInside.Close();
                sessionInside.Dispose();
                session = null;
            }
        }

        // Funciones propias:
        protected int validarToken()
        {
            int result = -1;

            string token = Request.Cookies["token"];

            if (token != null)   // Si existe la cookie token...
            {
                SessionInitialize();
                UsuarioRepository userRepo = new();
                UsuarioCEN userCEN = new(userRepo);

                try
                {
                    result = userCEN.CheckToken(token); // Token válido: ID del Usuario. Si no lo es, devuelve -1
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                SessionClose();
            }

            return result;
        }        
        
        // Funciones propias:
        protected string tipoUsuario(int id)
        {
            string rolUsuario = "Visitante";

            
            UsuarioRepository usuarioRepository = new();
            UsuarioCEN usuarioCEN = new(usuarioRepository);

            UsuarioEN usuario = usuarioCEN.DamePorOID(id);

            if (usuario is AdministradorEN) rolUsuario = "Administrador";
            else if (usuario is UsuarioPremiumEN) rolUsuario = "Premium";
            else if (usuario is UsuarioEN) rolUsuario = "Normal";

            return rolUsuario;
        }

        protected List<ArticuloEN> filtrarXFecha(List<ArticuloEN> list, string filtroFechaIni, string filtroFechaFin)
        {
            DateTime dataAux;
            if (filtroFechaIni != null)
            {
                DateTime.TryParse(filtroFechaIni, out dataAux); // Intentamos pasar la fecha a tipo DateTime
                list.RemoveAll(art => art.FechaPublicacion < dataAux);
            }
            if (filtroFechaFin != null)
            {
                DateTime.TryParse(filtroFechaFin, out dataAux); // Intentamos pasar la fecha a tipo DateTime
                list.RemoveAll(art => art.FechaPublicacion > dataAux);
            }
            return list;
        }

        protected IList<ArticuloEN> ordenar(IList<ArticuloEN> list, string ordenarPor)
        {
            if (ordenarPor != null)
            {
                switch (ordenarPor)
                {
                    case "Fecha<":  // Nuevo -> Viejo
                        list = list.OrderBy(art => art.FechaPublicacion).ToList();
                        break;

                    case "Fecha>":  // Viejo -> Nuevo
                        list = list.OrderByDescending(art => art.FechaPublicacion).ToList();
                        break;

                    case "Precio<":  // Barato -> Caro
                        list = list.OrderBy(art => art.Precio).ToList();
                        break;

                    case "Precio>":  // Caro -> Barato
                        list = list.OrderByDescending(art => art.Precio).ToList();
                        break;

                    default:
                        Console.WriteLine("Criterio de ordenación no definido");
                        break;
                }
            }

            return list;
        }
        protected List<ArticuloEN> filtrarXInput(List<ArticuloEN> list, string input)
        {
            if (input != null)
                list = list.Where(art => art.Nombre.ToLower().Contains(input.ToLower())).ToList();
            
            return list;
        }

        protected List<ArticuloEN> filtrarXTipo(List<ArticuloEN> list, string filtroTipo)
        {
            if (filtroTipo != null)
            {
                switch (filtroTipo)
                {
                    case "1":
                        list.RemoveAll(art => art.Tipo != TipoArticuloEnum.Traje);
                        break;
                    case "2":
                        list.RemoveAll(art => art.Tipo != TipoArticuloEnum.Arma);
                        break;
                    case "3":
                        list.RemoveAll(art => art.Tipo != TipoArticuloEnum.Nave);
                        break;
                    case "4":
                        list.RemoveAll(art => art.Tipo != TipoArticuloEnum.Grafitti);
                        break;
                    case "5":
                        list.RemoveAll(art => art.Tipo != TipoArticuloEnum.Animacion);
                        break;
                }
            }

            return list;
        }

        protected List<ArticuloEN> filtrarXRareza(List<ArticuloEN> list, string filtroRareza)
        {
            if (filtroRareza != null)
            {
                switch (filtroRareza)
                {
                    case "1":
                        list.RemoveAll(art => art.Rareza != RarezaArticuloEnum.Comun);
                        break;
                    case "2":
                        list.RemoveAll(art => art.Rareza != RarezaArticuloEnum.Raro);
                        break;
                    case "3":
                        list.RemoveAll(art => art.Rareza != RarezaArticuloEnum.Legendario);
                        break;
                    case "4":
                        list.RemoveAll(art => art.Rareza != RarezaArticuloEnum.Premium);
                        break;
                }
            }

            return list;
        }

        protected void actualizarEstado ()
        {
            
            UsuarioRepository usuarioRepository = new();
            UsuarioCEN usuarioCEN = new(usuarioRepository);

            
            UsuarioEN usuario = usuarioCEN.DamePorOID(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            ViewData["cartera"] = usuario.Cartera;
            ViewData["foto"] = usuario.Foto_perfil;
            ViewData["nombre"] = usuario.Nombre;
        }
    }
}


