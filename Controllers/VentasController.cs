using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Venta_Productos_Lacteos.Models;
using Proyecto_Venta_Productos_Lacteos.Models.CRUDs;
using Proyecto_Venta_Productos_Lacteos.Models.ViewModels;

namespace Proyecto_Venta_Productos_Lacteos.Controllers
{
    [Authorize]
    public class VentasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearVenta(VentaViewModel model)
        {
            CRUDVentas crudVentas = new CRUDVentas();
            Console.WriteLine("RESPUESTA A INGRESO DE VENTA: " + crudVentas.create(model));
            crudVentas = null;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EliminarVenta(int botonEliminar)
        {
            CRUDVentas crudVentas = new CRUDVentas();
            Console.WriteLine("RESPUESTA DE ELIMINACIÓN DE VENTA: " + crudVentas.delete(botonEliminar));
            crudVentas = null;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditarVenta(int botonEditar) 
        {
            CRUDVentas crudVentas = new CRUDVentas();

            Venta venta = crudVentas.read_by_code(botonEditar);

            VentaViewModel model = new VentaViewModel();

            if (venta != null)
            {
                model.cod_venta = venta.cod_venta;
                model.cod_cliente = venta.cliente.cod_clientes;
                model.cod_producto = venta.producto.cod_producto;
                model.cantidad = venta.cantidad;
                model.fecha = venta.fecha;
                model.forma_pago = venta.forma_pago;
                model.total = venta.total;
            }

            Console.WriteLine("RESPUESTA DE SELECCION DE VENTA: " + (venta != null ? true : false));
            crudVentas = null;

            return View("Index", model);
        }

        [HttpPost]
        public IActionResult ActualizarVenta(VentaViewModel model)
        {

            CRUDVentas crudVentas = new CRUDVentas();
            Console.WriteLine("RESPUESTA DE ACTUALIZACIÓN DE VENTA: " + crudVentas.update(model));
            crudVentas = null;

            return RedirectToAction("Index");
        }
    }
}
