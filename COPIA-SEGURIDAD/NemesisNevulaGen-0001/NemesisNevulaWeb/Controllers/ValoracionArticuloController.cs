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
            return View();
        }

        // POST: ValoracionArticuloController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public ActionResult Create(ValoracionArticuloVM valoracionArticulo)
        {
            try
            {
                new ValoracionArticuloCEN(new ValoracionArticuloRepository()).CrearValoracion(
                    valoracionArticulo.Valoracion,
                    1,
                    1
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
            new ValoracionArticuloCEN(new ValoracionArticuloRepository()).BorrarValoracion(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: ValoracionArticuloController/Delete/5
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
