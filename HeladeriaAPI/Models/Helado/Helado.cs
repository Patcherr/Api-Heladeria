using HeladeriaAPI.Models.Estado;
using HeladeriaAPI.Models.Ingrediente;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HeladeriaAPI.Models.Helado
{
    public class Helado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public double Precio { get; set; }
        public bool IsArtesanal { get; set; }

        [ForeignKey("EstadoId")]
        public Estado.Estado Estado { get; set; } = null!;
        public int EstadoId { get; set; }

        public List<Ingrediente.Ingrediente> Ingredientes { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
