using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Proyecto1.Models
{
    public class Tiquete
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El parqueo es requerido")]
        [Display(Name = "Parqueo")]
        public int Parqueo { get; set; }

        [Required(ErrorMessage = "La fecha y hora de entrada es requerida")]
        [Display(Name = "Fecha y hora de entrada")]
        public DateTime FechaYHoraEntrada { get; set; }

        [Required(ErrorMessage = "La fecha y hora de salida es requerida")]
        [Display(Name = "Fecha y hora de salida")]
        public DateTime FechaYHoraSalida { get; set; }

        [Required(ErrorMessage = "La placa es requerida")]
        [Display(Name = "Placa")]
        public string? Placa { get; set;}

        [Required(ErrorMessage = "La tarifa por hora es requerida")]
        [Display(Name = "Tarifa por hora")]
        public double TarifaPorHora { get; set; }

        [Required(ErrorMessage = "La tarifa por media hora es requerida")]
        [Display(Name = "Tarifa por media hora")]
        public double TarifaPorMediaHora { get; set; }

	}
}
