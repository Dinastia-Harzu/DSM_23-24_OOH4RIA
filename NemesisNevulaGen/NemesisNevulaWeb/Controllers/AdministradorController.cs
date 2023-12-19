using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.Infraestructure.Repository.NemesisNevula;
using NemesisNevulaWeb.Assemblers;
using NemesisNevulaWeb.Models;

namespace NemesisNevulaWeb.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdministradorController : BasicController
    {
        // GET: AdministradorController
        public ActionResult Index()
        {
            SessionInitialize();
            AdministradorRepository adminRepository = new AdministradorRepository();
            AdministradorCEN adminCEN = new AdministradorCEN(adminRepository);

            IList<AdministradorEN> listEN = adminCEN.DameTodos(0, -1);
            IEnumerable<AdministradorVM> listAdmins = new AdministradorAssembler().ConvertirListENToViewModel(listEN).ToList();
            SessionClose();

            return View(listAdmins);
        }

        // GET: AdministradorController/Details/5
        public ActionResult Details(int id)
        {
            SessionInitialize();
            AdministradorRepository administradorRepository = new AdministradorRepository(session);
            AdministradorCEN administradorCEN = new AdministradorCEN(administradorRepository);

            AdministradorEN administradorEN = administradorCEN.DamePorOID(id);
            AdministradorVM administradorVM = new AdministradorAssembler().ConvertirENToViewModel(administradorEN);

            SessionClose();
            return View(administradorVM);
        }

        // GET: AdministradorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdministradorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdministradorVM admin)
        {
            try
            {
                AdministradorRepository adminRepository = new AdministradorRepository();
                AdministradorCEN adminCEN = new AdministradorCEN(adminRepository);

                adminCEN.CrearAdmin(admin.Nombre, admin.Correo, admin.ConGoogle, admin.Foto_perfil, admin.PuntosNevula, admin.Cartera,
                admin.Pass);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdministradorController/Edit/5
        public ActionResult Edit(int id)
        {
            SessionInitialize();
            AdministradorRepository administradorRepository = new AdministradorRepository(session);
            AdministradorCEN administradorCEN = new AdministradorCEN(administradorRepository);

            AdministradorEN administradorEN = administradorCEN.DamePorOID(id);
            AdministradorVM administradorVM = new AdministradorAssembler().ConvertirENToViewModel(administradorEN);

            SessionClose();
            return View(administradorVM);
        }

        // POST: AdministradorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AdministradorVM admin)
        {
            try
            {
                AdministradorRepository adminRepository = new AdministradorRepository();
                AdministradorCEN adminCEN = new AdministradorCEN(adminRepository);
                adminCEN.ModificarAdmin(admin.Id, admin.Nombre, admin.Correo, admin.ConGoogle, admin.Foto_perfil, admin.PuntosNevula,
                admin.Cartera, admin.Pass);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdministradorController/Delete/5
        public ActionResult Delete(int id)
        {
            AdministradorRepository adminRepository = new AdministradorRepository();
            AdministradorCEN adminCEN = new AdministradorCEN(adminRepository);
            adminCEN.BorrarAdmin(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: AdministradorController/Delete/5
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
