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

namespace NemesisNevulaWeb.Controllers
{
    public class ArticuloController : BasicController
    {
        // GET: ArticuloController
        public ActionResult Index()
        {
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

            ViewBag.CurrentPage = "Tienda";
            SessionInitialize();
            ArticuloRepository artRepo = new ArticuloRepository(session);
            ArticuloCEN articuloCEN = new ArticuloCEN(artRepo);

            ArticuloEN articulo = articuloCEN.DamePorOID(id);
            ArticuloVM articuloVM = new ArticuloAssembler().ConvertirENToViewModel(articulo);

            SessionClose();
            var viewModel = new Tuple<ArticuloVM, IEnumerable<NoticiaVM>, IEnumerable<ArticuloVM>>(articuloVM, listaNoticias,ar);
            return View(viewModel);
        }

        // GET: ArticuloController/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.CurrentPage = "Tienda";
            return View();
        }

        // POST: ArticuloController/Create
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticuloVM articulo)
        {
            ViewBag.CurrentPage = "Tienda";
            try
            {
                ArticuloRepository artRepo = new ArticuloRepository();
                ArticuloCEN articuloCEN = new ArticuloCEN(artRepo);
                articuloCEN.CrearArticulo(
                    articulo.Nombre,
                    articulo.Descripcion,
                    articulo.Precio,
                    articulo.Fotografia,
                    articulo.Rareza,
                    articulo.Tipo,
                    articulo.Valoracion,
                    articulo.EsPublicado,
                    articulo.FechaPublicacion,
                    articulo.Temporada,
                    articulo.Previsualizacion
                );
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ArticuloController/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int id)
        {
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
        public ActionResult Edit(int id, ArticuloVM articulo)
        {
            ViewBag.CurrentPage = "Tienda";
            try
            {
                ArticuloRepository artRepo = new ArticuloRepository();
                ArticuloCEN articuloCEN = new ArticuloCEN(artRepo);
                articuloCEN.ModificarArticulo(
                    id,
                    articulo.Nombre,
                    articulo.Descripcion,
                    articulo.Precio,
                    articulo.Fotografia,
                    articulo.Rareza,
                    articulo.Tipo,
                    articulo.Valoracion,
                    articulo.EsPublicado,
                    articulo.FechaPublicacion,
                    articulo.Temporada,
                    articulo.Previsualizacion
                );
                return RedirectToAction(nameof(Index));
            }
            catch
            {
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

            IList<ArticuloEN> artsList = artCEN.DameTodos(0,-1);

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
