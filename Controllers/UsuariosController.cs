using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Venta_Productos_Lacteos.Models;
using Proyecto_Venta_Productos_Lacteos.Models.CRUDs;
using Proyecto_Venta_Productos_Lacteos.Models.ViewModels;
using System.Reflection;

namespace Proyecto_Venta_Productos_Lacteos.Controllers
{
    [Authorize(Roles = "Administrador, Supervisor")]
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CrearUsuario(UsuarioViewModel model)
        {
            CRUDUsuarios crudUsuarios = new CRUDUsuarios();
            Console.WriteLine("RESPUESTA A INGRESO DE USUARIO: " + crudUsuarios.create(model));
            crudUsuarios = null;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EliminarUsuario(int botonEliminar)
        {
            CRUDUsuarios crudUsuarios = new CRUDUsuarios();
            Console.WriteLine("RESPUESTA DE ELIMINACIÓN DE USUARIO: " + crudUsuarios.delete(botonEliminar));
            crudUsuarios = null;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditarUsuario(int botonEditar)
        {
            CRUDUsuarios crudUsuarios = new CRUDUsuarios();

            Usuario usuario = crudUsuarios.read_by_code(botonEditar);

            UsuarioViewModel model = new UsuarioViewModel();

            if(usuario != null)
            {
                model.cod_usuario = usuario.cod_usuario;
                model.nombre = usuario.nombre;
                model.usuario = usuario.usuario;
                model.contrasena = usuario.contrasena;
                model.cod_rol = usuario.rol.cod_rol;
            }

            Console.WriteLine("RESPUESTA DE SELECCION DE USUARIO: " + (usuario != null ? true : false));
            crudUsuarios = null;

            return View("Index", model);
        }

        public IActionResult ActualizarUsuario(UsuarioViewModel model)
        {
            CRUDUsuarios crudUsuarios = new CRUDUsuarios();
            Console.WriteLine("RESPUESTA DE ACTUALIZACIÓN DE USUARIO: " + crudUsuarios.update(model));
            crudUsuarios = null;

            return RedirectToAction("Index");
        }
    }
}
