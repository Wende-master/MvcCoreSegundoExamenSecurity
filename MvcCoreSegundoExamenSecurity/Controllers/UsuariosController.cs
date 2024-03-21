using Microsoft.AspNetCore.Mvc;
using MvcCoreSegundoExamenSecurity.Filters;
using MvcCoreSegundoExamenSecurity.Repositories;

namespace MvcCoreSegundoExamenSecurity.Controllers
{
    public class UsuariosController : Controller
    {
        private RepositoryExamen repo;
        public UsuariosController(RepositoryExamen repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AuthorizeLibros]
        public async Task<IActionResult> PerfilUsuario()
        {
            return View();
        }
    }
}
