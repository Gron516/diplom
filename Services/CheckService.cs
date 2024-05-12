using RecipeService.Models;
using System.Text;

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
                // выполняем запрос
                using (var responseHttp = await httpClient.GetAsync(serverAddress))
                {
                    var debugInfo = await responseHttp.Content.ReadAsStringAsync();
                    Console.WriteLine(debugInfo);
                    var content = await responseHttp.Content.ReadFromJsonAsync<Response>();
                    //распарсит json
                    var drugRecipeArray = content.Products;
                    pageCount = content.Pagination.PageCount;
                    foreach (var recipe in drugRecipeArray)
                    {
                        sb.AppendLine($"Название препарата: {recipe.RusName}<br>");
                        sb.AppendLine($"Форма: {recipe.ZipInfo}<br>");
                        sb.AppendLine($"Дозировка: {recipe.Document.Dosage}<br>");
                        sb.AppendLine("****************************************************<br>");
                    }
                }
            }
            while (pageCount > currentPage);

            return sb.ToString();
        } 
    }

}
