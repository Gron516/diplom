namespace RecipeService.Models
{
    public class Response
    {
        public DrugRecipe[] Products { get; set; }
        public Pagination Pagination { get; set; }
    }
} 