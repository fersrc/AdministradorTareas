using System.ComponentModel.DataAnnotations;

namespace AdministradorTareas.Models
{
    public class estadoTarea
    {
        public int ID { get; set; }
        [Display(Name = "Nombre Categoria")]
        [Required(ErrorMessage = "El nombre del estado es obligatorio.")]
        public string estado { get; set; }
    }
}
