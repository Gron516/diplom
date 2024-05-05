using RecipeService.Models;

namespace RecipeService.Services
{
    public static class CheckService
    {
        public static string GetRecipe(string name)
        {

            // получение данных
            using (ApplicationContext db = new ApplicationContext())
            {
                // получаем объекты из бд и выводим на консоль
                var drug = db.Drugs.Where(drug => drug.Name.ToLower() == name).SingleOrDefault();
                if (drug == null)
                {
                    return "такого препарата нет в базе данных";
                }
                if (drug.Recipe == null)
                {
                    return "рецепт для данного препарата отсутствует";
                }
                else
                    return drug.Recipe;
            }


        }
    }
}
