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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace NemesisNevulaWeb.Controllers
{
    public class MetodoPagoController : BasicController
    {
        // GET: MetodoPagoController
        [Authorize(Roles = "Administrador")]
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

        [Authorize]
        // GET: MetodoPagoController/Details/5
        public ActionResult Details(int id)
        {
            SessionInitialize();

            MetodoPagoRepository mpRepository = new MetodoPagoRepository(session);
            MetodoPagoCEN mpCEN = new MetodoPagoCEN(mpRepository);

            MetodoPagoEN mp = mpCEN.DamePorOID(id);

            // Validamos que el método de pago le pertenezca al usuario logueado
            int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            UsuarioEN userTc = mp.UsuarioPoseedor;

            if (userTc.Id != idUserLogued)
                return RedirectToAction("Index", "Home");

            SessionClose();

            return View();
        }

        // GET: MetodoPagoController/Create
        [Authorize]
        public ActionResult Create(MetodoPagoVM mp)
        {
            try
            {
                MetodoPagoRepository mpRepo = new MetodoPagoRepository();
                MetodoPagoCEN mpCEN = new MetodoPagoCEN(mpRepo);

                int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                mpCEN.CrearMetodoPago(idUserLogued);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            } 
        }

        // POST: MetodoPagoController/Create
        [Authorize]
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
        [Authorize]
        public ActionResult Edit(int id)
        {
            SessionInitialize();

            MetodoPagoRepository mpRepository = new MetodoPagoRepository(session);
            MetodoPagoCEN mpCEN = new MetodoPagoCEN(mpRepository);

            MetodoPagoEN mp = mpCEN.DamePorOID(id);

            // Validamos que el método de pago le pertenezca al usuario logueado
            int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            UsuarioEN userTc = mp.UsuarioPoseedor;

            if (userTc.Id != idUserLogued)
                return RedirectToAction("Index", "Home");

            SessionClose();

            return View();
        }

        // POST: MetodoPagoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                SessionInitialize();

                MetodoPagoRepository mpRepository = new MetodoPagoRepository(session);
                MetodoPagoCEN mpCEN = new MetodoPagoCEN(mpRepository);

                MetodoPagoEN mp = mpCEN.DamePorOID(id);

                // Validamos que el método de pago le pertenezca al usuario logueado
                int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                UsuarioEN userTc = mp.UsuarioPoseedor;

                if (userTc.Id != idUserLogued)
                    return RedirectToAction("Index", "Home");

                SessionClose();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MetodoPagoController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            SessionInitialize();

            MetodoPagoRepository mpRepository = new MetodoPagoRepository(session);
            MetodoPagoCEN mpCEN = new MetodoPagoCEN(mpRepository);

            MetodoPagoEN mp = mpCEN.DamePorOID(id);

            // Validamos que el método de pago le pertenezca al usuario logueado
            int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            UsuarioEN userTc = mp.UsuarioPoseedor;

            if (userTc.Id != idUserLogued)
                return RedirectToAction("Index", "Home");

            SessionClose();

            return View();
        }

        // POST: MetodoPagoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                SessionInitialize();

                MetodoPagoRepository mpRepository = new MetodoPagoRepository(session);
                MetodoPagoCEN mpCEN = new MetodoPagoCEN(mpRepository);

                MetodoPagoEN mp = mpCEN.DamePorOID(id);

                // Validamos que el método de pago le pertenezca al usuario logueado
                int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                UsuarioEN userTc = mp.UsuarioPoseedor;

                if (userTc.Id != idUserLogued)
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
