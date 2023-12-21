using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula;
using NemesisNevulaGen.Infraestructure.Repository.NemesisNevula;
using NemesisNevulaWeb.Assemblers;
using NemesisNevulaWeb.Models;
using System.Security.Claims;

namespace NemesisNevulaWeb.Controllers
{
    public class ArticuloController : BasicController
    {
        private readonly IWebHostEnvironment _webHost;

        public ArticuloController(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }
        // GET: ArticuloController
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated) actualizarEstado();
            ViewBag.CurrentPage = "Tienda";
            SessionInitialize();
            ArticuloRepository articuloRepository = new ArticuloRepository();
            ArticuloCEN articuloCEN = new ArticuloCEN(articuloRepository);

            IList<ArticuloEN> listEN = articuloCEN.DameTodos(0, -1);

            IEnumerable<ArticuloVM> listArts = new ArticuloAssembler().ConvertirListENToViewModel(listEN).ToList();

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
            return View(listArts);
        }

        // GET: ArticuloController vista admin
        public ActionResult VistaAdmin()
        {
            if (User.Identity.IsAuthenticated) actualizarEstado();
            SessionInitialize();
            ArticuloRepository articuloRepository = new ArticuloRepository();
            ArticuloCEN articuloCEN = new ArticuloCEN(articuloRepository);

            IList<ArticuloEN> listEN = articuloCEN.DameTodos(0, -1);

            IEnumerable<ArticuloVM> listArts = new ArticuloAssembler().ConvertirListENToViewModel(listEN).ToList();
            ViewBag.CurrentPage = "TodosArticulos";
            SessionClose();
            return View(listArts);
        }

        // GET: ArticuloController/Details/5
        public ActionResult Details(int id)
        {

            NoticiaRepository noticiaRepository = new NoticiaRepository();
            NoticiaCEN noticiaCEN = new NoticiaCEN(noticiaRepository);
            IList<NoticiaEN> listaEN = noticiaCEN.DameTodos(0, -1);
            IEnumerable<NoticiaVM> listaNoticias = new NoticiaAssembler().ListEN2VM(listaEN).ToList();

            ArticuloRepository articuloRepository = new ArticuloRepository();
            ArticuloCEN articuloCEN1 = new ArticuloCEN(articuloRepository);
            IList<ArticuloEN> artEN = articuloCEN1.DameTodos(0, -1);
            IEnumerable<ArticuloVM> ar = new ArticuloAssembler().ConvertirListENToViewModel(artEN).ToList();

            // Pasa el modelo IndexViewModel a la vista
            if (User.Identity.IsAuthenticated) actualizarEstado();
            ViewBag.CurrentPage = "Tienda";
            SessionInitialize();
            string idUserString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UsuarioRepository userRepo = new UsuarioRepository(session);
            UsuarioCEN userCEN = new(userRepo);
            IList<ArticuloEN> userArts = userCEN.DameArticulosComprados(int.Parse(idUserString));
            IEnumerable<ArticuloVM> userArtsVM = new ArticuloAssembler().ConvertirListENToViewModel(userArts).ToList();

            ArticuloRepository artRepo = new ArticuloRepository(session);
            ArticuloCEN articuloCEN = new ArticuloCEN(artRepo);

            ArticuloEN articulo = articuloCEN.DamePorOID(id);
            ArticuloVM articuloVM = new ArticuloAssembler().ConvertirENToViewModel(articulo);

            SessionClose();
            var viewModel = new Tuple<ArticuloVM, IEnumerable<NoticiaVM>, IEnumerable<ArticuloVM>, IEnumerable<ArticuloVM>>(articuloVM, listaNoticias, ar, userArtsVM);
            return View(viewModel);
        }

        // GET: ArticuloController/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated) actualizarEstado();
            ViewBag.CurrentPage = "CrearArticulo";
            return View();
        }

        // POST: ArticuloController/Create
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(ArticuloVM articulo)
        {
            ViewBag.CurrentPage = "CrearArticulo";
            ArticuloRepository artiRepo = new();
            ArticuloCEN artiCEN = new(artiRepo);

            // Validamos el token del usuario

            try
            {

                // Manejamos la subida de foto de perfil
                string fileName = "", path = "";
                if (articulo.Fotografia2 != null && articulo.Fotografia2.Length > 0)
                {
                    Console.WriteLine("DENTRO DEL IF" + articulo.Fotografia2);
                    fileName = Path.GetFileName(articulo.Fotografia2.FileName).Trim();

                    string directory = _webHost.WebRootPath + "/css/estilos/imagenes/";
                    path = Path.Combine(directory, fileName);

                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);
                    if (!System.IO.File.Exists(path))
                    {
                        using (var stream = System.IO.File.Create(path))
                        {
                            await articulo.Fotografia2.CopyToAsync(stream);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("DENTRO DEL ELSE" + articulo.Fotografia2);
                    fileName = "noImage.jpg";
                }

                fileName = "/css/estilos/imagenes/" + fileName;
                var previ = fileName;
                Console.WriteLine("DENTRO DEL ELSE" + fileName);

                artiCEN.CrearArticulo(articulo.Nombre, articulo.Descripcion, articulo.Precio, fileName, articulo.Rareza, articulo.Tipo, articulo.Valoracion, articulo.EsPublicado, articulo.FechaPublicacion, articulo.Temporada, previ);

                return RedirectToAction(nameof(Index));

            }
            catch (Exception e)
            {
                Console.WriteLine("excepcion" + e);
                return View();
            }
        }

        // GET: ArticuloController/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int id)
        {
            if (User.Identity.IsAuthenticated) actualizarEstado();
            ViewBag.CurrentPage = "Tienda";
            SessionInitialize();
            ArticuloRepository artRepo = new ArticuloRepository(session);
            ArticuloCEN articuloCEN = new ArticuloCEN(artRepo);

            ArticuloEN articuloEN = articuloCEN.DamePorOID(id);
            ArticuloVM articulo = new ArticuloAssembler().ConvertirENToViewModel(articuloEN);

            SessionClose();
            return View(articulo);
        }



        // POST: ArticuloController/Edit/5
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, ArticuloVM articulo)
        {
            if (User.Identity.IsAuthenticated) actualizarEstado();
            ViewBag.CurrentPage = "Tienda";
            ArticuloRepository artiRepo = new();
            ArticuloCEN artiCEN = new(artiRepo);

            // Validamos el token del usuario

            try
            {

                // Manejamos la subida de foto de perfil
                string fileName = "", path = "";
                if (articulo.Fotografia2 != null && articulo.Fotografia2.Length > 0)
                {
                    string midirectorio = "/css/estilos/imagenes/";
                    fileName = Path.GetFileName(articulo.Fotografia2.FileName).Trim();

                    string directory = _webHost.WebRootPath + midirectorio;
                    path = Path.Combine(directory, fileName);

                    Console.WriteLine("antes del directory \n");
                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    Console.WriteLine("antes del path \n");
                    if (!System.IO.File.Exists(path))
                    {
                        using (var stream = System.IO.File.Create(path))
                        {
                            await articulo.Fotografia2.CopyToAsync(stream);
                        }
                    }
                    Console.WriteLine("HASTA AQUI \n");
                    fileName = midirectorio + fileName;
                }
                else
                {
                    fileName = articulo.Fotografia;
                }



                Console.WriteLine("antes de modificar" + articulo.Fotografia + "\n");
                artiCEN.ModificarArticulo(id, articulo.Nombre, articulo.Descripcion, articulo.Precio, fileName, articulo.Rareza, articulo.Tipo, articulo.Valoracion, articulo.EsPublicado, articulo.FechaPublicacion, articulo.Temporada, articulo.Previsualizacion);
                Console.WriteLine("DESPUES de modificar" + articulo.Fotografia2);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception e)
            {
                Console.WriteLine("eXCEPCION" + e.Message + articulo.Fotografia);
                return View();
            }
        }

        // GET: ArticuloController/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int id)
        {
            ArticuloRepository artRepo = new ArticuloRepository();
            ArticuloCEN articuloCEN = new ArticuloCEN(artRepo);

            articuloCEN.BorrarArticulo(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: ArticuloController/Delete/5
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult FiltrarArts(string filtroBusqueda, string ordenarPor, string filtroRareza, string filtroTipo, string filtroFechaIni, string filtroFechaFin)
        {
            if (User.Identity.IsAuthenticated) actualizarEstado();
            ViewBag.CurrentPage = "Tienda";
            Console.WriteLine("\n\n\n--FILTROS--\n");
            Console.WriteLine("Barra de Búsqueda: " + filtroBusqueda + "\n");
            Console.WriteLine("Criterio de ordenacion: " + ordenarPor + "\n");
            Console.WriteLine("Rareza: " + filtroRareza + "\n");
            Console.WriteLine("Tipo Articulo: " + filtroTipo + "\n");
            Console.WriteLine("Fecha inicio: " + filtroFechaIni + "\n");
            Console.WriteLine("Fecha final: " + filtroFechaFin + "\n");

            SessionInitialize();

            ArticuloRepository artRepo = new(session);
            ArticuloCEN artCEN = new(artRepo);

            IList<ArticuloEN> artsList = artCEN.DameTodos(0, -1);

            // FILTROS:
            artsList = ordenar(artsList, ordenarPor);

            List<ArticuloEN> listaAux = new(artsList);

            listaAux = filtrarXRareza(listaAux, filtroRareza);

            listaAux = filtrarXTipo(listaAux, filtroTipo);

            listaAux = filtrarXFecha(listaAux, filtroFechaIni, filtroFechaFin);

            listaAux = filtrarXInput(listaAux, filtroBusqueda);

            artsList = listaAux;

            IEnumerable<ArticuloVM> listArts = new ArticuloAssembler().ConvertirListENToViewModel(artsList).ToList();

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

            return View(listArts);
        }
    }
}