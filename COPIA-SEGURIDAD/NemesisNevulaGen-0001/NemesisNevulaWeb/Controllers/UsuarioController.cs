using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.Infraestructure.Repository.NemesisNevula;
using NemesisNevulaWeb.Assemblers;
using NemesisNevulaWeb.Models;

namespace NemesisNevulaWeb.Controllers
{
    public class UsuarioController : BasicController
    {
        // GET: UsuarioController
        public ActionResult Index()
        {
            SessionInitialize();
            UsuarioRepository ususarioRepository = new UsuarioRepository();
            UsuarioCEN usuarioCEN = new UsuarioCEN(ususarioRepository);

            IList<UsuarioEN> listEN = usuarioCEN.DameTodos(0, -1);

            IEnumerable<UsuarioVM> listUsers = new UsuarioAssembler().ConvertirListENToViewModel(listEN).ToList();
            SessionClose();

            return View(listUsers);
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            SessionInitialize();

            UsuarioRepository userRepo = new();
            UsuarioCEN userCEN = new(userRepo);

            UsuarioEN userEN = userCEN.DamePorOID(id);
            UsuarioVM userView = new UsuarioAssembler().ConvertirENToViewModel(userEN);

            SessionClose();
            return View(userView);
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioVM user)
        {
            try
            {
                UsuarioRepository userRepo = new();
                UsuarioCEN userCEN = new(userRepo);

                userCEN.CrearUsuario(user.Nombre, user.Correo, user.ConGoogle, user.Foto_perfil, user.PuntosNevula, user.Cartera, user.Pass);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            SessionInitialize();

            UsuarioRepository userRepo = new();
            UsuarioCEN userCEN = new(userRepo);

            UsuarioEN userEN = userCEN.DamePorOID(id);
            UsuarioVM userView = new UsuarioAssembler().ConvertirENToViewModel(userEN);

            SessionClose();
            return View(userView);
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UsuarioVM user)
        {
            try
            {
                UsuarioRepository userRepo = new();
                UsuarioCEN userCEN = new(userRepo);

                userCEN.ModificarUsuario(id, user.Nombre, user.Correo, user.ConGoogle, user.Foto_perfil, user.PuntosNevula, user.Cartera, user.Pass);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            UsuarioRepository userRepo = new();
            UsuarioCEN userCEN = new(userRepo);

            userCEN.BorrarUsuario(id);

            return RedirectToAction(nameof(Index));
        }

        // POST: UsuarioController/Delete/5
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
