using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeladeriaAPI.Models.Ingrediente
{
    public class Ingrediente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }

    public class IngredienteHelado
    {
        public int IngredienteId { get; set; }
        public int HeladoId { get; set; }
    }

    public class IngredienteSalsa
    {
        public int IngredienteId { get; set; }
        public int SalsaId { get; set; }
    }
}
