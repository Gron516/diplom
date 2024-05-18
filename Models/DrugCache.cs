namespace RecipeService.Models
{
    public class DrugCache
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Recipe { get; set; }
        public string? Dosage { get; set; }
    }
}
