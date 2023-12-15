using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Net.Http.Headers;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.Utils;
using NemesisNevulaGen.Infraestructure.Repository.NemesisNevula;
using NemesisNevulaWeb.Assemblers;
using NemesisNevulaWeb.Models;
using NHibernate.Id.Insert;
using System.Collections.Generic;

namespace NemesisNevulaWeb.Controllers
{
    public class UsuarioController : BasicController
    {
        // GET: UsuarioController/Login
        public ActionResult Login()
        {
            if (validarToken() != -1)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: UsuarioController/Login
        [HttpPost]
        public ActionResult Login(LoginUsuarioVM login)
        {
            // Validamos el token del usuario
            int idUser = validarToken();

            if (idUser != -1)
                return RedirectToAction("Index", "Home");

            UsuarioRepository userRepo = new();
            UsuarioCEN userCEN = new(userRepo);

            IList<UsuarioEN> users = userCEN.DamePorNombre(login.Nombre);

            if (users.Count == 1)
            {
                UsuarioEN user = users[0];

                string token = userCEN.Login(user.Id, login.Pass);   // Función que devuelve token si se inicia sesión correctamente
                if (token != null)
                {
                    Response.Cookies.Append("token", token, new CookieOptions
                    {
                        Expires = DateTime.Now.AddHours(2),
                        HttpOnly = true,
                        Secure = true,
                        SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict
                    });
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "La contraseña no coinicide con la del usuario introducido");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "No existe un usuario con ese nombre");
                return View();
            }

            return View();
        }

        // GET: UsuarioController
        public ActionResult Index()
        {
            // Validamos el token del usuario
            int idUser = validarToken();

            if (idUser == -1)
                return RedirectToAction("Login", "Usuario");

            tipoUsuario();

            SessionInitialize();
            UsuarioRepository userRepo = new();
            UsuarioCEN userCEN = new(userRepo);

            IList<UsuarioEN> listEN = userCEN.DameTodos(0, -1);

            IEnumerable<UsuarioVM> listUsers = new UsuarioAssembler().ConvertirListENToViewModel(listEN).ToList();
            SessionClose();

            return View(listUsers);
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            // Validamos el token del usuario
            int idUser = validarToken();

            if (idUser == -1)
                return RedirectToAction("Login", "Usuario");

            SessionInitialize();

            UsuarioRepository userRepo = new();
            UsuarioCEN userCEN = new(userRepo);

            UsuarioEN userEN = userCEN.DamePorOID(id);
            UsuarioVM userView = new UsuarioAssembler().ConvertirENToViewModel(userEN);

            SessionClose();

            return View(userView);
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            // Validamos el token del usuario
            int idUser = validarToken();

            if (idUser == -1)
                return RedirectToAction("Index", "Home");

            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioVM user)
        {
            // Validamos el token del usuario
            int idUser = validarToken();

            if (idUser == -1)
                return RedirectToAction("Index", "Home");

            try
            {
                UsuarioRepository userRepo = new();
                UsuarioCEN userCEN = new(userRepo);

                // Vamos a comprobar que no exista ya un usuario con el nombre que nos pasan
                IList<UsuarioEN> users = userCEN.DamePorNombre(user.Nombre);
                if (users.Count() != 0)
                {
                    ModelState.AddModelError("", "Ya existe un usuario con ese nombre");
                    return View();
                }
                else
                {
                    userCEN.CrearUsuario(user.Nombre, user.Correo, user.ConGoogle, user.Foto_perfil, user.PuntosNevula, user.Cartera, user.Pass);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            if (validarToken() == -1)
            {
                return RedirectToAction("Login", "Usuario");
            }

            SessionInitialize();

            UsuarioRepository userRepo = new();
            UsuarioCEN userCEN = new(userRepo);

            UsuarioEN userEN = userCEN.DamePorOID(id);
            UsuarioVM userView = new UsuarioAssembler().ConvertirENToViewModel(userEN);

            SessionClose();
            return View(userView);
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UsuarioVM user)
        {
            // Validamos el token del usuario
            int idUser = validarToken();

            if (idUser == -1)
                return RedirectToAction("Login", "Usuario");

            try
            {
                UsuarioRepository userRepo = new();
                UsuarioCEN userCEN = new(userRepo);

                userCEN.ModificarUsuario(id, user.Nombre, user.Correo, user.ConGoogle, user.Foto_perfil, user.PuntosNevula, user.Cartera, user.Pass);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            // Validamos el token del usuario
            int idUser = validarToken();

            if (idUser == -1)
                return RedirectToAction("Login", "Usuario");

            UsuarioRepository userRepo = new();
            UsuarioCEN userCEN = new(userRepo);

            userCEN.BorrarUsuario(id);

            return RedirectToAction(nameof(Index));
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            // Validamos el token del usuario
            int idUser = validarToken();

            if (idUser == -1)
                return RedirectToAction("Login", "Usuario");

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/ArtsAdquiridos/5
        public ActionResult ArtsAdquiridos(int id, string filtroBusqueda, string ordenarPor, string filtroRareza, string filtroTipo, string filtroFechaIni, string filtroFechaFin)
        {
            // Muestra de filtros
            Console.WriteLine("\n\n\n--FILTROS--\n");
            Console.WriteLine("Barra de Búsqueda: " + filtroBusqueda + "\n");
            Console.WriteLine("Criterio de ordenacion: " + ordenarPor + "\n");
            Console.WriteLine("Rareza: "+filtroRareza+ "\n");
            Console.WriteLine("Tipo Articulo: " + filtroTipo + "\n");
            Console.WriteLine("Fecha inicio: " + filtroFechaIni + "\n");
            Console.WriteLine("Fecha final: " + filtroFechaFin + "\n");

            //Validación de usuario
            int idUser = validarToken();

            if (idUser == -1)   // token erroneo o no definido
                return RedirectToAction("Login", "Usuario");

            if (idUser != id)   // usuario de token != usuario pedido por url
                return RedirectToAction(nameof(Index)); // CAMBIAR A PAGINA ERROR 404 O AVISO

            SessionInitialize();

            UsuarioRepository userRepo = new(session);
            UsuarioCEN userCEN = new(userRepo);

            UsuarioEN user = userCEN.DamePorOID(id);
            IList<CompraEN> userCompras = user.CompraRegalo;

            // Recogemos los articulos comprados por el usuario
            IList<ArticuloEN> userArts = userCEN.DameArticulosComprados(id);

            // FILTROS:
            userArts = ordenar(userArts, ordenarPor);

            List<ArticuloEN> listaAux = new(userArts);

            listaAux = filtrarXRareza(listaAux, filtroRareza);

            listaAux = filtrarXTipo(listaAux, filtroTipo);

            listaAux = filtrarXFecha(listaAux, filtroFechaIni, filtroFechaFin);

            listaAux = filtrarXInput(listaAux, filtroBusqueda);

            userArts = listaAux;

            IEnumerable<ArticuloVM> userArtsVM = new ArticuloAssembler().ConvertirListENToViewModel(userArts).ToList();

            // Recogemos los enumerados
            Array valoresRarezaEnum = Enum.GetValues(typeof(RarezaArticuloEnum));
            Array valoresTipoArtEnum = Enum.GetValues(typeof(TipoArticuloEnum));

            // Creamos una lista de select items y se los inyectamos
            IList<SelectListItem> artRarezas = new List<SelectListItem>();
            foreach (var valor in valoresRarezaEnum)
            {
                artRarezas.Add(new SelectListItem
                {
                    Text = valor.ToString(),
                    Value = ((int)valor).ToString() 
                });
            }
            IList<SelectListItem> artTipos = new List<SelectListItem>();
            foreach (var valor in valoresTipoArtEnum)
            {
                artTipos.Add(new SelectListItem
                {
                    Text = valor.ToString(),
                    Value = ((int)valor).ToString()
                });
            }

            // Creamos un ViewData con los valores obtenidos
            ViewData["artRarezas"] = artRarezas;
            ViewData["artTipos"] = artTipos;

            SessionClose();

            return View(userArtsVM);
        }

        // GET: UsuarioController/ArtsFavoritos/5
        public ActionResult ArtsFavoritos(int id, string filtroBusqueda, string ordenarPor, string filtroRareza, string filtroTipo, string filtroFechaIni, string filtroFechaFin)
        {
            // Muestra de filtros
            Console.WriteLine("\n\n\n--FILTROS--\n");
            Console.WriteLine("Barra de Búsqueda: " + filtroBusqueda + "\n");
            Console.WriteLine("Criterio de ordenacion: " + ordenarPor + "\n");
            Console.WriteLine("Rareza: " + filtroRareza + "\n");
            Console.WriteLine("Tipo Articulo: " + filtroTipo + "\n");
            Console.WriteLine("Fecha inicio: " + filtroFechaIni + "\n");
            Console.WriteLine("Fecha final: " + filtroFechaFin + "\n");

            // Validación de usuario
            int idUser = validarToken();

            if (idUser == -1)   // token erroneo o no definido
                return RedirectToAction("Login", "Usuario");

            if (idUser != id)   // usuario de token != usuario pedido por url
                return RedirectToAction(nameof(Index)); // CAMBIAR A PAGINA ERROR 404 O AVISO

            SessionInitialize();

            // Recogemos los articulos favoritos del usuario
            UsuarioRepository userRepo = new(session);
            UsuarioCEN userCEN = new(userRepo);

            IList<ArticuloEN> userArtsFavs = userCEN.DamePorOID(id).ArticulosFavs;

            // FILTROS: 
            userArtsFavs = ordenar(userArtsFavs, ordenarPor);

            List<ArticuloEN> listaAux = new(userArtsFavs);

            listaAux = filtrarXRareza(listaAux, filtroRareza);

            listaAux = filtrarXTipo(listaAux, filtroTipo);

            listaAux = filtrarXFecha(listaAux, filtroFechaIni, filtroFechaFin);

            listaAux = filtrarXInput(listaAux, filtroBusqueda);

            userArtsFavs = listaAux;

            IEnumerable<ArticuloVM> userArtsFavsVM = new ArticuloAssembler().ConvertirListENToViewModel(userArtsFavs).ToList();

            // Recogemos los enumerados
            Array valoresRarezaEnum = Enum.GetValues(typeof(RarezaArticuloEnum));
            Array valoresTipoArtEnum = Enum.GetValues(typeof(TipoArticuloEnum));

            // Creamos una lista de select items y se los inyectamos
            IList<SelectListItem> artRarezas = new List<SelectListItem>();
            foreach (var valor in valoresRarezaEnum)
            {
                artRarezas.Add(new SelectListItem
                {
                    Text = valor.ToString(),
                    Value = ((int)valor).ToString()
                });
            }
            IList<SelectListItem> artTipos = new List<SelectListItem>();
            foreach (var valor in valoresTipoArtEnum)
            {
                artTipos.Add(new SelectListItem
                {
                    Text = valor.ToString(),
                    Value = ((int)valor).ToString()
                });
            }

            // Creamos un ViewData con los valores obtenidos
            ViewData["artRarezas"] = artRarezas;
            ViewData["artTipos"] = artTipos;

            SessionClose();

            return View(userArtsFavsVM);
        }
    }
}
