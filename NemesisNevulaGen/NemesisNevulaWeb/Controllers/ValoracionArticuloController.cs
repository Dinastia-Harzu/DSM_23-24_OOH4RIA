using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Completion;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CP.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.Infraestructure.CP;
using NemesisNevulaGen.Infraestructure.EN.NemesisNevula;
using NemesisNevulaGen.Infraestructure.Repository.NemesisNevula;
using NemesisNevulaWeb.Assemblers;
using NemesisNevulaWeb.Models;
using System.Security.Claims;

namespace NemesisNevulaWeb.Controllers
{
    public class ValoracionArticuloController : BasicController
    {
        // GET: ValoracionArticuloController
        [Authorize(Roles = "Administracion")]
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

        // GET: ValoracionArticuloController/Create/:valoracion/:usuario/:articulo
        [Authorize]
        public ActionResult Create(int valoracion, int usuario, int articulo)
        {
            try
            {
                SessionInitialize();

                UsuarioRepository usuarioRepository = new(session);
                UsuarioCEN usuarioCEN = new(usuarioRepository);
                UsuarioEN usuarioEN = usuarioCEN.DamePorOID(usuario);
                List<ValoracionArticuloEN> listaOriginal = new(usuarioEN.ValoracionArticulo);
                
                UsuarioCP usuarioCP = new(new SessionCPNHibernate());
                usuarioCP.ValorarArticulo(usuario, articulo, valoracion);

                SessionClose();
                SessionInitialize();

                ValoracionArticuloRepository valoracionArticuloRepository = new(session);
                ValoracionArticuloCEN valoracionArticuloCEN = new(valoracionArticuloRepository);

                usuarioRepository = new(session);
                usuarioCEN = new(usuarioRepository);
                usuarioEN = usuarioCEN.DamePorOID(usuario);

                List<ValoracionArticuloEN> listaActualizada = new(usuarioEN.ValoracionArticulo);
                int idValoracionArticulo = -1;

                List<int> listaIDsOriginal = new();
                foreach(ValoracionArticuloEN val in listaOriginal)
                {
                    listaIDsOriginal.Add(val.Id);
                }
                List<int> listaIDsActualizada = new();
                foreach(ValoracionArticuloEN val in listaActualizada)
                {
                    listaIDsActualizada.Add(val.Id);
                }
                foreach(int id in listaIDsActualizada)
                {
                    if(!listaIDsOriginal.Contains(id))
                    {
                        idValoracionArticulo = id;
                        break;
                    }
                }
                if(idValoracionArticulo == -1)
                {
                    throw new Exception("No existe la valoración");
                }

                ValoracionArticuloEN valoracionArticuloEN = valoracionArticuloCEN.DamePorOID(idValoracionArticulo);
                ValoracionArticuloVM valoracionArticulo = new ValoracionArticuloAssembler().EN2VM(valoracionArticuloEN);

                SessionClose();

                return RedirectToAction("Details", "Articulo", new { id = articulo });
            } catch
            {
                return RedirectToAction("Details", "Articulo", new { id = articulo });
            }
        }

        // POST: ValoracionArticuloController/Create/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public ActionResult Create(int id, ValoracionArticuloVM valoracionArticulo)
        {
            try
            {
                // Nada que ver por aquí                
                return RedirectToAction("Details", "Articulo", new { id = id });
            }
            catch
            {
                return View();
            }
        }

        // GET: ValoracionArticuloController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            SessionInitialize();

            ValoracionArticuloRepository valoracionArticuloRepository= new(session);
            ValoracionArticuloCEN valoracionArticuloCEN = new(valoracionArticuloRepository);

            ValoracionArticuloEN valoracionArticuloEN = valoracionArticuloCEN.DamePorOID(id);

            // Validamos que el usuario logueado no intente editar la valoración de otro
            int idUser = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if(valoracionArticuloEN.Usuario.Id != idUser)
                RedirectToAction("Index", "Home");

            ValoracionArticuloVM valoracionArticulo = new ValoracionArticuloAssembler().EN2VM(valoracionArticuloEN);
            SessionClose();
            return View(valoracionArticulo);
        }

        // POST: ValoracionArticuloController/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ValoracionArticuloVM valoracionArticulo)
        {
            try
            {
                SessionInitialize();

                ValoracionArticuloRepository valoracionArticuloRepository = new(session);
                ValoracionArticuloCEN valoracionArticuloCEN = new(valoracionArticuloRepository);

                ValoracionArticuloEN valoracionArticuloEN = valoracionArticuloCEN.DamePorOID(id);

                // Validamos que el usuario logueado no intente editar la valoración de otro
                int idUser = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (valoracionArticuloEN.Usuario.Id != idUser)
                    RedirectToAction("Index", "Home");

                new ValoracionArticuloCEN(new ValoracionArticuloRepository()).ModificarValoracion(
                    id,
                    valoracionArticulo.Valoracion
                );

                SessionClose();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ValoracionArticuloController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            SessionInitialize();

            ValoracionArticuloRepository valoracionArticuloRepository = new(session);
            ValoracionArticuloCEN valoracionArticuloCEN = new(valoracionArticuloRepository);

            ValoracionArticuloEN valoracionArticuloEN = valoracionArticuloCEN.DamePorOID(id);

            // Validamos que el usuario logueado no intente editar la valoración de otro
            int idUser = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (valoracionArticuloEN.Usuario.Id != idUser)
                RedirectToAction("Index", "Home");

            new ValoracionArticuloCEN(new ValoracionArticuloRepository()).BorrarValoracion(id);

            SessionClose();
            return RedirectToAction(nameof(Index));
        }

        // POST: ValoracionArticuloController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                SessionInitialize();

                ValoracionArticuloRepository valoracionArticuloRepository = new(session);
                ValoracionArticuloCEN valoracionArticuloCEN = new(valoracionArticuloRepository);

                ValoracionArticuloEN valoracionArticuloEN = valoracionArticuloCEN.DamePorOID(id);

                // Validamos que el usuario logueado no intente editar la valoración de otro
                int idUser = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (valoracionArticuloEN.Usuario.Id != idUser)
                    RedirectToAction("Index", "Home");

                SessionClose();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
