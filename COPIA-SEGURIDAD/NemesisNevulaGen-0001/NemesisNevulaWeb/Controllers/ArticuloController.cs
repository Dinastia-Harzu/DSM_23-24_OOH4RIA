﻿using Microsoft.AspNetCore.Http;
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
        // GET: ArticuloController
        public ActionResult Index()
        {
            SessionInitialize();
            ArticuloRepository articuloRepository = new ArticuloRepository();
            ArticuloCEN articuloCEN = new ArticuloCEN(articuloRepository);

            IList<ArticuloEN> listEN = articuloCEN.DameTodos(0, -1);

            IEnumerable<ArticuloVM> listArts = new ArticuloAssembler().ConvertirListENToViewModel(listEN).ToList();

            SessionClose();
            return View(listArts);
        }

        // GET: ArticuloController/Details/5
        public ActionResult Details(int id)
        {
            SessionInitialize();
            ArticuloRepository artRepo = new ArticuloRepository(session);
            ArticuloCEN articuloCEN = new ArticuloCEN(artRepo);

            ArticuloEN articulo = articuloCEN.DamePorOID(id);
            ArticuloVM articuloVM = new ArticuloAssembler().ConvertirENToViewModel(articulo);

            SessionClose();
            return View(articuloVM);
        }

        // GET: ArticuloController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArticuloController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticuloVM articulo)
        {
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
        public ActionResult Edit(int id)
        {
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
        public ActionResult Edit(int id, ArticuloVM articulo)
        {
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
