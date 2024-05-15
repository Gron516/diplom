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
                    var cachedDrugs = db.СachedDrugs.Where(cachedDrug => cachedDrug.Name.ToLower().Contains(name.ToLower())).ToArray();
                    if (cachedDrugs.Length == 0)
                    {
                        var recipe = await GetRecipeWithApi(name.ToLower());
                        return recipe;
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (var cacheDrug in cachedDrugs)
                        {
                            sb.AppendLine($"Название препарата: {cacheDrug.Name}<br>");
                            sb.AppendLine($"Форма: {cacheDrug.Recipe}<br>");
                            sb.AppendLine($"Дозировка: {cacheDrug.Dosage}<br>");
                            sb.AppendLine("****************************************************<br>");
                        }

                        return sb.ToString();
                    }

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
            using (ApplicationContext db = new ApplicationContext())
            {
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
                            DrugCache cachedDrug = new DrugCache { Name = recipe.RusName, Recipe = recipe.ZipInfo, Dosage = recipe.Document.Dosage };
                            db.СachedDrugs.AddRange(cachedDrug);
                            sb.AppendLine($"Название препарата: {recipe.RusName}<br>");
                            sb.AppendLine($"Форма: {recipe.ZipInfo}<br>");
                            sb.AppendLine($"Дозировка: {recipe.Document.Dosage}<br>");
                            sb.AppendLine("****************************************************<br>");
                        }
                    }
                }
                while (pageCount > currentPage);
                db.SaveChanges();
                return sb.ToString();
            }

        } 
    }

}
