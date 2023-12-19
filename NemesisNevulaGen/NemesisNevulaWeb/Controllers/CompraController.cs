using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.Infraestructure.Repository.NemesisNevula;
using NemesisNevulaWeb.Assemblers;
using NemesisNevulaWeb.Models;

namespace NemesisNevulaWeb.Controllers
{
    [Authorize(Roles = "Administrador")]
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
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            UsuarioCEN usuarioCEN = new UsuarioCEN(usuarioRepository);

            IList<UsuarioEN> listaUsuarios = usuarioCEN.DameTodos(0, -1);
            IList<SelectListItem> usuariosItems = new List<SelectListItem>();

            foreach (UsuarioEN usuario in listaUsuarios)
            {
                usuariosItems.Add(new SelectListItem { Text = usuario.Id.ToString(), Value = usuario.Id.ToString() });
            }

            ViewData["usuariosItems"] = usuariosItems;

            // --------------------------------------------------------------------------------

            ArticuloRepository articuloRepository = new ArticuloRepository();
            ArticuloCEN articuloCEN = new ArticuloCEN(articuloRepository);

            IList<ArticuloEN> listaArticulos = articuloCEN.DameTodos(0, -1);
            IList<SelectListItem> articulosItems = new List<SelectListItem>();

            foreach (ArticuloEN articulo in listaArticulos)
            {
                articulosItems.Add(new SelectListItem { Text = articulo.Id.ToString(), Value = articulo.Id.ToString() });
            }

            ViewData["articulosItems"] = articulosItems;

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
                compraCEN.CrearCompra(comp.Fecha, comp.IdComprador, comp.IdArticulo, comp.PrecioTotal, comp.FechaCaducidad, false);
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
