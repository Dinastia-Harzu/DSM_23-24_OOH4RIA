﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.Infraestructure.Repository.NemesisNevula;
using NemesisNevulaWeb.Assemblers;
using NemesisNevulaWeb.Models;

namespace NemesisNevulaWeb.Controllers
{
    public class NoticiaController : BasicController
    {
        // GET: NoticiaController
        public ActionResult Index()
        {
            SessionInitialize();
            NoticiaRepository noticiaRepository = new();
            NoticiaCEN noticiaCEN = new(noticiaRepository);
            IList<NoticiaEN> listaEN = noticiaCEN.DameTodos(0, -1);
            IEnumerable<NoticiaVM> listaNoticias = new NoticiaAssembler().ListEN2VM(listaEN).ToList();
            SessionClose();
            return View(listaNoticias);
        }

        // GET: NoticiaController/Details/5
        public ActionResult Details(int id)
        {
            SessionInitialize();
            NoticiaRepository noticiaRepository = new(session);
            NoticiaCEN noticiaCEN = new(noticiaRepository);

            NoticiaEN noticiaEN = noticiaCEN.DamePorOID(id);
            NoticiaVM noticiaVM = new NoticiaAssembler().EN2VM(noticiaEN);

            SessionClose();
            return View(noticiaVM);
        }

        // GET: NoticiaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NoticiaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoticiaVM noticia)
        {
            try
            {
                new NoticiaCEN(new NoticiaRepository()).CrearNoticia(
                    noticia.Descripcion,
                    noticia.EsPublicada
                );
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NoticiaController/Edit/5
        public ActionResult Edit(int id)
        {
            SessionInitialize();
            NoticiaRepository noticiaRepository = new(session);
            NoticiaCEN noticiaCEN = new(noticiaRepository);
            NoticiaEN noticiaEN = noticiaCEN.DamePorOID(id);
            NoticiaVM noticia = new NoticiaAssembler().EN2VM(noticiaEN);
            SessionClose();
            return View(noticia);
        }

        // POST: NoticiaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NoticiaVM noticia)
        {
            try
            {
                new NoticiaCEN(new NoticiaRepository()).ModificarNoticia(
                    id,
                    noticia.Descripcion,
                    noticia.EsPublicada
                );
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NoticiaController/Delete/5
        public ActionResult Delete(int id)
        {
            new NoticiaCEN(new NoticiaRepository()).BorrarNoticia(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: NoticiaController/Delete/5
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
