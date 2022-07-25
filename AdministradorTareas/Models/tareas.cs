using System.ComponentModel.DataAnnotations;
namespace AdministradorTareas.Models
{
    public class tareas
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "La Categoria es obligatoria.")]
        [Display(Name = "Categoria")]
        public int categoria { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Tarea")]
        public string tarea { get; set; }
        [Required(ErrorMessage = "El usuario asignado es obligatorio")]
        [Display(Name = "Asignacion")]
        public string asignacion { get; set; }
        [Display(Name = "Creador")]
        public string creador { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Creacion")]
        public DateTime fechaCreacion { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Limite")]
        public DateTime fechaLimite { get; set; }
        [Display(Name = "Estado")]
        public int estado { get; set; } = 1;
        // 1 Iniciada
        // 2 Terminada
        // 3 Cancelada
    }
}
