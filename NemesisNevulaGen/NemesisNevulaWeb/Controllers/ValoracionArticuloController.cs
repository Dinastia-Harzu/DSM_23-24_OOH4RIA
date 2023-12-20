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

        // GET: ValoracionArticuloController/Create
        [Authorize]
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: ValoracionArticuloController/Create/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public ActionResult Create(int id, ValoracionArticuloVM valoracionArticulo)
        {
            
            try
            {
                int idUserLogued = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                new ValoracionArticuloCEN(new ValoracionArticuloRepository()).CrearValoracion(
                    valoracionArticulo.Valoracion,
                    id,
                    idUserLogued
                );
                return RedirectToAction(nameof(Index));
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
