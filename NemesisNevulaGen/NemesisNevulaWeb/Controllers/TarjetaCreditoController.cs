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
            if (User.Identity.IsAuthenticated) actualizarEstado();
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
            if (User.Identity.IsAuthenticated) actualizarEstado();
            SessionInitialize();
            TarjetaCreditoRepository tcRepo = new TarjetaCreditoRepository(session);
            TarjetaCreditoCEN tcCEN = new TarjetaCreditoCEN(tcRepo);

            TarjetaCreditoEN tcEN = tcCEN.DamePorOID(id);

            // Validamos que la tarjeta de crédito le pertenezca al usuario logueado
            int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            UsuarioEN userTc = tcEN.UsuarioPoseedor;

            if (userTc.Id != idUserLogued)
                return RedirectToAction("Index", "Home");

            TarjetaCreditoVM tcView = new TarjetaCreditoAssembler().ConvertirENToViewModel(tcEN);

            SessionClose();
            return View(tcView);
        }

        // GET: TarjetaCreditoController/Create
        [Authorize]
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated) actualizarEstado();
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
                if (User.Identity.IsAuthenticated) actualizarEstado();
                TarjetaCreditoRepository tcRepo = new TarjetaCreditoRepository();
                TarjetaCreditoCEN tcCEN = new TarjetaCreditoCEN(tcRepo);

                int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                tcCEN.CrearTarjetaCredito(idUserLogued, tc.TipoTarjeta,tc.NombreEnTarjeta, tc.Numero, tc.FechaExpedicion, tc.CodigoSeguridad);
                return RedirectToAction("MetodPago", "Usuario", new { id = idUserLogued });
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
            if (User.Identity.IsAuthenticated) actualizarEstado();
            SessionInitialize();
            TarjetaCreditoRepository tcRepo = new TarjetaCreditoRepository(session);
            TarjetaCreditoCEN tcCEN = new TarjetaCreditoCEN(tcRepo);

            TarjetaCreditoEN tcEN = tcCEN.DamePorOID(id);

            // Validamos que la tarjeta de crédito le pertenezca al usuario logueado
            int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            UsuarioEN userTc = tcEN.UsuarioPoseedor;

            Console.WriteLine("Poseedor de la tarjeta: " + userTc.Id);
            Console.WriteLine("Usuario logueado: " + userTc.Id);

            if (userTc.Id != idUserLogued)
                return RedirectToAction("Index", "Home");

            Console.WriteLine("Usuario poseedor y logueado son los mismos");

            TarjetaCreditoVM tcView = new TarjetaCreditoAssembler().ConvertirENToViewModel(tcEN);

            SessionClose();
            return View(tcView);
        }

        // POST: TarjetaCreditoController/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TarjetaCreditoVM tc)
        {
            try
            {
                if (User.Identity.IsAuthenticated) actualizarEstado();
                SessionInitialize();

                TarjetaCreditoRepository tcRepo = new TarjetaCreditoRepository(session);
                TarjetaCreditoCEN tcCEN = new TarjetaCreditoCEN(tcRepo);

                TarjetaCreditoEN tcEN = tcCEN.DamePorOID(id);

                // Validamos que la tarjeta de crédito le pertenezca al usuario logueado
                int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                UsuarioEN userTc = tcEN.UsuarioPoseedor;

                if (userTc.Id != idUserLogued)
                    return RedirectToAction("Index", "Home");

                SessionClose();

                tcRepo = new TarjetaCreditoRepository();
                tcCEN = new TarjetaCreditoCEN(tcRepo);

                Console.WriteLine("Nombre actualizado? " + tc.NombreEnTarjeta);

                tcCEN.ModificarTarjetaCredito(id,tc.TipoTarjeta,tc.NombreEnTarjeta,tc.Numero, tc.FechaExpedicion,tc.CodigoSeguridad);

                return RedirectToAction("MetodPago", "Usuario", new {id = idUserLogued});
            }
            catch(Exception e)
            {
                Console.WriteLine("TarjetaCredito/Edit -> ERROR FATAL: "+e.Message);
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

            UsuarioEN userTc = tcEN.UsuarioPoseedor;

            Console.WriteLine("\nPoseedor de la tarjeta: " + userTc.Id + "\n");
            Console.WriteLine("Usuario logueado: " + userTc.Id + "\n");

            if (userTc.Id != idUserLogued)
                return RedirectToAction("Index", "Home");

            Console.WriteLine("Usuario poseedor y logueado son los mismos" + "\n");

            SessionClose();

            tcRepo = new TarjetaCreditoRepository();
            tcCEN = new TarjetaCreditoCEN(tcRepo);

            tcCEN.BorrarTarjetaCredito(id);

            return RedirectToAction("MetodPago", "Usuario", new { id = idUserLogued });
        }

        // POST: TarjetaCreditoController/Delete/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Console.WriteLine("Borrando Tarjeta de crédito con id " + id + "...");
                SessionInitialize();

                TarjetaCreditoRepository tcRepo = new TarjetaCreditoRepository(session);
                TarjetaCreditoCEN tcCEN = new TarjetaCreditoCEN(tcRepo);

                TarjetaCreditoEN tcEN = tcCEN.DamePorOID(id);

                // Validamos que la tarjeta de crédito le pertenezca al usuario logueado
                int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                UsuarioEN userTc = tcEN.UsuarioPoseedor;

                if (userTc.Id != idUserLogued)
                    return RedirectToAction("Index", "Home");

                SessionClose();
                return RedirectToAction("MetodPago", "Usuario", new { id = idUserLogued });
            }
            catch
            {
                return View();
            }
        }
    }
}
