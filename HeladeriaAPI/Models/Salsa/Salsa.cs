using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeladeriaAPI.Models.Salsa
{
    public class Salsa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;

        public List<Ingrediente.Ingrediente> Ingredientes { get; set; }

        public bool? IsSinTac {  get; set; }

    }
}
