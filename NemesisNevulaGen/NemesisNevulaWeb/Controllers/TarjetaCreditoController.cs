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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace NemesisNevulaWeb.Controllers
{
    public class TarjetaCreditoController : BasicController
    {
        // GET: TarjetaCreditoController
        [Authorize(Roles = "Administrador")]
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
        [Authorize]
        public ActionResult Details(int id)
        {
            SessionInitialize();
            TarjetaCreditoRepository tcRepo = new TarjetaCreditoRepository(session);
            TarjetaCreditoCEN tcCEN = new TarjetaCreditoCEN(tcRepo);

            TarjetaCreditoEN tcEN = tcCEN.DamePorOID(id);

            // Validamos que la tarjeta de crédito le pertenezca al usuario logueado
            int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            IList<UsuarioEN> listTcUsers = tcEN.UsuarioPoseedor;

            if (!listTcUsers.Any(tc => tc.Id == idUserLogued))
                return RedirectToAction("Index", "Home");

            TarjetaCreditoVM tcView = new TarjetaCreditoAssembler().ConvertirENToViewModel(tcEN);

            SessionClose();
            return View(tcView);
        }

        // GET: TarjetaCreditoController/Create
        [Authorize]
        public ActionResult Create()
        { 
            return View();
        }

        // POST: TarjetaCreditoController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TarjetaCreditoVM tc)
        {
            try
            {
                TarjetaCreditoRepository tcRepo = new TarjetaCreditoRepository();
                TarjetaCreditoCEN tcCEN = new TarjetaCreditoCEN(tcRepo);
                tcCEN.CrearTarjetaCredito(tc.TipoTarjeta,tc.NombreEnTarjeta, tc.Numero, tc.FechaExpedicion, tc.CodigoSeguridad);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TarjetaCreditoController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            SessionInitialize();
            TarjetaCreditoRepository tcRepo = new TarjetaCreditoRepository(session);
            TarjetaCreditoCEN tcCEN = new TarjetaCreditoCEN(tcRepo);

            TarjetaCreditoEN tcEN = tcCEN.DamePorOID(id);

            // Validamos que la tarjeta de crédito le pertenezca al usuario logueado
            int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            IList<UsuarioEN> listTcUsers = tcEN.UsuarioPoseedor;

            if (!listTcUsers.Any(tc => tc.Id == idUserLogued))
                return RedirectToAction("Index", "Home");

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
                SessionInitialize();

                TarjetaCreditoRepository tcRepo = new TarjetaCreditoRepository(session);
                TarjetaCreditoCEN tcCEN = new TarjetaCreditoCEN(tcRepo);

                TarjetaCreditoEN tcEN = tcCEN.DamePorOID(id);

                // Validamos que la tarjeta de crédito le pertenezca al usuario logueado
                int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                IList<UsuarioEN> listTcUsers = tcEN.UsuarioPoseedor;

                if (!listTcUsers.Any(tc => tc.Id == idUserLogued))
                    return RedirectToAction("Index", "Home");

                tcCEN.ModificarTarjetaCredito(id,tc.TipoTarjeta,tc.CodigoSeguridad,tc.Numero, tc.FechaExpedicion,tc.NombreEnTarjeta);

                SessionClose();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TarjetaCreditoController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            SessionInitialize();

            TarjetaCreditoRepository tcRepo = new TarjetaCreditoRepository(session);
            TarjetaCreditoCEN tcCEN = new TarjetaCreditoCEN(tcRepo);

            TarjetaCreditoEN tcEN = tcCEN.DamePorOID(id);

            // Validamos que la tarjeta de crédito le pertenezca al usuario logueado
            int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            IList<UsuarioEN> listTcUsers = tcEN.UsuarioPoseedor;

            if (!listTcUsers.Any(tc => tc.Id == idUserLogued))
                return RedirectToAction("Index", "Home");

            tcCEN.BorrarTarjetaCredito(id);

            SessionClose();
            return RedirectToAction(nameof(Index));
        }

        // POST: TarjetaCreditoController/Delete/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                SessionInitialize();

                TarjetaCreditoRepository tcRepo = new TarjetaCreditoRepository(session);
                TarjetaCreditoCEN tcCEN = new TarjetaCreditoCEN(tcRepo);

                TarjetaCreditoEN tcEN = tcCEN.DamePorOID(id);

                // Validamos que la tarjeta de crédito le pertenezca al usuario logueado
                int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                IList<UsuarioEN> listTcUsers = tcEN.UsuarioPoseedor;

                if (!listTcUsers.Any(tc => tc.Id == idUserLogued))
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
