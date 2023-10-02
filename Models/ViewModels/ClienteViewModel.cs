using Proyecto_Venta_Productos_Lacteos.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Venta_Productos_Lacteos.Models.ViewModels
{
    public class ClienteViewModel
    {
        [Required]
        [CodValidation]
        public int cod_clientes { get; set; }

        [Required]
        [MaxLength(45)]
        public string nombre { get; set; }

        [Required]
        [MaxLength(45)]
        public string apellido { get; set; }

        [Required]
        [MaxLength(10)]
        public string cedula { get; set; }

        [EmailAddress]
        [Required]
        [MaxLength(30)]
        public string email { get; set; }

        [Required]
        [MaxLength(10)]
        public string telefono { get; set; }

        [Required]
        [MaxLength(100)]
        public string direccion { get; set; }

        public override string ToString()
        {
            return "Codigo = " + cod_clientes + ", Nombre = " + nombre + ", Apellido = " + apellido + ", Cedula = " + cedula +
                ", Email = " + email + ", Telefono = " + telefono + ", Dirección =" + direccion + "";
        }
    }
}
