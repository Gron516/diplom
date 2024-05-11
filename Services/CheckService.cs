using RecipeService.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace RecipeService.Services
{
public static class CheckService
    {
        public static async Task<string> GetRecipe(string name)
        {

            // получение данных
            using (ApplicationContext db = new ApplicationContext())
            {
                // получаем объекты из бд и выводим на консоль
                var drug = db.Drugs.Where(drug => drug.Name.ToLower() == name.ToLower()).SingleOrDefault();
                if (drug == null)
                {
                    var recipe = await GetRecipeWithApi(name.ToLower());
                    return recipe;
                }
                if (drug.Recipe == null)
                {
                    return "рецепт для данного препарата отсутствует";
                }
                else
                    return drug.Recipe;
            }


        }
        
        public static async Task<string> GetRecipeWithApi(string name) 
        {
            var drugRecipes = new List<DrugRecipe>();
            StringBuilder sb = new StringBuilder();

            int currentPage = 0;
            int pageCount = 1;
            do
            {
                currentPage++;
                HttpClient httpClient = new HttpClient();
                // устанавливаем заголовк
                httpClient.DefaultRequestHeaders.Add("X-Token", "iQpsJ5IHWdPv");
                var serverAddress = $"https://www.vidal.ru/api/rest/v1/product/list?filter[name]={name}&page={currentPage}";
                Console.WriteLine(serverAddress);
                // выполняем запрос
                using (var responseHttp = await httpClient.GetAsync(serverAddress))
                {
                    var content = await responseHttp.Content.ReadFromJsonAsync<Response>();
                    //распарсит json
                    //var response = JsonSerializer.Deserialize<Response> (content); не актуально?
                    var drugRecipeArray = content.Products;
                    pageCount = content.Pagination.PageCount;
                    foreach (var recipe in drugRecipeArray)
                    {
                        sb.AppendLine($"RusName:{recipe.RusName};");
                        sb.AppendLine($"ZipInfo:{recipe.ZipInfo};");
                        sb.AppendLine($"Dosage:{recipe.Document.Dosage};");
                        sb.AppendLine("*********************************");
                    }
                }
            }
            while (pageCount > currentPage);

            return sb.ToString();
        } 
    }

}
