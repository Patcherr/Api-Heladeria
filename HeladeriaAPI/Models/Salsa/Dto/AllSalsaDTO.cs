namespace HeladeriaAPI.Models.Salsa.Dto
{
    public class AllSalsaDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public bool? IsSinTac { get; set; }

        public List<string> Ingredientes { get; set; } = new();
    }
}
