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

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace NemesisNevulaWeb.Controllers
{
    public class UsuarioController : BasicController
    {

        private readonly IWebHostEnvironment _webHost;

        public UsuarioController(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        // GET: UsuarioController/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        // POST: UsuarioController/Login
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(LoginUsuarioVM login)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            UsuarioRepository userRepo = new();
            UsuarioCEN userCEN = new(userRepo);

            IList<UsuarioEN> users = userCEN.DamePorNombre(login.Nombre);

            if (users.Count == 0)
            {
                ModelState.AddModelError("", "No existe un usuario con ese nombre");
                return View();
            }
            UsuarioEN user = users[0];

            string token = userCEN.Login(user.Id, login.Pass);   // Función que devuelve token si se inicia sesión correctamente
            if (token == null)
            {
                ModelState.AddModelError("", "La contraseña no coinicide con la del usuario introducido");
                return View();
            }

            // Recogemos el rol del usuario
            string rolUser = tipoUsuario(user.Id);

            // Creamos los campos del usuario logueado
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),   // ID del usuario
                new Claim(ClaimTypes.Name, user.Nombre),    // Nombre de usurio
                new Claim(ClaimTypes.Role, rolUser)     // Rol
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        // GET: UsuarioController
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            ViewBag.CurrentPage = "Perfil";

            SessionInitialize();
            UsuarioRepository userRepo = new();
            UsuarioCEN userCEN = new(userRepo);

            IList<UsuarioEN> listEN = userCEN.DameTodos(0, -1);

            IEnumerable<UsuarioVM> listUsers = new UsuarioAssembler().ConvertirListENToViewModel(listEN).ToList();
            SessionClose();

            return View(listUsers);
        }

        // GET: UsuarioController/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            // Validamos el token del usuario registrado
            string idUserString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string rolUser = User.FindFirstValue(ClaimTypes.Role);

            ViewBag.CurrentPage = "Perfil";

            if (idUserString != id.ToString() && rolUser != "Administrador")
                return RedirectToAction("Index", "Home");

            SessionInitialize();

            UsuarioRepository userRepo = new();
            UsuarioCEN userCEN = new(userRepo);

            UsuarioEN userEN = userCEN.DamePorOID(id);
            UsuarioVM userView = new UsuarioAssembler().ConvertirENToViewModel(userEN);

            SessionClose();

            return View(userView);
        }

        // GET: UsuarioController/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        // POST: UsuarioController/Create
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(UsuarioVM user)
        {
            // Validamos el token del usuario
            if (User.Identity.IsAuthenticated)
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
                    // Manejamos la subida de foto de perfil
                    string fileName = "", path = "";
                    if (user.Foto_perfil2 != null && user.Foto_perfil2.Length > 0)
                    {
                        fileName = Path.GetFileName(user.Foto_perfil2.FileName).Trim();

                        string directory = _webHost.WebRootPath + "/FotosPerfil";
                        path = Path.Combine(directory, fileName);

                        if (!Directory.Exists(directory))
                            Directory.CreateDirectory(directory);

                        using (var stream = System.IO.File.Create(path))
                        {
                            await user.Foto_perfil2.CopyToAsync(stream);
                        }
                    }
                    else
                    {
                        fileName = "sinFoto.png";
                    }

                    fileName = "/FotosPerfil/" + fileName;

                    userCEN.CrearUsuario(user.Nombre, user.Correo, user.ConGoogle, fileName, user.PuntosNevula, user.Cartera, user.Pass);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            string idUserString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string rolUser = User.FindFirstValue(ClaimTypes.Role);

            if (idUserString != id.ToString() && rolUser != "Administrador")
                return RedirectToAction("Index", "Home");

            ViewBag.CurrentPage = "Perfil";

            SessionInitialize();

            UsuarioRepository userRepo = new();
            UsuarioCEN userCEN = new(userRepo);

            UsuarioEN userEN = userCEN.DamePorOID(id);
            UsuarioVM userView = new UsuarioAssembler().ConvertirENToViewModel(userEN);

            SessionClose();
            return View(userView);
        }

        // POST: UsuarioController/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, UsuarioVM user)
        {
            string idUserString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string rolUser = User.FindFirstValue(ClaimTypes.Role);

            Console.WriteLine("¿Entramos?");

            if (idUserString != id.ToString() && rolUser != "Administrador")
                return RedirectToAction("Index", "Home");

            ViewBag.CurrentPage = "Perfil";

            Console.WriteLine("ENTRAMOS");

            try
            {
                UsuarioRepository userRepo = new();
                UsuarioCEN userCEN = new(userRepo);

                Console.WriteLine("Foto: " + user.Foto_perfil2);

                // Manejamos la subida de foto de perfil
                string fileName = "", path = "", miDirectorio="/FotosPerfil/";
                if (user.Foto_perfil2 != null && user.Foto_perfil2.Length > 0)
                {
                    Console.WriteLine("Foto definida");

                    fileName = Path.GetFileName(user.Foto_perfil2.FileName).Trim();

                    Console.WriteLine("Nombre foto: "+fileName);

                    string directory = _webHost.WebRootPath + "/FotosPerfil";
                    path = Path.Combine(directory, fileName);

                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    using (var stream = System.IO.File.Create(path))
                    {
                        await user.Foto_perfil2.CopyToAsync(stream);
                    }

                    fileName = miDirectorio + fileName;
                }
                else
                {
                    Console.WriteLine("NO HAY FOTO");
                    fileName = user.Foto_perfil;
                    Console.WriteLine("Nombre de foto antigua: " + fileName);
                }

                // Si no ha introducido valor para cambiar de contraseña...
                UsuarioEN userAux = userCEN.DamePorOID(id);

                string userPass = "";

                if (user.Pass == null)
                    userPass = userAux.Pass;
                else
                    userPass = user.Pass;

                Console.WriteLine("Strig imagen definitivo: " + fileName);

                userCEN.ModificarUsuario(id, user.Nombre, user.Correo, userAux.ConGoogle, fileName, userAux.PuntosNevula, userAux.Cartera, userPass);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR FATAL: "+e.Message);
                return View();
            }
        }

        // GET: UsuarioController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            string idUserString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string rolUser = User.FindFirstValue(ClaimTypes.Role);

            if (idUserString != id.ToString() && rolUser != "Administrador")
                return RedirectToAction("Index", "Home");

            UsuarioRepository userRepo = new();
            UsuarioCEN userCEN = new(userRepo);

            userCEN.BorrarUsuario(id);

            return RedirectToAction(nameof(Index));
        }

        // POST: UsuarioController/Delete/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            string idUserString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string rolUser = User.FindFirstValue(ClaimTypes.Role);

            if (idUserString != id.ToString() && rolUser != "Administrador")
                return RedirectToAction("Index", "Home");

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
        [Authorize]
        public ActionResult ArtsAdquiridos(int id, string filtroBusqueda, string ordenarPor, string filtroRareza, string filtroTipo, string filtroFechaIni, string filtroFechaFin)
        {
            // Validamos que solo el usuario propio o el administrador pueda ver los articulos adquiridos
            string idUserString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string rolUser = User.FindFirstValue(ClaimTypes.Role);

            if (idUserString != id.ToString() && rolUser != "Administrador")
                return RedirectToAction("Index", "Home");

            ViewBag.CurrentPage = "Perfil";
            // Muestra de filtros
            Console.WriteLine("\n\n\n--FILTROS--\n");
            Console.WriteLine("Barra de Búsqueda: " + filtroBusqueda + "\n");
            Console.WriteLine("Criterio de ordenacion: " + ordenarPor + "\n");
            Console.WriteLine("Rareza: " + filtroRareza + "\n");
            Console.WriteLine("Tipo Articulo: " + filtroTipo + "\n");
            Console.WriteLine("Fecha inicio: " + filtroFechaIni + "\n");
            Console.WriteLine("Fecha final: " + filtroFechaFin + "\n");

            if (idUserString != id.ToString() && rolUser != "Administrador")
                return RedirectToAction("Index", "Home");

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
        [Authorize]
        public ActionResult ArtsFavoritos(int id, string filtroBusqueda, string ordenarPor, string filtroRareza, string filtroTipo, string filtroFechaIni, string filtroFechaFin)
        {
            string idUserString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string rolUser = User.FindFirstValue(ClaimTypes.Role);

            if (idUserString != id.ToString() && rolUser != "Administrador")
                return RedirectToAction("Index", "Home");

            ViewBag.CurrentPage = "Perfil";
            // Muestra de filtros
            Console.WriteLine("\n\n\n--FILTROS--\n");
            Console.WriteLine("Barra de Búsqueda: " + filtroBusqueda + "\n");
            Console.WriteLine("Criterio de ordenacion: " + ordenarPor + "\n");
            Console.WriteLine("Rareza: " + filtroRareza + "\n");
            Console.WriteLine("Tipo Articulo: " + filtroTipo + "\n");
            Console.WriteLine("Fecha inicio: " + filtroFechaIni + "\n");
            Console.WriteLine("Fecha final: " + filtroFechaFin + "\n");

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

        // GET: UsuarioController/MetodPago
        // id de usuario
        public ActionResult MetodPago(int id)
        {
            // Validación de usuario
            string idUserString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string rolUser = User.FindFirstValue(ClaimTypes.Role);

            if (idUserString != id.ToString() && rolUser != "Administrador")
                return RedirectToAction("Index", "Home");

            SessionInitialize();
            PaypalRepository ppRepository = new PaypalRepository(session);
            PaypalCEN ppCEN = new PaypalCEN(ppRepository);

            UsuarioRepository usuarioRepository = new UsuarioRepository(session);
            UsuarioCEN userCEN = new UsuarioCEN(usuarioRepository);
            UsuarioEN user = userCEN.DamePorOID(id);

            IList<MetodoPagoEN> listMPEN = user.MetodoPago;

            List<MetodoPagoEN> listaAux = new List<MetodoPagoEN>(listMPEN);

            listaAux.RemoveAll(mp => mp is not PaypalEN);

            List<PaypalEN> ppList = new List<PaypalEN>();

            foreach (var pp in listaAux)
            {
                ppList.Add((PaypalEN) pp);
            }

            IEnumerable<PaypalVM> listPP = new PaypalAssembler().ConvertirListENToViewModel(ppList).ToList();

            TarjetaCreditoRepository tjRepository = new TarjetaCreditoRepository(session);
            TarjetaCreditoCEN tcCEN = new TarjetaCreditoCEN(tjRepository);

            listaAux = new List<MetodoPagoEN>(listMPEN);

            listaAux.RemoveAll(mp => mp is not TarjetaCreditoEN);

            List<TarjetaCreditoEN> tcList = new List<TarjetaCreditoEN>();

            foreach (var pp in listaAux)
            {
                tcList.Add((TarjetaCreditoEN) pp);
            }

            IEnumerable<TarjetaCreditoVM> listTC = new TarjetaCreditoAssembler().ConvertirListENToViewModel(tcList).ToList();


            var viewModel = new Tuple<IEnumerable<PaypalVM>, IEnumerable<TarjetaCreditoVM>>(listPP, listTC);
            SessionClose();
            return View(viewModel);
        }

        /*
        //GET: UsuarioController/MetodosPago/5
        [Authorize]
        public ActionResult MetodosPago(int id) 
        {
            // Validamos que el usuario introducido sea el que está registrado
            int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (idUserLogued != id)
                return RedirectToAction("Index", "Home");

            UsuarioRepository userRepository = new();
            UsuarioCEN userCEN = new(userRepository);

            // Recogemos los métodos de pago del usuario y los convertiimos a VM
            IList<MetodoPagoEN> mpsUserListEN = userCEN.DameMetodosDePago(id);

            IEnumerable<MetodoPagoVM> mpsUserListVM = new MetodoPagoAssembler().ConvertirListENToViewModel(mpsUserListEN).ToList();

            return View(mpsUserListVM);
        }
        */
    }
}
