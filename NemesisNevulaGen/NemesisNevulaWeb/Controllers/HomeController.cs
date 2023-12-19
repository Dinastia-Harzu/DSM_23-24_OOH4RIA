using Microsoft.AspNetCore.Mvc;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.Infraestructure.Repository.NemesisNevula;
using NemesisNevulaWeb.Assemblers;
using NemesisNevulaWeb.Models;
using System.Diagnostics;

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace NemesisNevulaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult 
            Index()
        {

            NoticiaRepository noticiaRepository = new NoticiaRepository();
            NoticiaCEN noticiaCEN = new NoticiaCEN(noticiaRepository);
            IList<NoticiaEN> listaEN = noticiaCEN.DameTodos(0, -1);
            IEnumerable<NoticiaVM> listaNoticias = new NoticiaAssembler().ListEN2VM(listaEN).ToList();

            var viewModel = new Tuple<IEnumerable<NoticiaVM>>(listaNoticias);

            // Pasa el modelo IndexViewModel a la vista
            ViewBag.CurrentPage = "Inicio";
            return View(viewModel);  
        }

        public IActionResult 
            Privacy()
        {
            return View();
        }

        public IActionResult
            Contacto()
        {
            ViewBag.CurrentPage = "Contacto";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
