using Proyecto_Venta_Productos_Lacteos.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Venta_Productos_Lacteos.Models.ViewModels
{
    public class UsuarioViewModel
    {
        [Required]
        [CodValidation]
        public int cod_usuario { get; set; }

        [Required]
        [MaxLength(45)]
        public string nombre { get; set; }

        [Required]
        [MaxLength(45)]
        public string usuario { get; set; }

        [Required]
        [MaxLength(45)]
        public string contrasena { get; set; }

        [Required]
        public int cod_rol { get; set; }
    }
}
