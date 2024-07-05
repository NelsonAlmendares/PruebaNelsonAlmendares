using System.ComponentModel.DataAnnotations;
namespace Administracion.Models
{
    public class EmpleadoModelo
    {
        // Creamos los metodos para inicializar las variables
        public int ID_empleado { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? DateBirth { get; set; }
        [Required]
        public string? Position { get; set; }
        [Required]
        public double ? Amount { get; set; }
    }
}
