using Proyecto_Venta_Productos_Lacteos.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Venta_Productos_Lacteos.Models.ViewModels
{
    public class VentaViewModel
    {
        
        public int cod_venta { get; set; }

        
        public int cod_cliente { get; set; }

        
        public int cod_producto { get; set; }

        
        public int cantidad { get; set; }

        
        public DateTime fecha { get; set; }

        
        public string forma_pago { get; set; }

        
        public double total { get; set; }

        public override string ToString()
        {
            return "Codigo = " + cod_venta + ", Cliente = " + cod_cliente + ", Producto = " + cod_producto + ", Cantidad = " + cantidad + 
                ", Fecha = " + fecha.ToString() + ", Forma de Pago = " + forma_pago + ", Total = " + total + "";
        }
    }
}
