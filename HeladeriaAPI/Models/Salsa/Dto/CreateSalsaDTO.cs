using System.ComponentModel.DataAnnotations;

namespace HeladeriaAPI.Models.Salsa.Dto
{
    public class CreateSalsaDTO
    {    
        [Required(ErrorMessage ="El nombre de la salsa es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [MinLength(1, ErrorMessage = "Debe seleccionar al menos un ingrediente.")]
        public List<int>? IngredientesIds { get; set; }

        public bool? IsSinTac { get; set; }
    }
}
