﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.Infraestructure.Repository.NemesisNevula;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.Infraestructure.Repository.NemesisNevula;
using NemesisNevulaWeb.Assemblers;
using NemesisNevulaWeb.Models;
using System.Security.Claims;

namespace NemesisNevulaWeb.Controllers
{
    public class PaypalController : BasicController
    {
        // GET: PayPalController
        public ActionResult Index()
        {
            int idUser = validarToken();

            if (idUser == -1)
                return RedirectToAction("Login","Usuario");

            SessionInitialize();
            PaypalRepository ppRepository = new PaypalRepository(session);
            PaypalCEN ppCEN = new PaypalCEN(ppRepository);

            IList<PaypalEN> listEN = ppCEN.DameTodos(0, -1);

            IEnumerable<PaypalVM> listPP = new PaypalAssembler().ConvertirListENToViewModel(listEN).ToList();

            

            SessionClose();
            return View(listPP);
        }

        // GET: PayPalController/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
                return RedirectToAction("Index","Home");
            SessionInitialize();
            PaypalRepository ppRepo = new PaypalRepository(session);
            PaypalCEN ppCEN = new PaypalCEN(ppRepo);

            PaypalEN ppEN = ppCEN.DamePorOID(id);
            PaypalVM ppView = new PaypalAssembler().ConvertirENToViewModel(ppEN);

            SessionClose();
            return View(ppView);
        }

        // GET: PayPalController/Create
        public ActionResult Create()
        {
                return View();
        }

        // POST: PayPalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaypalVM pp)
        {
            try
            {
                PaypalRepository ppRepo = new PaypalRepository();
                PaypalCEN ppCEN = new PaypalCEN(ppRepo);
                ppCEN.CrearPaypal(Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), pp.Email, pp.Pass);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: PayPalController/Edit/5
        public ActionResult Edit(int id)
        {
            SessionInitialize();
            PaypalRepository ppRepo = new PaypalRepository(session);
            PaypalCEN ppCEN = new PaypalCEN(ppRepo);

            PaypalEN ppEN = ppCEN.DamePorOID(id);
            PaypalVM ppView = new PaypalAssembler().ConvertirENToViewModel(ppEN);

            SessionClose();
            return View(ppView);
        }

        // POST: PayPalController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,PaypalVM pp)
        {
            try
            {
                PaypalRepository ppRepo = new PaypalRepository();
                PaypalCEN ppCEN = new PaypalCEN(ppRepo);
                ppCEN.ModificarPaypal(id,pp.Email,pp.Pass);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PayPalController/Delete/5
        public ActionResult Delete(int id)
        {
            PaypalRepository ppRepo = new PaypalRepository();
            PaypalCEN ppCEN = new PaypalCEN(ppRepo);
            ppCEN.BorrarPaypal(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: PayPalController/Delete/5
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
