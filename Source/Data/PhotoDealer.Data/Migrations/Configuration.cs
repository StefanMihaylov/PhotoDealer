namespace PhotoDealer.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Linq;

    using PhotoDealer.Data.Models;
    using PhotoDealer.Common;

    internal sealed class Configuration : DbMigrationsConfiguration<PhotoDealer.Data.AppDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(AppDbContext context)
        {
            if (!context.CategoryGroups.Any())
            {
                SeedCategories(context);
            }

            if (!context.Roles.Any())
            {
                var roles = new List<IdentityRole>()
                {
                   // new IdentityRole(GlobalConstants.RegularUserRoleName),
                    new IdentityRole(GlobalConstants.TrustedUserRoleName),
                    new IdentityRole(GlobalConstants.ModeratorRoleName),
                    new IdentityRole(GlobalConstants.AdministratorRoleName),
                };

                context.Roles.AddOrUpdate(roles.ToArray());
                context.SaveChanges();
            }
        }

        private void SeedCategories(AppDbContext context)
        {
            var categoryGroups = new List<CategoryGroup>();
            var categories = new List<Category>();

            categoryGroups.Add(new CategoryGroup() { GroupName = "Buildings" });
            categories.Add(new Category()
            {
                Name = "Agricultural",
                CategoryGroup = categoryGroups[0]
            });
            categories.Add(new Category()
            {
                Name = "Architectural",
                CategoryGroup = categoryGroups[0]
            });
            categories.Add(new Category()
            {
                Name = "Bridges",
                CategoryGroup = categoryGroups[0]
            });
            categories.Add(new Category()
            {
                Name = "Industrial",
                CategoryGroup = categoryGroups[0]
            });
            categories.Add(new Category()
            {
                Name = "Interiors",
                CategoryGroup = categoryGroups[0]
            });
            categories.Add(new Category()
            {
                Name = "Monuments",
                CategoryGroup = categoryGroups[0]
            });
            categories.Add(new Category()
            {
                Name = "Religious",
                CategoryGroup = categoryGroups[0]
            });
            categories.Add(new Category()
            {
                Name = "Residences",
                CategoryGroup = categoryGroups[0]
            });

            categoryGroups.Add(new CategoryGroup() { GroupName = "Man-made spaces" });
            categories.Add(new Category()
            {
                Name = "Cemeteries",
                CategoryGroup = categoryGroups[1]
            });
            categories.Add(new Category()
            {
                Name = "Cities From Above",
                CategoryGroup = categoryGroups[1]
            });
            categories.Add(new Category()
            {
                Name = "Gardens",
                CategoryGroup = categoryGroups[1]
            });
            categories.Add(new Category()
            {
                Name = "Amusement Parks",
                CategoryGroup = categoryGroups[1]
            });
            categories.Add(new Category()
            {
                Name = "Public benches",
                CategoryGroup = categoryGroups[1]
            });
            categories.Add(new Category()
            {
                Name = "Trails",
                CategoryGroup = categoryGroups[1]
            });
            categories.Add(new Category()
            {
                Name = "Walkways",
                CategoryGroup = categoryGroups[1]
            });
            categories.Add(new Category()
            {
                Name = "Skylines",
                CategoryGroup = categoryGroups[1]
            });
            categories.Add(new Category()
            {
                Name = "Tunnels",
                CategoryGroup = categoryGroups[1]
            });

            categoryGroups.Add(new CategoryGroup() { GroupName = "Roads" });
            categories.Add(new Category()
            {
                Name = "Border Crossings",
                CategoryGroup = categoryGroups[2]
            });
            categories.Add(new Category()
            {
                Name = "Road curves",
                CategoryGroup = categoryGroups[2]
            });
            categories.Add(new Category()
            {
                Name = "Highways",
                CategoryGroup = categoryGroups[2]
            });

            categoryGroups.Add(new CategoryGroup() { GroupName = "Transportation" });
            categories.Add(new Category()
            {
                Name = "Small Planes",
                CategoryGroup = categoryGroups[3]
            });
            categories.Add(new Category()
            {
                Name = "ATVs",
                CategoryGroup = categoryGroups[3]
            });
            categories.Add(new Category()
            {
                Name = "Autos",
                CategoryGroup = categoryGroups[3]
            });
            categories.Add(new Category()
            {
                Name = "Bicycles",
                CategoryGroup = categoryGroups[3]
            });
            categories.Add(new Category()
            {
                Name = "Boats",
                CategoryGroup = categoryGroups[3]
            });
            categories.Add(new Category()
            {
                Name = "Busses",
                CategoryGroup = categoryGroups[3]
            });
            categories.Add(new Category()
            {
                Name = "Trains",
                CategoryGroup = categoryGroups[3]
            });
            categories.Add(new Category()
            {
                Name = "Carriages",
                CategoryGroup = categoryGroups[3]
            });
            categories.Add(new Category()
            {
                Name = "Classic Cars",
                CategoryGroup = categoryGroups[3]
            });
            categories.Add(new Category()
            {
                Name = "Motorbikes",
                CategoryGroup = categoryGroups[3]
            });
            categories.Add(new Category()
            {
                Name = "Trucks",
                CategoryGroup = categoryGroups[3]
            });

            categoryGroups.Add(new CategoryGroup() { GroupName = "Animals and Wildlife" });
            categories.Add(new Category()
            {
                Name = "Animal Tracks",
                CategoryGroup = categoryGroups[4]
            });
            categories.Add(new Category()
            {
                Name = "Birds",
                CategoryGroup = categoryGroups[4]
            });
            categories.Add(new Category()
            {
                Name = "Fish",
                CategoryGroup = categoryGroups[4]
            });
            categories.Add(new Category()
            {
                Name = "Insects",
                CategoryGroup = categoryGroups[4]
            });
            categories.Add(new Category()
            {
                Name = "Mammals",
                CategoryGroup = categoryGroups[4]
            });
            categories.Add(new Category()
            {
                Name = "Marine Life",
                CategoryGroup = categoryGroups[4]
            });
            categories.Add(new Category()
            {
                Name = "Pets",
                CategoryGroup = categoryGroups[4]
            });
            categories.Add(new Category()
            {
                Name = "Reptiles",
                CategoryGroup = categoryGroups[4]
            });

            categoryGroups.Add(new CategoryGroup() { GroupName = "Atmosphere and Sky" });
            categories.Add(new Category()
            {
                Name = "Alpenglow",
                CategoryGroup = categoryGroups[5]
            });
            categories.Add(new Category()
            {
                Name = "Aurora Borealis",
                CategoryGroup = categoryGroups[5]
            });
            categories.Add(new Category()
            {
                Name = "Cloudless Sky",
                CategoryGroup = categoryGroups[5]
            });
            categories.Add(new Category()
            {
                Name = "Clouds",
                CategoryGroup = categoryGroups[5]
            });
            categories.Add(new Category()
            {
                Name = "Fog",
                CategoryGroup = categoryGroups[5]
            });
            categories.Add(new Category()
            {
                Name = "Moon",
                CategoryGroup = categoryGroups[5]
            });
            categories.Add(new Category()
            {
                Name = "Rain",
                CategoryGroup = categoryGroups[5]
            });
            categories.Add(new Category()
            {
                Name = "Sea of Clouds",
                CategoryGroup = categoryGroups[5]
            });
            categories.Add(new Category()
            {
                Name = "Stars",
                CategoryGroup = categoryGroups[5]
            });
            categories.Add(new Category()
            {
                Name = "Sun",
                CategoryGroup = categoryGroups[5]
            });
            categories.Add(new Category()
            {
                Name = "Sunrises",
                CategoryGroup = categoryGroups[5]
            });
            categories.Add(new Category()
            {
                Name = "Sunsets",
                CategoryGroup = categoryGroups[5]
            });
            categories.Add(new Category()
            {
                Name = "Twilight",
                CategoryGroup = categoryGroups[5]
            });

            categoryGroups.Add(new CategoryGroup() { GroupName = "Nature Environments" });
            categories.Add(new Category()
            {
                Name = "Deserts",
                CategoryGroup = categoryGroups[6]
            });
            categories.Add(new Category()
            {
                Name = "Grasslands",
                CategoryGroup = categoryGroups[6]
            });
            categories.Add(new Category()
            {
                Name = "High Altitude",
                CategoryGroup = categoryGroups[6]
            });
            categories.Add(new Category()
            {
                Name = "Middle Mountain",
                CategoryGroup = categoryGroups[6]
            });
            categories.Add(new Category()
            {
                Name = "North Woods",
                CategoryGroup = categoryGroups[6]
            });
            categories.Add(new Category()
            {
                Name = "Oasis",
                CategoryGroup = categoryGroups[6]
            });
            categories.Add(new Category()
            {
                Name = "Subtropics",
                CategoryGroup = categoryGroups[6]
            });
            categories.Add(new Category()
            {
                Name = "Timberline",
                CategoryGroup = categoryGroups[6]
            });
            categories.Add(new Category()
            {
                Name = "Tropics",
                CategoryGroup = categoryGroups[6]
            });
            categories.Add(new Category()
            {
                Name = "Tundra",
                CategoryGroup = categoryGroups[6]
            });
            categories.Add(new Category()
            {
                Name = "Wetlands",
                CategoryGroup = categoryGroups[6]
            });

            categoryGroups.Add(new CategoryGroup() { GroupName = "Flowers" });
            categories.Add(new Category()
            {
                Name = "Alpine Flowers",
                CategoryGroup = categoryGroups[7]
            });
            categories.Add(new Category()
            {
                Name = "Beargrass",
                CategoryGroup = categoryGroups[7]
            });
            categories.Add(new Category()
            {
                Name = "Brittlebush",
                CategoryGroup = categoryGroups[7]
            });
            categories.Add(new Category()
            {
                Name = "Cactus Blooms",
                CategoryGroup = categoryGroups[7]
            });
            categories.Add(new Category()
            {
                Name = "Coreopsis Flowers",
                CategoryGroup = categoryGroups[7]
            });
            categories.Add(new Category()
            {
                Name = "Daffodils",
                CategoryGroup = categoryGroups[7]
            });
            categories.Add(new Category()
            {
                Name = "Daisies",
                CategoryGroup = categoryGroups[7]
            });
            categories.Add(new Category()
            {
                Name = "Dalhias",
                CategoryGroup = categoryGroups[7]
            });
            categories.Add(new Category()
            {
                Name = "Decorative Flowers",
                CategoryGroup = categoryGroups[7]
            });
            categories.Add(new Category()
            {
                Name = "Flower Carpets",
                CategoryGroup = categoryGroups[7]
            });
            categories.Add(new Category()
            {
                Name = "Iceplant",
                CategoryGroup = categoryGroups[7]
            });
            categories.Add(new Category()
            {
                Name = "Irises",
                CategoryGroup = categoryGroups[7]
            });
            categories.Add(new Category()
            {
                Name = "Lupine",
                CategoryGroup = categoryGroups[7]
            });
            categories.Add(new Category()
            {
                Name = "Morning Glories",
                CategoryGroup = categoryGroups[7]
            });
            categories.Add(new Category()
            {
                Name = "Orchids",
                CategoryGroup = categoryGroups[7]
            });
            categories.Add(new Category()
            {
                Name = "Rododendrons",
                CategoryGroup = categoryGroups[7]
            });
            categories.Add(new Category()
            {
                Name = "Roses",
                CategoryGroup = categoryGroups[7]
            });
            categories.Add(new Category()
            {
                Name = "Sunflowers",
                CategoryGroup = categoryGroups[7]
            });
            categories.Add(new Category()
            {
                Name = "Trillium",
                CategoryGroup = categoryGroups[7]
            });
            categories.Add(new Category()
            {
                Name = "Tulips",
                CategoryGroup = categoryGroups[7]
            });

            categoryGroups.Add(new CategoryGroup() { GroupName = "Freshwater" });
            categories.Add(new Category()
            {
                Name = "Braided Rivers",
                CategoryGroup = categoryGroups[8]
            });
            categories.Add(new Category()
            {
                Name = "Cascades",
                CategoryGroup = categoryGroups[8]
            });
            categories.Add(new Category()
            {
                Name = "Flooded Plains",
                CategoryGroup = categoryGroups[8]
            });
            categories.Add(new Category()
            {
                Name = "Islets In Lakes",
                CategoryGroup = categoryGroups[8]
            });
            categories.Add(new Category()
            {
                Name = "Lakes",
                CategoryGroup = categoryGroups[8]
            });
            categories.Add(new Category()
            {
                Name = "Ponds",
                CategoryGroup = categoryGroups[8]
            });
            categories.Add(new Category()
            {
                Name = "Rainbows",
                CategoryGroup = categoryGroups[8]
            });
            categories.Add(new Category()
            {
                Name = "Rivers",
                CategoryGroup = categoryGroups[8]
            });
            categories.Add(new Category()
            {
                Name = "Streams",
                CategoryGroup = categoryGroups[8]
            });
            categories.Add(new Category()
            {
                Name = "Swamps",
                CategoryGroup = categoryGroups[8]
            });
            categories.Add(new Category()
            {
                Name = "Waterfalls",
                CategoryGroup = categoryGroups[8]
            });

            categoryGroups.Add(new CategoryGroup() { GroupName = "Mountains" });
            categories.Add(new Category()
            {
                Name = "Distant Ranges",
                CategoryGroup = categoryGroups[9]
            });
            categories.Add(new Category()
            {
                Name = "Forested Peaks",
                CategoryGroup = categoryGroups[9]
            });
            categories.Add(new Category()
            {
                Name = "Moraines",
                CategoryGroup = categoryGroups[9]
            });
            categories.Add(new Category()
            {
                Name = "Mountain Vistas",
                CategoryGroup = categoryGroups[9]
            });
            categories.Add(new Category()
            {
                Name = "Ridges",
                CategoryGroup = categoryGroups[9]
            });
            categories.Add(new Category()
            {
                Name = "Rocky Peaks",
                CategoryGroup = categoryGroups[9]
            });
            categories.Add(new Category()
            {
                Name = "Snowy Peaks",
                CategoryGroup = categoryGroups[9]
            });
            categories.Add(new Category()
            {
                Name = "Valleys",
                CategoryGroup = categoryGroups[9]
            });

            categoryGroups.Add(new CategoryGroup() { GroupName = "Ice and Snow" });
            categories.Add(new Category()
            {
                Name = "Frost",
                CategoryGroup = categoryGroups[10]
            });
            categories.Add(new Category()
            {
                Name = "Frozen Rivers",
                CategoryGroup = categoryGroups[10]
            });
            categories.Add(new Category()
            {
                Name = "Frozen Waterfalls",
                CategoryGroup = categoryGroups[10]
            });
            categories.Add(new Category()
            {
                Name = "Glaciers",
                CategoryGroup = categoryGroups[10]
            });
            categories.Add(new Category()
            {
                Name = "Hail",
                CategoryGroup = categoryGroups[10]
            });
            categories.Add(new Category()
            {
                Name = "Icebergs",
                CategoryGroup = categoryGroups[10]
            });
            categories.Add(new Category()
            {
                Name = "Permanent Snow",
                CategoryGroup = categoryGroups[10]
            });
            categories.Add(new Category()
            {
                Name = "Seasonal Snow",
                CategoryGroup = categoryGroups[10]
            });

            categoryGroups.Add(new CategoryGroup() { GroupName = "Vegetation" });
            categories.Add(new Category()
            {
                Name = "Aquatic Plants",
                CategoryGroup = categoryGroups[11]
            });
            categories.Add(new Category()
            {
                Name = "Bamboo",
                CategoryGroup = categoryGroups[11]
            });
            categories.Add(new Category()
            {
                Name = "Blossoms",
                CategoryGroup = categoryGroups[11]
            });
            categories.Add(new Category()
            {
                Name = "Carnivorous Plants",
                CategoryGroup = categoryGroups[11]
            });
            categories.Add(new Category()
            {
                Name = "Desert Plants",
                CategoryGroup = categoryGroups[11]
            });
            categories.Add(new Category()
            {
                Name = "Epiphytes",
                CategoryGroup = categoryGroups[11]
            });
            categories.Add(new Category()
            {
                Name = "Fall Colors",
                CategoryGroup = categoryGroups[11]
            });
            categories.Add(new Category()
            {
                Name = "Ferns",
                CategoryGroup = categoryGroups[11]
            });
            categories.Add(new Category()
            {
                Name = "Forests",
                CategoryGroup = categoryGroups[11]
            });
            categories.Add(new Category()
            {
                Name = "Grasses",
                CategoryGroup = categoryGroups[11]
            });
            categories.Add(new Category()
            {
                Name = "Mushrooms",
                CategoryGroup = categoryGroups[11]
            });
            categories.Add(new Category()
            {
                Name = "Shrubs",
                CategoryGroup = categoryGroups[11]
            });
            categories.Add(new Category()
            {
                Name = "Trees",
                CategoryGroup = categoryGroups[11]
            });
            categories.Add(new Category()
            {
                Name = "Vines",
                CategoryGroup = categoryGroups[11]
            });

            categoryGroups.Add(new CategoryGroup() { GroupName = "People" });
            categories.Add(new Category()
            {
                Name = "Artists",
                CategoryGroup = categoryGroups[12]
            });
            categories.Add(new Category()
            {
                Name = "Children",
                CategoryGroup = categoryGroups[12]
            });
            categories.Add(new Category()
            {
                Name = "Couples",
                CategoryGroup = categoryGroups[12]
            });
            categories.Add(new Category()
            {
                Name = "Crowds",
                CategoryGroup = categoryGroups[12]
            });
            categories.Add(new Category()
            {
                Name = "Families",
                CategoryGroup = categoryGroups[12]
            });
            categories.Add(new Category()
            {
                Name = "Monks",
                CategoryGroup = categoryGroups[12]
            });
            categories.Add(new Category()
            {
                Name = "Portraits",
                CategoryGroup = categoryGroups[12]
            });
            categories.Add(new Category()
            {
                Name = "Uniformed People",
                CategoryGroup = categoryGroups[12]
            });
            categories.Add(new Category()
            {
                Name = "Weddings",
                CategoryGroup = categoryGroups[12]
            });
            categories.Add(new Category()
            {
                Name = "Men",
                CategoryGroup = categoryGroups[12]
            });
            categories.Add(new Category()
            {
                Name = "Women",
                CategoryGroup = categoryGroups[12]
            });

            context.CategoryGroups.AddOrUpdate(categoryGroups.ToArray());
            context.Categories.AddOrUpdate(categories.ToArray());

            context.SaveChanges();
        }

    }
}
