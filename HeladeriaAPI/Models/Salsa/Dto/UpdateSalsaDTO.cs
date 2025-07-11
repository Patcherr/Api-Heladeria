using System.ComponentModel.DataAnnotations;

namespace HeladeriaAPI.Models.Salsa.Dto
{
    public class UpdateSalsaDTO
    {
        [StringLength(100, ErrorMessage = "El nombre del helado no puede exceder los 100 caracteres.")]
        public string? Nombre { get; set; }

        [MinLength(1, ErrorMessage = "Debe haber al menos un ingrediente.")]
        public List<int>? IngredientesIds { get; set; }

        public bool? IsSinTac { get; set; }
    }
}