using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApplication3.Services
{
    public static class PodborService
    {
        public static string Vibor(string nazvan)
        {

            switch(nazvan)
                {
                case "парацетамол":
                    return "выпейте две таблетки и ложитесь спать";
                case "пурген":
                    return "примите внутрь и идите в туалет";
                case "бакта":
                    return "залейте в ванну, забирайтесь внутрь и медитируйте";
                default:
                    return "такого препарата нет в базе данных";
                }


            }
    }
}
