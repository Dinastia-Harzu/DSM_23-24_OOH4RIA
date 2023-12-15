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
            SessionInitialize();
            PaypalRepository ppRepo = new PaypalRepository(session);
            PaypalCEN ppCEN = new PaypalCEN(ppRepo);

            PaypalEN ppEN = ppCEN.DamePorOID(id);
            PaypalVM ppView = new PaypalAssembler().ConvertirENToViewModel(ppEN);

            SessionClose();
            return View(ppView);
        }

        // GET: PayPalController/Create
        public ActionResult Create()
        {
                return View();
        }

        // POST: PayPalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaypalVM pp)
        {
            try
            {
                PaypalRepository ppRepo = new PaypalRepository();
                PaypalCEN ppCEN = new PaypalCEN(ppRepo);

                UsuarioRepository usuarioRepository = new();
                UsuarioCEN usuarioCEN = new UsuarioCEN(usuarioRepository);
                IList<UsuarioEN> listaEN = usuarioCEN.DameTodos(0, -1);
                IList<int> listaUsuarios = new List<int>();
                foreach (UsuarioEN en in listaEN)
                {
                    listaUsuarios.Add(en.Id);
                }
                ppCEN.CrearPaypal(listaUsuarios, pp.Email, pp.Pass);
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
            SessionInitialize();
            PaypalRepository ppRepo = new PaypalRepository(session);
            PaypalCEN ppCEN = new PaypalCEN(ppRepo);

            PaypalEN ppEN = ppCEN.DamePorOID(id);
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
                PaypalRepository ppRepo = new PaypalRepository();
                PaypalCEN ppCEN = new PaypalCEN(ppRepo);
                ppCEN.ModificarPaypal(id,pp.Email,pp.Pass);
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
            PaypalRepository ppRepo = new PaypalRepository();
            PaypalCEN ppCEN = new PaypalCEN(ppRepo);
            ppCEN.BorrarPaypal(id);
            return RedirectToAction(nameof(Index));
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
