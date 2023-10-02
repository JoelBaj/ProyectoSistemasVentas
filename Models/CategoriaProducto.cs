namespace Proyecto_Venta_Productos_Lacteos.Models
{
    public class CategoriaProducto
    {
        public int cod_categoria { get; set; }
        public string nombre { get; set; }

        public override string ToString()
        {
            return "Cod_Categoria = " + cod_categoria + ", Nombre = " + nombre +"";
        }
    }
}
