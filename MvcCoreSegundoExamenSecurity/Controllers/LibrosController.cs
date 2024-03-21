using Microsoft.AspNetCore.Mvc;
using MvcCoreSegundoExamenSecurity.Models;
using MvcCoreSegundoExamenSecurity.Repositories;

namespace MvcCoreSegundoExamenSecurity.Controllers
{
    public class LibrosController : Controller
    {
        private RepositoryExamen repo;
        public LibrosController(RepositoryExamen repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            List<Libro> libros = await this.repo.GetLibrosAsync();
            return View(libros);
        }

        public async Task<IActionResult> LibrosGenero(int idgenero)
        {
            List<Libro> libros = await this.repo.GetLibrosGenero(idgenero);
            return View(libros);
        }
    }
}
