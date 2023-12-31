﻿using Microsoft.AspNetCore.Mvc;
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
    public class HomeController : BasicController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult 
            Index()
        {
            if (User.Identity.IsAuthenticated) actualizarEstado();
            NoticiaRepository noticiaRepository = new NoticiaRepository();
            NoticiaCEN noticiaCEN = new NoticiaCEN(noticiaRepository);
            IList<NoticiaEN> listaEN = noticiaCEN.DameTodos(0, -1);
            IEnumerable<NoticiaVM> listaNoticias = new NoticiaAssembler().ListEN2VM(listaEN).ToList();

            var viewModel = new Tuple<IEnumerable<NoticiaVM>>(listaNoticias);

            // Pasa el modelo IndexViewModel a la vista
            if (User.Identity.IsAuthenticated) actualizarEstado();
            ViewBag.CurrentPage = "Inicio";
            return View(viewModel);  
        }

        public IActionResult 
            Privacy()
        {
            if (User.Identity.IsAuthenticated) actualizarEstado();
            return View();
        }

        public IActionResult
            Contacto()
        {
            if (User.Identity.IsAuthenticated) actualizarEstado();
            ViewBag.CurrentPage = "Contacto";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            if (User.Identity.IsAuthenticated) actualizarEstado();
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
