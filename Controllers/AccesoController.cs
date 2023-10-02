using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Venta_Productos_Lacteos.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Proyecto_Venta_Productos_Lacteos.Models.CRUDs;
using Proyecto_Venta_Productos_Lacteos.Models.ViewModels;

namespace Proyecto_Venta_Productos_Lacteos.Controllers
{
    public class AccesoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UsuarioViewModel model)
        {
            
            CRUDUsuarios crudUsuarios = new CRUDUsuarios();
            Usuario usuario = crudUsuarios.ValidarUsuario(model.usuario, model.contrasena);
            crudUsuarios = null;


            if (usuario != null)
            {

                var claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Name, usuario.nombre));
                claims.Add(new Claim("Usuario", usuario.usuario));

                claims.Add(new Claim(ClaimTypes.Role, usuario.rol.nombre));

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                //Console.WriteLine("Usuario no encontrado");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //Console.WriteLine("Usuario encontrado");
                return View();
            }
        }

        public async Task<IActionResult> Salir()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Acceso");
        }
    }
}