using System.ComponentModel.DataAnnotations;

namespace Proyecto_Venta_Productos_Lacteos.Models.Validations
{
    public class CodValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                if (value != null)
                {
                    int cod = (int)value;

                    if (cod < 1)
                    {
                        return new ValidationResult("El Codigo no puede ser menor a 1");
                    }
                }
            }
            catch
            {
                return new ValidationResult("El Codigo tiene que ser un número entero");
            }


            return ValidationResult.Success;
        }
    }
}
