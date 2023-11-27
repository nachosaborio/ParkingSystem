using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto1.Models
{
    public class Parqueo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del parqueo es requerido")]
        [Display(Name = "Nombre del parqueo")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "La capacidad máxima es requerida")]
        [Display(Name = "Capacidad máxima")]
        public int CapacidadMaxima { get; set; }

        [Required(ErrorMessage = "La hora de apertura es requerida")]
        [Display(Name = "Hora de apertura")]
        [DataType(DataType.Time)]
        public DateTime HoraApertura { get; set; }

        [Required(ErrorMessage = "La hora de cierre es requerida")]
        [Display(Name = "Hora de cierre")]
        [DataType(DataType.Time)]
        public DateTime HoraCierrre { get; set; }
    }
}