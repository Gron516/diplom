namespace RecipeService.Models
{
    using Microsoft.EntityFrameworkCore;

    public class ApplicationContext : DbContext
    {
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<DrugCache> СachedDrugs { get; set; }

        public ApplicationContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=RecipesServiceDb;Username=postgres;Password=123456");
        }
    }
}