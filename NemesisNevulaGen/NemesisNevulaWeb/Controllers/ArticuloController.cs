using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.Infraestructure.Repository.NemesisNevula;
using NemesisNevulaWeb.Assemblers;
using NemesisNevulaWeb.Models;

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
            ViewBag.CurrentPage = "Tienda";
            SessionInitialize();
            ArticuloRepository articuloRepository = new ArticuloRepository();
            ArticuloCEN articuloCEN = new ArticuloCEN(articuloRepository);

            IList<ArticuloEN> listEN = articuloCEN.DameTodos(0, -1);

            IEnumerable<ArticuloVM> listArts = new ArticuloAssembler().ConvertirListENToViewModel(listEN).ToList();

            SessionClose();
            return View(listArts);
        }

        // GET: ArticuloController vista admin
        public ActionResult VistaAdmin()
        {
            
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

            // Pasa el modelo IndexViewModel a la vista

            ViewBag.CurrentPage = "Tienda";
            SessionInitialize();
            ArticuloRepository artRepo = new ArticuloRepository(session);
            ArticuloCEN articuloCEN = new ArticuloCEN(artRepo);

            ArticuloEN articulo = articuloCEN.DamePorOID(id);
            ArticuloVM articuloVM = new ArticuloAssembler().ConvertirENToViewModel(articulo);

            SessionClose();
            var viewModel = new Tuple<ArticuloVM, IEnumerable<NoticiaVM>>(articuloVM, listaNoticias);
            return View(viewModel);
        }

        // GET: ArticuloController/Create
        public ActionResult Create()
        {
            ViewBag.CurrentPage = "CrearArticulo";
            return View();
        }

        // POST: ArticuloController/Create
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

                    using (var stream = System.IO.File.Create(path))
                    {
                        await articulo.Fotografia2.CopyToAsync(stream);
                    }
                }
                else
                {
                    Console.WriteLine("DENTRO DEL ELSE" + articulo.Fotografia2);
                    fileName = "noImage.jpg";
                }

                fileName = "/css/estilos/imagenes/" + fileName;


                artiCEN.CrearArticulo(articulo.Nombre, articulo.Descripcion, articulo.Precio, fileName, articulo.Rareza, articulo.Tipo, articulo.Valoracion, articulo.EsPublicado, articulo.FechaPublicacion, articulo.Temporada, articulo.Previsualizacion);

                    return RedirectToAction(nameof(Index));

                }
                catch (Exception e)
                {

                    return View();
                }
            }

        // GET: ArticuloController/Edit/5
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, ArticuloVM articulo)
        {
            Console.WriteLine("pRINCIPIO" + articulo.Fotografia);
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
                    Console.WriteLine("HOLIWIS22 \n" + articulo.Fotografia2 + " \n");
                    Console.WriteLine(articulo.Fotografia2);
                    fileName = Path.GetFileName(articulo.Fotografia2.FileName).Trim();

                    string directory = _webHost.WebRootPath + midirectorio;
                    path = Path.Combine(directory, fileName);

                    Console.WriteLine("antes del directory \n");
                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    Console.WriteLine("antes del path \n");
                    using (var stream = System.IO.File.Create(path))
                    {
                        await articulo.Fotografia2.CopyToAsync(stream);
                    }
                    Console.WriteLine("HASTA AQUI \n");
                    fileName = midirectorio + fileName;
                }
                else
                {
                    Console.WriteLine("ME MATO YA" + fileName);
                   
                    fileName = articulo.Fotografia;
                    Console.WriteLine("ME MATO YA DESPUES" + fileName);
                }



                Console.WriteLine("antes de modificar" + articulo.Fotografia);
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
        public ActionResult Delete(int id)
        {
            ArticuloRepository artRepo = new ArticuloRepository();
            ArticuloCEN articuloCEN = new ArticuloCEN(artRepo);

            articuloCEN.BorrarArticulo(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: ArticuloController/Delete/5
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
    }
}
