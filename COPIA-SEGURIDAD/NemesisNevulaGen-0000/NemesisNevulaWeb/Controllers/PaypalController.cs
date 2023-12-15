using Microsoft.AspNetCore.Http;
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

namespace NemesisNevulaWeb.Controllers
{
    public class PaypalController : BasicController
    {
        // GET: PayPalController
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
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PayPalController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PayPalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: PayPalController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PayPalController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: PayPalController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
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
