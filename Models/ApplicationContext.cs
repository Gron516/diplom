﻿namespace RecipeService.Models
{
    using Microsoft.EntityFrameworkCore;

    public class ApplicationContext : DbContext
    {
        public DbSet<Drug> Drugs { get; set; } = null!;
        public DbSet<DrugCache> СachedDrugs { get; set; } = null!;

        public ApplicationContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=RecipesServiceDb;Username=postgres;Password=123456");
        }
    }
}