namespace Proyecto_Venta_Productos_Lacteos.Models
{
    public class Producto
    {
        public int cod_producto { get; set; }
        public string nombre { get; set; }
        public int stock { get; set; }
        public double precio_unitario { get; set; }
        public CategoriaProducto categoria { get; set; }

        public override string ToString()
        {
            return "cod_producto = " + cod_producto + ", nombre = " + nombre + ", stock = " + stock + 
                ", precio_unitario = " + precio_unitario + ", categoría = " + categoria.ToString() + "";
        }
    }
}
