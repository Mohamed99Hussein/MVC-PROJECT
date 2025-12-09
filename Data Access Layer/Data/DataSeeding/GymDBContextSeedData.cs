using Data_Access_Layer.Data.Contexts;
using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data_Access_Layer.Data.DataSeeding
{
    public class GymDBContextSeedData
    {
        public static bool SeedData(GymSystemDBContext DbContext)
        {

           try
            {
                var HasPlans = DbContext.Plans.Any();
                var HasCategories = DbContext.Categories.Any();
                if (HasPlans && HasCategories) return false;
                if (!HasCategories)
                {
                    var Categories = LoadData<Category>("categories.json");
                    if (Categories.Any())
                        DbContext.Categories.AddRange(Categories);

                }

                if (!HasPlans)
                {
                    var Plans = LoadData<Plan>("plans.json");
                    if (Plans.Any())
                        DbContext.Plans.AddRange(Plans);

                }

                return DbContext.SaveChanges() > 0;
            }

            catch(Exception ex) 
            {
                Console.WriteLine($"Data Seeding is failed : {ex}");
                return false;
            }

        }


        private static List<T> LoadData<T>(string FileName)
        {
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files", FileName);
            if (!File.Exists(FilePath))
                 throw new FileNotFoundException();

            var Data = File.ReadAllText(FilePath);

            var Options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<List<T>>(Data, Options)??new List<T>();  

        }





    }
}
