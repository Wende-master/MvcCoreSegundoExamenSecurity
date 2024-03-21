using Microsoft.AspNetCore.Mvc;
using MvcCoreSegundoExamenSecurity.Extensions;
using MvcCoreSegundoExamenSecurity.Filters;
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

        public async Task<IActionResult> Detalles(int idlibro, int? idComprar)
        {

            Libro libro = await this.repo.FindLibroAsync(idlibro);

            if (idComprar != null)
            {
                List<int> librosCesta;

                if (HttpContext.Session.GetString("CARRITO") != null)
                {
                    librosCesta =
                        HttpContext.Session.GetObject<List<int>>("CARRITO");
                }
                else
                {
                    librosCesta = new List<int>();
                }
                librosCesta.Add(idComprar.Value);

                HttpContext.Session.SetObject("CARRITO", librosCesta);
                ViewData["MENSAJE"] = "Añadido al carrito!";
            }

            return View(libro);
        }

        [AuthorizeLibros]
        public async Task<IActionResult> Carrito(int? ideliminar)
        {
            List<int> ids =
                HttpContext.Session.GetObject<List<int>>("CARRITO");

            if (ids != null)
            {
                if (ideliminar != null)
                {
                    ids.Remove(ideliminar.Value);
                    if (ids.Count() == 0)
                    {
                        HttpContext.Session.Remove("CARRITO");
                    }
                    else
                    {
                        HttpContext.Session.SetObject("CARRITO", ids);
                    }
                }

                List<Libro> libros = await
                    this.repo.GetLibrosSessionAsync(ids);
                return View(libros);
            }
            else
            {
                ViewData["MENSAJE"] = "No hay artículos seleccionados";
                return View();
            }
        }

        [AuthorizeLibros]
        public IActionResult PedidosUsuario()
        {
            int idUsuario = int.Parse(HttpContext.User.FindFirst("IdUsuario").Value);
            List<VistaPedido> pedidos =
                this.repo.GetVistaPedidosUsuario(idUsuario);
            return View(pedidos);
        }

        [AuthorizeLibros]
        public IActionResult FinalizarCompra(int idUsuario)
        {
            List<int> ids =
                HttpContext.Session.GetObject<List<int>>("CARRITO");

            int idfactura = this.repo.GetNumFactura();

            foreach (int id in ids)
            {
                this.repo.InsertarPedido(idfactura, id, idUsuario);
            }
            HttpContext.Session.Remove("CARRITO");
            return RedirectToAction("PedidosUsuario");
        }
    }
}
