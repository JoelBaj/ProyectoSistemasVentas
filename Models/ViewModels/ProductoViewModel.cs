using Proyecto_Venta_Productos_Lacteos.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Venta_Productos_Lacteos.Models.ViewModels
{
    public class ProductoViewModel
    {
        [Required]
        [CodValidation]
        public int cod_producto { get; set; }

        [Required]
        [MaxLength(45)]
        public string nombre { get; set; }

        [Required]
        [CodValidation]
        public int stock { get; set; }

        [Required]
        public double precio_unitario { get; set; }

        [Required]
        [CodValidation]
        public int cod_categoria { get; set; }

        public override string ToString()
        {
            return "Codigo Producto = " + cod_producto + ", Nombre = " + nombre + ", Stock = " + stock +
                ", Precio Unitario = " + precio_unitario + ", Codigo Categoria = " + cod_categoria + "";
        }
    }
}
