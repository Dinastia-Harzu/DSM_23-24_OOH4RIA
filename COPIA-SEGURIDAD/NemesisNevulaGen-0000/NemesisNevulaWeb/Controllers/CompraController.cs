using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.Infraestructure.Repository.NemesisNevula;
using NemesisNevulaWeb.Assemblers;
using NemesisNevulaWeb.Models;

namespace NemesisNevulaWeb.Controllers
{
    public class CompraController : BasicController
    {
        // GET: CompraController
        public ActionResult Index()
        {
            SessionInitialize();
            CompraRepository compraRepository = new CompraRepository();
            CompraCEN compraCEN = new CompraCEN(compraRepository);

            IList<CompraEN> listEN = compraCEN.DameTodos(0, -1);
            IEnumerable<CompraVM> listComps = new CompraAssembler().ConvertirListENToViewModel(listEN).ToList();
            SessionClose();

            return View(listComps);
        }

        // GET: CompraController/Details/5
        public ActionResult Details(int id)
        {
            SessionInitialize();
            CompraRepository compraRepository = new CompraRepository(session);
            CompraCEN compraCEN = new CompraCEN(compraRepository);

            CompraEN compraEN = compraCEN.DamePorOID(id);
            CompraVM compraVM = new CompraAssembler().ConvertirENToViewModel(compraEN);

            SessionClose();
            return View(compraVM);
        }

        // GET: CompraController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompraController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompraVM comp)
        {
            try
            {
                CompraRepository compraRepository = new CompraRepository();
                CompraCEN compraCEN = new CompraCEN(compraRepository);

                // CREACION NO DEFINITIVA: A ESPERAS DE COMO SE HACE
                compraCEN.CrearCompra(comp.Fecha, 32768, 1, comp.PrecioTotal, comp.FechaCaducidad, false, 32768);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CompraController/Edit/5
        public ActionResult Edit(int id)
        {
            SessionInitialize();
            CompraRepository compraRepository = new CompraRepository(session);
            CompraCEN compraCEN = new CompraCEN(compraRepository);

            CompraEN compraEN = compraCEN.DamePorOID(id);
            CompraVM compraVM = new CompraAssembler().ConvertirENToViewModel(compraEN);

            SessionClose();

            return View(compraVM);
        }

        // POST: CompraController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CompraVM comp)
        {
            try
            {
                CompraRepository compraRepository = new CompraRepository();
                CompraCEN compraCEN = new CompraCEN(compraRepository);
                compraCEN.ModificarCompra(comp.Id, comp.Fecha, comp.PrecioTotal, comp.FechaCaducidad, false);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CompraController/Delete/5
        public ActionResult Delete(int id)
        {
            CompraRepository compraRepository = new CompraRepository();
            CompraCEN compraCEN = new CompraCEN(compraRepository);
            compraCEN.BorrarCompra(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: CompraController/Delete/5
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
