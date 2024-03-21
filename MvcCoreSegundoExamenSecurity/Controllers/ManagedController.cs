using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MvcCoreSegundoExamenSecurity.Models;
using MvcCoreSegundoExamenSecurity.Repositories;
using System.Numerics;
using System.Security.Claims;

namespace MvcCoreSegundoExamenSecurity.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryExamen repo;
        public ManagedController(RepositoryExamen repo)
        {
            this.repo = repo;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            Usuario usuario = await this.repo.LoginAsync(email, password);

            if (usuario != null)
            {
                ClaimsIdentity identity =
                    new ClaimsIdentity(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        ClaimTypes.Name, ClaimTypes.Role);
                Claim claimName = new Claim(
                    ClaimTypes.Name, usuario.Email);
                identity.AddClaim(claimName);

                Claim claimNombre = new Claim("Nombre", usuario.Nombre);
                identity.AddClaim(claimNombre);
                
                Claim claimApellido = new Claim("Apellidos", usuario.Apellido);
                identity.AddClaim(claimApellido);

                Claim claimFoto = new Claim("Foto", usuario.Foto);
                identity.AddClaim(claimFoto);


                Claim claimId = new Claim("IdUsuario", usuario.IdUsuario.ToString());
                identity.AddClaim(claimId);


                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal);

                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();

                return RedirectToAction(action, controller);
            }
            else
            {
                ViewData["ERROR"] = "Usuario o contrraseña incorrectos";
                return View();
            }
        }

        public IActionResult ErrorAcceso()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
