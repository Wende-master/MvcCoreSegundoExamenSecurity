using Microsoft.AspNetCore.Mvc;
using MvcCoreSegundoExamenSecurity.Models;
using MvcCoreSegundoExamenSecurity.Repositories;

namespace MvcCoreSegundoExamenSecurity.ViewComponents
{
    public class MenuGenerosViewComponent: ViewComponent
    {
        private RepositoryExamen repo;

        public MenuGenerosViewComponent(RepositoryExamen repo)
        {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Genero> generos = await this.repo
                .GetGeneroAsync();
            return View(generos);
        }
    }
}
