using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Net.Http.Headers;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.Enumerated.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.Utils;
using NemesisNevulaGen.Infraestructure.Repository.NemesisNevula;
using NemesisNevulaWeb.Assemblers;
using NemesisNevulaWeb.Models;
using NHibernate.Id.Insert;
using System.Collections.Generic;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace NemesisNevulaWeb.Controllers
{
    public class NoticiaController : BasicController
    {

        private readonly IWebHostEnvironment _webHost;

        public NoticiaController(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }
        // GET: NoticiaController
        public ActionResult Index()
        {
            
            SessionInitialize();
            NoticiaRepository noticiaRepository = new();
            NoticiaCEN noticiaCEN = new(noticiaRepository);
            IList<NoticiaEN> listaEN = noticiaCEN.DameTodos(0, -1);
            IEnumerable<NoticiaVM> listaNoticias = new NoticiaAssembler().ListEN2VM(listaEN).ToList();
            ViewBag.CurrentPage = "TodasNoticias";
            SessionClose();
            return View(listaNoticias);
        }

        // GET: NoticiaController/Details/5
        public ActionResult Details(int id)
        {
            
            SessionInitialize();
            NoticiaRepository noticiaRepository = new(session);
            NoticiaCEN noticiaCEN = new(noticiaRepository);

            NoticiaEN noticiaEN = noticiaCEN.DamePorOID(id);
            NoticiaVM noticiaVM = new NoticiaAssembler().EN2VM(noticiaEN);

            IList<NoticiaEN> listaEN = noticiaCEN.DameTodos(0, -1);
            IEnumerable<NoticiaVM> listaNoticias = new NoticiaAssembler().ListEN2VM(listaEN).ToList();
            

            SessionClose();

            var viewModel = new Tuple<NoticiaVM, IEnumerable<NoticiaVM>>(noticiaVM, listaNoticias);
            return View(viewModel);
        }

        // GET: NoticiaController/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.CurrentPage = "CrearNoticia";
            return View();
        }

        // POST: NoticiaController/Create
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(NoticiaVM noticia)
        {
            ViewBag.CurrentPage = "CrearNoticia";
            Console.WriteLine("HOLIWIS" + noticia.Foto2);
            NoticiaRepository notiRepo = new();
            NoticiaCEN notiCEN = new(notiRepo);
            
            // Validamos el token del usuario
           
            try
            {
              
                    // Manejamos la subida de foto de perfil
                    string fileName = "", path = "";
                    if (noticia.Foto2 != null && noticia.Foto2.Length > 0)
                    {
                        Console.WriteLine( "DENTRO DEL IF" + noticia.Foto2);
                        fileName = Path.GetFileName(noticia.Foto2.FileName).Trim();

                        string directory = _webHost.WebRootPath + "/css/estilos/imagenes/";
                        path = Path.Combine(directory, fileName);

                        if (!Directory.Exists(directory))
                            Directory.CreateDirectory(directory);

                        using (var stream = System.IO.File.Create(path))
                        {
                            await noticia.Foto2.CopyToAsync(stream);
                        }
                    }
                    else
                    {
                    Console.WriteLine("DENTRO DEL ELSE" + noticia.Foto2);
                    fileName = "noImage.jpg";
                    }

                    fileName = "/css/estilos/imagenes/" + fileName;

                  

                    notiCEN.CrearNoticia(noticia.Descripcion, noticia.EsPublicada, noticia.Titulo, fileName);

                    return RedirectToAction(nameof(Index));
                
            }
            catch (Exception e)
            {
               
                return View();
            }
        }

        // GET: NoticiaController/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int id)
        {
            
            SessionInitialize();
            NoticiaRepository noticiaRepository = new(session);
            NoticiaCEN noticiaCEN = new(noticiaRepository);
            NoticiaEN noticiaEN = noticiaCEN.DamePorOID(id);
            NoticiaVM noticia = new NoticiaAssembler().EN2VM(noticiaEN);
            SessionClose();
            return View(noticia);
        }

        // POST: NoticiaController/Edit/5
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, NoticiaVM noticia)
        {
            Console.WriteLine("HOLIWIS" + noticia.Foto2);
            Console.WriteLine("HOLIWIS DOS" + noticia.Foto);
            NoticiaRepository notiRepo = new();
            NoticiaCEN notiCEN = new(notiRepo);
            ViewBag.CurrentPage = "CrearNoticia";
            // Validamos el token del usuario

            try
            {
                
                // Manejamos la subida de foto de perfil
                string fileName = "", path = "";
                if (noticia.Foto2 != null && noticia.Foto2.Length > 0)
                {
                    string midirectorio = "/css/estilos/imagenes/";
                    Console.WriteLine("HOLIWIS22" + noticia.Foto2);
                    Console.WriteLine(noticia.Foto2);
                    fileName = Path.GetFileName(noticia.Foto2.FileName).Trim();

                    string directory = _webHost.WebRootPath + midirectorio;
                    path = Path.Combine(directory, fileName);

                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    
                        using (var stream = System.IO.File.Create(path))
                        {
                            await noticia.Foto2.CopyToAsync(stream);
                        }
                    
                    fileName = midirectorio + fileName;
                }
                else
                {
                    Console.WriteLine("ME MATO YA" + noticia.Foto2);
                    fileName = noticia.Foto;
                }

                

                Console.WriteLine("antes de modificar" + noticia.Foto2);
                notiCEN.ModificarNoticia(id, noticia.Descripcion, noticia.EsPublicada, noticia.Titulo, fileName);
                Console.WriteLine("DESPUES de modificar" + noticia.Foto2);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception e)
            {
                Console.WriteLine("antes de modificar" + e.Message +noticia.Foto2);
                return View();
            }
        }

        // GET: NoticiaController/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int id)
        {
            
            new NoticiaCEN(new NoticiaRepository()).BorrarNoticia(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: NoticiaController/Delete/5
        [Authorize(Roles = "Administrador")]
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
