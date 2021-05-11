using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using WheelsCrawler.Data.Models;
using WheelsCrawler.Data.Models.Account;

namespace WheelsCrawler.Data.Helpers
{
    public class Seed
    {
        public static async Task SeedData(UserManager<User> userManager, RoleManager<AppRole> roleManager, WheelsCrawlerDbContext dbContext)
        {
            // var carData = await System.IO.File.ReadAllTextAsync("../WheelsCrawler.Data/jsonSeedData/Cars.json");
            // var cars = JsonConvert.DeserializeObject<List<Car>>(carData);

            // foreach (var car in cars)
            // {
            //     await dbContext.Cars.AddAsync(car);
            // }

            var urlData = await System.IO.File.ReadAllTextAsync("../WheelsCrawler.Data/jsonSeedData/Urls.json");
            var urls = JsonConvert.DeserializeObject<List<Url>>(urlData);

            foreach (var url in urls)
            {
                await dbContext.Urls.AddAsync(url);
                await dbContext.SaveChangesAsync();
            }

            var brandsData = await System.IO.File.ReadAllTextAsync("../WheelsCrawler.Data/jsonSeedData/CarBrands.json");
            var brands = JsonConvert.DeserializeObject<List<CarBrand>>(brandsData);

            foreach (var brand in brands)
            {
                await dbContext.CarBrands.AddAsync(brand);
                await dbContext.SaveChangesAsync();
            }

            var modelsData = await System.IO.File.ReadAllTextAsync("../WheelsCrawler.Data/jsonSeedData/CarModels.json");
            var models = JsonConvert.DeserializeObject<List<CarModel>>(modelsData);

            foreach (var model in models)
            {
                await dbContext.CarModels.AddAsync(model);
                await dbContext.SaveChangesAsync();
            }

            var users = new List<User>
            {
                new User { UserName = "elon"},
                new User { UserName = "stephan"},
                new User { UserName = "jeff"},
            };

            var roles = new List<AppRole>
            {
                new AppRole { Name = "Member" },
                new AppRole { Name = "Admin" },
                new AppRole { Name = "Moderator" },
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();

                await userManager.CreateAsync(user, "Password3000");
                await userManager.AddToRoleAsync(user, "Member");
            }

            var admin = new User
            {
                UserName = "admin"
            };

            await userManager.CreateAsync(admin, "Password3000");
            await userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });
            
        }

    }
}