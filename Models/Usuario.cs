namespace Proyecto_Venta_Productos_Lacteos.Models
{
    public class Usuario
    {
        public int cod_usuario { get; set; }
        public string nombre { get; set; }
        public string usuario { get; set; }
        public string contrasena { get; set; }
        public Rol rol { get; set; }

        public override string ToString()
        {
            return "Codigo: " + cod_usuario + ", Nombre: " + nombre + ", Usuario: " + usuario + ", Contraseña: " + contrasena + "";
        }
    }
}
