using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Venta_Productos_Lacteos.Models.CRUDs;
using System.Reflection;
using Proyecto_Venta_Productos_Lacteos.Models.ViewModels;
using Proyecto_Venta_Productos_Lacteos.Models;
using Proyecto_Venta_Productos_Lacteos.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Venta_Productos_Lacteos.Controllers
{
    [Authorize]
    public class ProductosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearProducto(ProductoViewModel model)
        {
            CRUDProductos crudProductos = new CRUDProductos();
            Console.WriteLine("RESPUESTA A INGRESO DE PRODUCTO: " + crudProductos.create(model));
            crudProductos = null;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EliminarProducto(int botonEliminar)
        {
            CRUDProductos crudProductos = new CRUDProductos();
            Console.WriteLine("RESPUESTA DE ELIMINACIÓN DE PRODUCTO: " + crudProductos.delete(botonEliminar));
            crudProductos = null;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditarProducto(int botonEditar)
        {
            CRUDProductos crudProductos = new CRUDProductos();

            Producto producto = crudProductos.read_by_code(botonEditar);

            ProductoViewModel model = new ProductoViewModel();

            if(producto != null)
            {
                model.cod_producto = producto.cod_producto;
                model.nombre = producto.nombre;
                model.stock = producto.stock;
                model.precio_unitario = producto.precio_unitario;
                model.cod_categoria = producto.categoria.cod_categoria;
            }

            Console.WriteLine("RESPUESTA DE SELECCION DE PRODUCTO: " + (producto != null ? true : false));
            crudProductos = null;

            return View("Index", model);
        }

        [HttpPost]
        public IActionResult ActualizarProducto(ProductoViewModel model)
        {
            CRUDProductos crudProductos = new CRUDProductos();
            Console.WriteLine("RESPUESTA DE ACTUALIZACIÓN DE PRODUCTO: " + crudProductos.update(model));
            crudProductos = null;

            return RedirectToAction("Index");
        }
    }
}
