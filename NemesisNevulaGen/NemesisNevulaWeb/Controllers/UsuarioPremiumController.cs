using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CP.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.Infraestructure.CP;
using NemesisNevulaGen.Infraestructure.Repository.NemesisNevula;
using NemesisNevulaWeb.Assemblers;
using NemesisNevulaWeb.Models;
using System.Security.Claims;


namespace NemesisNevulaWeb.Controllers
{
    public class UsuarioPremiumController : BasicController
    {
        // GET: UsuarioPremiumController
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            
            SessionInitialize();
            UsuarioPremiumRepository usupremRepository = new UsuarioPremiumRepository();
            UsuarioPremiumCEN usupremCEN = new UsuarioPremiumCEN(usupremRepository);

            IList<UsuarioPremiumEN> listEN = usupremCEN.DameTodos(0, -1);
            IEnumerable<UsuarioPremiumVM> listUsusPrem = new UsuarioPremiumAssembler().ConvertirListENToViewModel(listEN).ToList();
            SessionClose();

            return View(listUsusPrem);
        }

        // GET: UsuarioPremiumController/Details/5
        [Authorize(Roles = "Administrador,Premium")]
        public ActionResult Details(int id)
        {
            
            SessionInitialize();
            UsuarioPremiumRepository usupremRepository = new UsuarioPremiumRepository(session);
            UsuarioPremiumCEN usupremCEN = new UsuarioPremiumCEN(usupremRepository);

            UsuarioPremiumEN usupremEN = usupremCEN.DamePorOID(id);

            // Verificamos que el usuario logueado sea el mismo que el de la id

            int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            string rolUserLogued = User.FindFirstValue(ClaimTypes.Role);

            if (idUserLogued != id && rolUserLogued != "Administrador")
                return RedirectToAction("Index", "Home");
            

            UsuarioPremiumVM usupremVM = new UsuarioPremiumAssembler().ConvertirENToViewModel(usupremEN);

            SessionClose();
            return View(usupremVM);
        }

        // GET: UsuarioPremiumController/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: UsuarioPremiumController/Create
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioPremiumVM usuprem)
        {
            
            try
            {
                UsuarioPremiumRepository usupremRepository = new UsuarioPremiumRepository();
                UsuarioPremiumCEN usupremCEN = new UsuarioPremiumCEN(usupremRepository);
                UsuarioPremiumCP usuariopremiumCP = new UsuarioPremiumCP(new SessionCPNHibernate());

                usuariopremiumCP.CrearUsuarioPremium(usuprem.Nombre, usuprem.Correo, usuprem.ConGoogle, usuprem.Foto_perfil, usuprem.PuntosNevula, usuprem.Cartera,
                usuprem.Pass);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioPremiumController/Edit/5
        [Authorize(Roles = "Administrador,Premium")]
        public ActionResult Edit(int id)
        {
            
            SessionInitialize();
            UsuarioPremiumRepository usupremRepository = new UsuarioPremiumRepository(session);
            UsuarioPremiumCEN usupremCEN = new UsuarioPremiumCEN(usupremRepository);

            UsuarioPremiumEN usupremEN = usupremCEN.DamePorOID(id);

            // Verificamos que el usuario logueado sea el mismo que el de la id

            int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            string rolUserLogued = User.FindFirstValue(ClaimTypes.Role);

            if (idUserLogued != id && rolUserLogued != "Administrador")
                return RedirectToAction("Index", "Home");

            UsuarioPremiumVM usupremVM = new UsuarioPremiumAssembler().ConvertirENToViewModel(usupremEN);

            SessionClose();

            return View(usupremVM);
        }

        // POST: UsuarioPremiumController/Edit/5
        [Authorize(Roles = "Administrador,Premium")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UsuarioPremiumVM usup)
        {
            
            try
            {
                UsuarioPremiumRepository usupremRepository = new UsuarioPremiumRepository();
                UsuarioPremiumCEN usupremCEN = new UsuarioPremiumCEN(usupremRepository);

                // Verificamos que el usuario logueado sea el mismo que el de la id

                int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                string rolUserLogued = User.FindFirstValue(ClaimTypes.Role);

                if (idUserLogued != id && rolUserLogued != "Administrador")
                    return RedirectToAction("Index", "Home");

                usupremCEN.ModificarUsuarioPremium(usup.Id, usup.Nombre, usup.Correo, usup.ConGoogle, usup.Foto_perfil, usup.PuntosNevula,
                usup.Cartera, usup.Pass);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioPremiumController/Delete/5
        [Authorize(Roles = "Administrador,Premium")]
        public ActionResult Delete(int id)
        {
            
            UsuarioPremiumRepository usupremRepository = new UsuarioPremiumRepository();
            UsuarioPremiumCEN usupremCEN = new UsuarioPremiumCEN(usupremRepository);

            // Verificamos que el usuario logueado sea el mismo que el de la id

            int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            string rolUserLogued = User.FindFirstValue(ClaimTypes.Role);

            if (idUserLogued != id && rolUserLogued != "Administrador")
                return RedirectToAction("Index", "Home");

            usupremCEN.BorrarUsuarioPremium(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: UsuarioPremiumController/Delete/5
        [Authorize(Roles = "Administrador,Premium")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            
            try
            {
                // Verificamos que el usuario logueado sea el mismo que el de la id

                int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                string rolUserLogued = User.FindFirstValue(ClaimTypes.Role);

                if (idUserLogued != id && rolUserLogued != "Administrador")
                    return RedirectToAction("Index", "Home");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult PremiumActualizar()
        {
            if (User.Identity.IsAuthenticated) actualizarEstado();
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }
    }
}
