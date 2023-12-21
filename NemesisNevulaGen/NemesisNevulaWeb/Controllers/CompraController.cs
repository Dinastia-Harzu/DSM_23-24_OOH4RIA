using Microsoft.AspNetCore.Authentication;
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
            if (User.Identity.IsAuthenticated) actualizarEstado();
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

        /*
        // GET: CompraController/Create
        public ActionResult Create(int id, float precio, bool regalo)
        {
            if (User.Identity.IsAuthenticated) actualizarEstado();
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

            if (p_usuario.Cartera > precio)
            {
                if (User.Identity.IsAuthenticated) actualizarEstado();
            }
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
                if (User.Identity.IsAuthenticated) actualizarEstado();
                CompraRepository compraRepository = new CompraRepository();
                CompraCEN compraCEN = new CompraCEN(compraRepository);

                string desc = Request.Form["desc"];

                UsuarioRepository usuarioRepository = new UsuarioRepository();
                UsuarioCEN usuarioCEN = new UsuarioCEN(usuarioRepository);
                UsuarioEN usu = usuarioCEN.DamePorOID(Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
                ArticuloRepository articuloRepository = new ArticuloRepository();
                ArticuloCEN articuloCEN = new ArticuloCEN(articuloRepository);

                // Precio descontado
                float precio_descontado = comp.PrecioTotal;
                if (desc != null) { precio_descontado -= (float)usu.PuntosNevula / 100; }

                // Compranos articulo
                UsuarioCP usuarioCP = new(new SessionCPNHibernate());
                CompraCP compraCP = new(new SessionCPNHibernate());


                // Vemos si se va a comprar o regalar
                int regalo = Int32.Parse(Request.Form["regalo"]);

                int result_comp = 0;
                if (regalo == 0)
                {

                    ViewData["regalo"] = false;
                    // Compramos el articulo
                    usuarioCP.ComprarArticulo(usu.Id, comp.IdArticulo, (desc != null));

                    // Creamos compra
                    result_comp = compraCEN.CrearCompra(DateTime.Now, comp.IdComprador, comp.IdArticulo, precio_descontado, false);

                }
                else
                {
                    ViewData["regalo"] = true;
                    // Regalamos
                    string nom_usu_r = Request.Form["nom_usu_r"];
                    UsuarioEN usu_r = usuarioCEN.DamePorNombre(nom_usu_r).First();

                    // Creamos compra
                    if (!(usu_r is AdministradorEN))
                    {
                        result_comp = compraCEN.CrearCompra(DateTime.Now, comp.IdComprador, comp.IdArticulo, precio_descontado, true);
                        compraCP.Regalar(result_comp, usu_r.Id);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                IList<ArticuloEN> listaArticulos = articuloCEN.DameTodos(0, -1);
                IList<SelectListItem> articulosItems = new List<SelectListItem>();

                foreach (ArticuloEN articulo in listaArticulos)
                {
                    articulosItems.Add(new SelectListItem { Text = articulo.Id.ToString(), Value = articulo.Id.ToString() });
                }

                IList<UsuarioEN> listaUsuarios = usuarioCEN.DameTodos(0, -1);
                IList<SelectListItem> usuariosItems = new List<SelectListItem>();

                foreach (UsuarioEN usuario in listaUsuarios)
                {
                    usuariosItems.Add(new SelectListItem { Text = usuario.Id.ToString(), Value = usuario.Id.ToString() });
                }

                ViewData["usuariosItems"] = usuariosItems;
                ViewData["articulosItems"] = articulosItems;

                return RedirectToAction("Details", new { id = result_comp });


            }
            catch
            {
                return View();
            }
        }
        */
       
        // GET: CompraController/Create
        public ActionResult Create(int id, float precio, bool regalo)
        {
            if (User.Identity.IsAuthenticated) actualizarEstado();

            UsuarioRepository usuarioRepository = new UsuarioRepository();
            UsuarioCEN usuarioCEN = new UsuarioCEN(usuarioRepository);

            ArticuloRepository articuloRepository = new();
            ArticuloCEN articuloCEN = new(articuloRepository);

            // Recogemos el usuario logueado
            int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            UsuarioEN userEN = usuarioCEN.DamePorOID(idUserLogued);

            ArticuloEN artEN = articuloCEN.DamePorOID(id);

            CompraEN compraEN = new CompraEN(-1, DateTime.Now, userEN, artEN, precio, false, null);     // Compra auxiliar que no viaja a la BD (Necesario para el VM)

            // ------------------------------------------------------------------------------------
            // Ver el precio y el saldo para ver lo de los botones
            ViewData["suficiente"] = (userEN.Cartera > precio);

            if (userEN.Cartera > precio)
            {
                if (User.Identity.IsAuthenticated) actualizarEstado();
            }
            // Ver el nombre del articulo
            ViewData["nom_art"] = artEN.Nombre;

            // Ver si se regala el articulo
            ViewData["regalo"] = regalo;

            // Recogemos todos los usuario de la BD
            IList<UsuarioEN> listUsersEN = usuarioCEN.DameTodos(0, -1);

            // Creamos una lista de select items y se los inyectamos
            IList<SelectListItem> listUsersSLI = new List<SelectListItem>();
            foreach (UsuarioEN user in listUsersEN)
            {
                listUsersSLI.Add(new SelectListItem
                {
                    Text = user.Nombre,
                    Value = user.Nombre
                });
            }

            ViewData["listUsers"] = listUsersSLI;

            // Vista final
            return View(new CompraAssembler().ConvertirENToViewModel(compraEN));
        }

        // POST: CompraController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, CompraVM comp, string usuarioReceptor)
        {

            try
            {
                if (User.Identity.IsAuthenticated) actualizarEstado();

                CompraRepository compraRepository = new CompraRepository();
                CompraCEN compraCEN = new CompraCEN(compraRepository);

                UsuarioRepository usuarioRepository = new UsuarioRepository();
                UsuarioCEN usuarioCEN = new UsuarioCEN(usuarioRepository);

                UsuarioEN usu = usuarioCEN.DamePorOID(Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

                ArticuloRepository articuloRepository = new ArticuloRepository();
                ArticuloCEN articuloCEN = new ArticuloCEN(articuloRepository);

                // Declaramos los CPs
                UsuarioCP usuarioCP = new(new SessionCPNHibernate());
                CompraCP compraCP = new(new SessionCPNHibernate());

                bool aplicarDescuento = false;
                string desc = Request.Form["desc"];

                if (!string.IsNullOrEmpty(desc))
                {
                    if (desc == "on") aplicarDescuento = true;
                    else aplicarDescuento = false;
                }
               
                /*
                // Recogemos el usuario logueado
                int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                UsuarioEN usu = usuarioCEN.DamePorOID(idUserLogued);
                */

                // Recogemos el articulo a comprar
                ArticuloEN art = articuloCEN.DamePorOID(id);

                // Vemos si se va a comprar o regalar
                int regalo = int.Parse(Request.Form["regalo"]);

                // Compramos el articulo
                    int idCompra = usuarioCP.ComprarArticulo(usu.Id, id, aplicarDescuento);

                if (regalo == 0)
                    ViewData["regalo"] = false;
                else
                {
                    ViewData["regalo"] = true;

                    // Recogemos el usuario objetivo
                    string userObjetivoNombre = usuarioReceptor;
                    UsuarioEN userObjetivo = usuarioCEN.DamePorNombre(userObjetivoNombre).First();

                    // Si no es Admin, compramos y regalamos el artículo
                    if (!(userObjetivo is AdministradorEN))
                        compraCP.Regalar(idCompra, userObjetivo.Id);      
                }

                return RedirectToAction("Details", "Articulo", new { id = art.Id });
            }
            catch(Exception e)
            {
                Console.WriteLine("[POST] Compra/Create ERROR FATAL: " + e.Message);

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
