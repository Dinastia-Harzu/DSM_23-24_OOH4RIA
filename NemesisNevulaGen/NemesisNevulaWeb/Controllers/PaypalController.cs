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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Drawing;

namespace NemesisNevulaWeb.Controllers
{
    public class PaypalController : BasicController
    {
        // GET: PayPalController
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            SessionInitialize();

            PaypalRepository ppRepository = new PaypalRepository(session);
            PaypalCEN ppCEN = new PaypalCEN(ppRepository);

            IList<PaypalEN> listEN = ppCEN.DameTodos(0, -1);

            IEnumerable<PaypalVM> listPP = new PaypalAssembler().ConvertirListENToViewModel(listEN).ToList();

            SessionClose();
            return View(listPP);
        }

        // GET: PayPalController/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            SessionInitialize();
            PaypalRepository ppRepo = new PaypalRepository(session);
            PaypalCEN ppCEN = new PaypalCEN(ppRepo);

            PaypalEN ppEN = ppCEN.DamePorOID(id);

            // Validamos que el paypal le pertenezca al usuario logueado
            IList<UsuarioEN> listPpUsers = ppEN.UsuarioPoseedor;

            int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!listPpUsers.Any(user => user.Id == idUserLogued))
                return RedirectToAction("Index", "Home");

            PaypalVM ppView = new PaypalAssembler().ConvertirENToViewModel(ppEN);

            SessionClose();
            return View(ppView);
        }

        // GET: PayPalController/Create
        [Authorize]
        public ActionResult Create()
        {
                return View();
        }

        // POST: PayPalController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaypalVM pp)
        {
            try
            {
                PaypalRepository ppRepo = new PaypalRepository();
                PaypalCEN ppCEN = new PaypalCEN(ppRepo);
                ppCEN.CrearPaypal(pp.Email, pp.Pass);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: PayPalController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            SessionInitialize();
            PaypalRepository ppRepo = new PaypalRepository(session);
            PaypalCEN ppCEN = new PaypalCEN(ppRepo);

            PaypalEN ppEN = ppCEN.DamePorOID(id);

            // Validamos que el paypal le pertenezca al usuario logueado
            IList<UsuarioEN> listPpUsers = ppEN.UsuarioPoseedor;

            int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!listPpUsers.Any(user => user.Id == idUserLogued))
                return RedirectToAction("Index", "Home");

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
                SessionInitialize();

                PaypalRepository ppRepo = new PaypalRepository();
                PaypalCEN ppCEN = new PaypalCEN(ppRepo);

                PaypalEN ppEN = ppCEN.DamePorOID(id);

                // Validamos que el paypal le pertenezca al usuario logueado
                IList<UsuarioEN> listPpUsers = ppEN.UsuarioPoseedor;

                int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (!listPpUsers.Any(user => user.Id == idUserLogued))
                    return RedirectToAction("Index", "Home");

                ppCEN.ModificarPaypal(id,pp.Email,pp.Pass);

                SessionClose();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PayPalController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            SessionInitialize();

            PaypalRepository ppRepo = new PaypalRepository(session);
            PaypalCEN ppCEN = new PaypalCEN(ppRepo);

            PaypalEN ppEN = ppCEN.DamePorOID(id);

            // Validamos que el paypal le pertenezca al usuario logueado
            IList<UsuarioEN> listPpUsers = ppEN.UsuarioPoseedor;

            int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!listPpUsers.Any(user => user.Id == idUserLogued))
                return RedirectToAction("Index", "Home");

            ppCEN.BorrarPaypal(id);

            SessionClose();
            return RedirectToAction(nameof(Index));
        }

        // POST: PayPalController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                SessionInitialize();

                PaypalRepository ppRepo = new PaypalRepository(session);
                PaypalCEN ppCEN = new PaypalCEN(ppRepo);

                PaypalEN ppEN = ppCEN.DamePorOID(id);

                // Validamos que el paypal le pertenezca al usuario logueado
                IList<UsuarioEN> listPpUsers = ppEN.UsuarioPoseedor;

                int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (!listPpUsers.Any(user => user.Id == idUserLogued))
                    return RedirectToAction("Index", "Home");

                SessionClose();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
