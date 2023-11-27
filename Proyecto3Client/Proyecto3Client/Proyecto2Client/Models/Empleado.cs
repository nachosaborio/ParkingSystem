using System.ComponentModel.DataAnnotations;

namespace Proyecto1.Models
{
    public class Empleado
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El parqueo es requerido")]
        [Display(Name = "Parqueo")]
        public int Parqueo { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es requerida")]
        [Display(Name = "Fecha de ingreso")]
        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [Display(Name = "Nombre")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Los apellidos son requeridos")]
        [Display(Name = "Apellidos")]
        public string? Apellidos { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date)]
        public DateTime FechaDeNacimiento { get; set; }

        [Required(ErrorMessage = "La cédula es requerida")]
        [Display(Name = "Cédula")]
        public long Cedula { get; set; }

        [Required(ErrorMessage = "La dirección es requerida")]
        [Display(Name = "Dirección")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage ="Por favor, ingrese un email válido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El teléfono es requerido")]
        [Display(Name = "Teléfono")]
        public int Telefono { get; set; }

        [Required(ErrorMessage = "La persona de contacto es requerida")]
        [Display(Name = "Persona de contacto")]
        public string? PersonaDeContacto { get; set; }
    }
}
