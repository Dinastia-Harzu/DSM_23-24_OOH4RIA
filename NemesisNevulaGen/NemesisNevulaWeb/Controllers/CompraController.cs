using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public ActionResult Create(int id, float precio)
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

            Console.WriteLine(id);
            Console.WriteLine(precio);

            //-----------------------------------------------------------------------------------
            int idUsuario = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            UsuarioEN p_usuario = usuarioCEN.DamePorOID(idUsuario);
            ArticuloEN p_articulo = articuloCEN.DamePorOID(id);


            CompraEN compraEN = new CompraEN(1, DateTime.Now, p_usuario, p_articulo, precio, false, null);

            // ViewData["id_art"] = id;

            return View(new CompraAssembler().ConvertirENToViewModel(compraEN));

            //return View();
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

                string desc = Request.Form["desc"];

                UsuarioRepository usuarioRepository = new UsuarioRepository();
                UsuarioCEN usuarioCEN = new UsuarioCEN(usuarioRepository);
                UsuarioEN usu = usuarioCEN.DamePorOID(Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

                // Compranos articulo
                UsuarioCP usuarioCP = new(new SessionCPNHibernate());
                usuarioCP.ComprarArticulo(usu.Id, comp.IdArticulo, (desc != null));

                // Creamos compra
                int result_comp = compraCEN.CrearCompra(comp.Fecha, comp.IdComprador, comp.IdArticulo, comp.PrecioTotal, false);
                return RedirectToAction("Details", new {id = result_comp});

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
                compraCEN.ModificarCompra(comp.Id, comp.Fecha, comp.PrecioTotal, false);

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
