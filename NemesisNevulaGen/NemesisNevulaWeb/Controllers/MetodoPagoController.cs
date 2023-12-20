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
using System.Security.Claims;
namespace NemesisNevulaWeb.Controllers
{
    public class MetodoPagoController : BasicController
    {
        // GET: MetodoPagoController
        public ActionResult Index()
        {
            SessionInitialize();
            MetodoPagoRepository mpRepository = new MetodoPagoRepository(session);
            MetodoPagoCEN mpCEN = new MetodoPagoCEN(mpRepository);

            IList<MetodoPagoEN> listEN = mpCEN.DameTodos(0, -1);

            IEnumerable<MetodoPagoVM> listMP = new MetodoPagoAssembler().ConvertirListENToViewModel(listEN).ToList();

            SessionClose();
            return View(listMP);
        }

        // GET: MetodoPagoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MetodoPagoController/Create
        public ActionResult Create(MetodoPagoVM mp)
        {
            try
            {
                MetodoPagoRepository mpRepo = new MetodoPagoRepository();
                MetodoPagoCEN mpCEN = new MetodoPagoCEN(mpRepo);
                mpCEN.CrearMetodoPago(Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            } 
        }

        // POST: MetodoPagoController/Create
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

        // GET: MetodoPagoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MetodoPagoController/Edit/5
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

        // GET: MetodoPagoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MetodoPagoController/Delete/5
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
