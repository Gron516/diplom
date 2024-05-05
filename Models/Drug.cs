namespace RecipeService.Models
{
    //[Table("drugs", Schema = "public")]
    public class Drug
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Recipe { get; set; }
    }
}
