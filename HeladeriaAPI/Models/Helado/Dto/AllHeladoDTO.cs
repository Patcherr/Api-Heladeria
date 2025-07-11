using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeladeriaAPI.Models.Helado.Dto
{
    public class AllHeladoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public double Precio { get; set; }
        public List<string> Ingredientes { get; set; } = new();
        public Estado.Estado Estado { get; set; } = null!;
    }
}
