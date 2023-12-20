using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Administrador")]
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

            // Obtenemos el nombre del objeto para mostrarlo
            ViewData["nom_art"] = compraEN.Articulo.Nombre;

            SessionClose();
            return View(compraVM);
        }

        // GET: CompraController/Create
        public ActionResult Create(int id, float precio, bool regalo)
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

            //-----------------------------------------------------------------------------------
            int idUsuario = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            UsuarioEN p_usuario = usuarioCEN.DamePorOID(idUsuario);
            ArticuloEN p_articulo = articuloCEN.DamePorOID(id);


            CompraEN compraEN = new CompraEN(1, DateTime.Now, p_usuario, p_articulo, precio, false, null);

            // ------------------------------------------------------------------------------------
            // Ver el precio y el saldo para ver lo de los botones
            ViewData["suficiente"] = (p_usuario.Cartera > precio);

            // Ver el nombre del articulo
            ViewData["nom_art"] = p_articulo.Nombre;

            // Ver si se regala el articulo
            ViewData["regalo"] = regalo;

            // Vista final
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

                // Precio descontado
                float precio_descontado = comp.PrecioTotal;
                if (desc != null) { precio_descontado -= (float)usu.PuntosNevula / 100; }

                // Compranos articulo
                UsuarioCP usuarioCP = new(new SessionCPNHibernate());
                CompraCP compraCP = new(new SessionCPNHibernate());

                // Vemos si se va a comprar o regalar
                int regalo = Int32.Parse(Request.Form["regalo"]);

                int result_comp = 0; 
                if (regalo == 0) {

                    // Compramos el articulo
                    usuarioCP.ComprarArticulo(usu.Id, comp.IdArticulo, (desc != null));

                    // Creamos compra
                    result_comp = compraCEN.CrearCompra(DateTime.Now, comp.IdComprador, comp.IdArticulo, precio_descontado, false);

                } else {

                    // Creamos compra
                    result_comp = compraCEN.CrearCompra(DateTime.Now, comp.IdComprador, comp.IdArticulo, precio_descontado, true);

                    // Regalamos
                    string nom_usu_r = Request.Form["nom_usu_r"];
                    UsuarioEN usu_r = usuarioCEN.DamePorNombre(nom_usu_r).First();
                    compraCP.Regalar(result_comp, usu_r.Id);

                }
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
