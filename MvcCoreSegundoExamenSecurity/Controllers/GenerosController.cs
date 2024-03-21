using Microsoft.AspNetCore.Mvc;
using MvcCoreSegundoExamenSecurity.Models;
using MvcCoreSegundoExamenSecurity.Repositories;

namespace MvcCoreSegundoExamenSecurity.Controllers
{
    public class GenerosController : Controller
    {
        private RepositoryExamen repo;
        public GenerosController(RepositoryExamen repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Generos()
        {
            List<Genero> generos = await this.repo.GetGeneroAsync();    

            return View(generos);
        }
    }
}
