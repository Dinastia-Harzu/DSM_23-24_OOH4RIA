using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CP.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.Infraestructure.EN.NemesisNevula;
using NemesisNevulaGen.Infraestructure.Repository.NemesisNevula;
using NemesisNevulaWeb.Assemblers;
using NemesisNevulaWeb.Models;

namespace NemesisNevulaWeb.Controllers
{
    public class ValoracionArticuloController : BasicController
    {
        // GET: ValoracionArticuloController
        public ActionResult Index()
        {
            if (validarToken() == -1)
            {
                return RedirectToAction("Login", "Usuario");
            }

            SessionInitialize();
            ValoracionArticuloRepository valoracionArticuloRepository = new();
            ValoracionArticuloCEN valoracionArticuloCEN = new(valoracionArticuloRepository);
            IList<ValoracionArticuloEN> listaEN = valoracionArticuloCEN.DameTodos(0, -1);
            IEnumerable<ValoracionArticuloVM> listaVM = new ValoracionArticuloAssembler().ListaEN2ListaVM(listaEN);
            SessionClose();
            return View(listaVM);
        }

        // GET: ValoracionArticuloController/Details/5
        public ActionResult Details(int id)
        {
            if (validarToken() == -1)
            {
                return RedirectToAction("Login", "Usuario");
            }

            SessionInitialize();
            ValoracionArticuloRepository valoracionArticuloRepository = new(session);
            ValoracionArticuloCEN valoracionArticuloCEN = new(valoracionArticuloRepository);
            ValoracionArticuloEN valoracionArticuloEN = valoracionArticuloCEN.DamePorOID(id);
            ValoracionArticuloVM valoracionArticuloVM = new ValoracionArticuloAssembler().EN2VM(valoracionArticuloEN);
            SessionClose();
            return View(valoracionArticuloVM);
        }

        // GET: ValoracionArticuloController/Create
        public ActionResult Create()
        {
            if (validarToken() == -1)
            {
                return RedirectToAction("Login", "Usuario");
            }

            return View();
        }

        // POST: ValoracionArticuloController/Create/5
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public ActionResult Create(int id, ValoracionArticuloVM valoracionArticulo)
        {
            if (validarToken() == -1)
            {
                return RedirectToAction("Login", "Usuario");
            }

            try
            {
                new ValoracionArticuloCEN(new ValoracionArticuloRepository()).CrearValoracion(
                    valoracionArticulo.Valoracion,
                    id,
                    validarToken()
                );
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ValoracionArticuloController/Edit/5
        public ActionResult Edit(int id)
        {
            if (validarToken() == -1)
            {
                return RedirectToAction("Login", "Usuario");
            }

            SessionInitialize();
            ValoracionArticuloRepository valoracionArticuloRepository= new(session);
            ValoracionArticuloCEN valoracionArticuloCEN = new(valoracionArticuloRepository);
            ValoracionArticuloEN valoracionArticuloEN = valoracionArticuloCEN.DamePorOID(id);
            ValoracionArticuloVM valoracionArticulo = new ValoracionArticuloAssembler().EN2VM(valoracionArticuloEN);
            SessionClose();
            return View(valoracionArticulo);
        }

        // POST: ValoracionArticuloController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ValoracionArticuloVM valoracionArticulo)
        {
            if (validarToken() == -1)
            {
                return RedirectToAction("Login", "Usuario");
            }

            try
            {
                new ValoracionArticuloCEN(new ValoracionArticuloRepository()).ModificarValoracion(
                    id,
                    valoracionArticulo.Valoracion
                );
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ValoracionArticuloController/Delete/5
        public ActionResult Delete(int id)
        {
            if (validarToken() == -1)
            {
                return RedirectToAction("Login", "Usuario");
            }

            new ValoracionArticuloCEN(new ValoracionArticuloRepository()).BorrarValoracion(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: ValoracionArticuloController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            if (validarToken() == -1)
            {
                return RedirectToAction("Login", "Usuario");
            }

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
