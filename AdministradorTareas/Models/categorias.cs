using System.ComponentModel.DataAnnotations;

namespace AdministradorTareas.Models
{
    public class categorias
    {
        public int ID { get; set; }
        [Display(Name = "Nombre Categoria")]
        [Required(ErrorMessage = "El nombre de la categoria es obligatorio.")]
        public string nombre { get; set; }
    }
}
