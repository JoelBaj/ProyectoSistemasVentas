namespace Proyecto_Venta_Productos_Lacteos.Models
{
    public class Venta
    {
        public int cod_venta { get; set; }
        public Cliente cliente { get; set; }
        public Producto producto { get; set; }
        public int cantidad { get; set; }
        public DateTime fecha { get; set; }
        public string forma_pago { get; set; }
        public double total { get; set; }
    }
}
