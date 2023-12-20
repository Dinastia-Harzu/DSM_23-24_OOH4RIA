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
using NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula;
using System.Security.Claims;

namespace NemesisNevulaWeb.Controllers
{
    public class TarjetaCreditoController : BasicController
    {
        // GET: TarjetaCreditoController
        public ActionResult Index()
        {
            int idUser = validarToken();
            if (idUser == -1)
                return RedirectToAction("Login", "Usuario");
            SessionInitialize();
            TarjetaCreditoRepository tjRepository = new TarjetaCreditoRepository(session);
            TarjetaCreditoCEN tcCEN = new TarjetaCreditoCEN(tjRepository);

            IList<TarjetaCreditoEN> listEN = tcCEN.DameTodos(0, -1);

            IEnumerable<TarjetaCreditoVM> listTC = new TarjetaCreditoAssembler().ConvertirListENToViewModel(listEN).ToList();

            SessionClose();
            return View(listTC);
        }

        // GET: TarjetaCreditoController/Details/5
        public ActionResult Details(int id)
        {
            SessionInitialize();
            TarjetaCreditoRepository tcRepo = new TarjetaCreditoRepository(session);
            TarjetaCreditoCEN tcCEN = new TarjetaCreditoCEN(tcRepo);

            TarjetaCreditoEN tcEN = tcCEN.DamePorOID(id);
            TarjetaCreditoVM tcView = new TarjetaCreditoAssembler().ConvertirENToViewModel(tcEN);

            SessionClose();
            return View(tcView);
        }

        // GET: TarjetaCreditoController/Create
        public ActionResult Create()
        { 
            return View();
        }

        // POST: TarjetaCreditoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TarjetaCreditoVM tc)
        {
            try
            {
                TarjetaCreditoRepository tcRepo = new TarjetaCreditoRepository();
                TarjetaCreditoCEN tcCEN = new TarjetaCreditoCEN(tcRepo);
                tcCEN.CrearTarjetaCredito(Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), tc.TipoTarjeta,tc.NombreEnTarjeta, tc.Numero, tc.FechaExpedicion, tc.CodigoSeguridad);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TarjetaCreditoController/Edit/5
        public ActionResult Edit(int id)
        {
            SessionInitialize();
            TarjetaCreditoRepository tcRepo = new TarjetaCreditoRepository(session);
            TarjetaCreditoCEN tcCEN = new TarjetaCreditoCEN(tcRepo);

            TarjetaCreditoEN tcEN = tcCEN.DamePorOID(id);
            TarjetaCreditoVM tcView = new TarjetaCreditoAssembler().ConvertirENToViewModel(tcEN);

            SessionClose();
            return View(tcView);
        }

        // POST: TarjetaCreditoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TarjetaCreditoVM tc)
        {
            try
            {
                TarjetaCreditoRepository tcRepo = new TarjetaCreditoRepository();
                TarjetaCreditoCEN tcCEN = new TarjetaCreditoCEN(tcRepo);
                tcCEN.ModificarTarjetaCredito(id,tc.TipoTarjeta,tc.CodigoSeguridad,tc.Numero, tc.FechaExpedicion,tc.NombreEnTarjeta);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TarjetaCreditoController/Delete/5
        public ActionResult Delete(int id)
        {
            TarjetaCreditoRepository tcRepo = new TarjetaCreditoRepository();
            TarjetaCreditoCEN tcCEN = new TarjetaCreditoCEN(tcRepo);
            tcCEN.BorrarTarjetaCredito(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: TarjetaCreditoController/Delete/5
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
