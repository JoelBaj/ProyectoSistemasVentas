using Microsoft.AspNetCore.Mvc;
using Proyecto_Venta_Productos_Lacteos.Models.CRUDs;
using Microsoft.AspNetCore.Authorization;
using Proyecto_Venta_Productos_Lacteos.Models.ViewModels;
using Proyecto_Venta_Productos_Lacteos.Models;
using Proyecto_Venta_Productos_Lacteos.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Venta_Productos_Lacteos.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearCliente(ClienteViewModel model)
        {
            CRUDClientes crudClientes = new CRUDClientes();
            Console.WriteLine("RESPUESTA A INGRESO DE CLIENTE: " + crudClientes.create(model));
            crudClientes = null;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EliminarCliente(int botonEliminar)
        {
            CRUDClientes crudClientes = new CRUDClientes();
            Console.WriteLine("RESPUESTA DE ELIMINACIÓN DE CLIENTE: " + crudClientes.delete(botonEliminar));
            crudClientes = null;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditarCliente(int botonEditar)
        {
            CRUDClientes crudClientes = new CRUDClientes();

            Cliente cliente = crudClientes.read_by_code(botonEditar);

            ClienteViewModel model = new ClienteViewModel();

            if(cliente != null)
            {
                model.cod_clientes = cliente.cod_clientes;
                model.nombre = cliente.nombre;
                model.apellido = cliente.apellido;
                model.cedula = cliente.cedula;
                model.email = cliente.email;
                model.telefono = cliente.telefono;
                model.direccion = cliente.direccion;
            }

            Console.WriteLine("RESPUESTA DE SELECCION DE CLIENTE: " + (cliente != null ? true : false));
            crudClientes = null;

            return View("Index", model);
        }

        [HttpPost]
        public IActionResult ActualizarCliente(ClienteViewModel model)
        {
            CRUDClientes crudClientes = new CRUDClientes();
            Console.WriteLine("RESPUESTA DE ACTUALIZACIÓN DE CLIENTE: " + crudClientes.update(model));
            crudClientes = null;

            Console.WriteLine(model.ToString());

            return RedirectToAction("Index");
        }
    }
}
