using Task5_EF.Models;

namespace Task5_EF.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Task5_EF.DbContext.NorthwindDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Task5_EF.DbContext.NorthwindDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            context.Categories.AddOrUpdate(
                new Category
                {
                    CategoryID = 1,
                    CategoryName = "Beverages",
                    Description = "Soft drinks, coffees, teas, beers, and ales"
                },
                new Category
                {
                    CategoryID = 2,
                    CategoryName = "Condiments",
                    Description = "Sweet and savory sauces, relishes, spreads, and seasonings"
                },
                new Category
                {
                    CategoryID = 3,
                    CategoryName = "Confections",
                    Description = "Desserts, candies, and sweet breads"
                },
                new Category
                {
                    CategoryID = 4,
                    CategoryName = "Dairy Products",
                    Description = "Cheeses"
                },
                new Category
                {
                    CategoryID = 5,
                    CategoryName = "Grains/Cereals",
                    Description = "Breads, crackers, pasta, and cereal"
                },
                new Category
                {
                    CategoryID = 6,
                    CategoryName = "Meat/Poultry",
                    Description = "Prepared meats"
                },
                new Category
                {
                    CategoryID = 7,
                    CategoryName = "Produce",
                    Description = "Dried fruit and bean curd"
                },
                new Category
                {
                    CategoryID = 8,
                    CategoryName = "Seafood",
                    Description = "Seaweed and fish"
                });

            context.Regions.AddOrUpdate(
                new Region
                {
                    RegionID = 1,
                    RegionDescription = "Eastern"
                },
                new Region
                {
                    RegionID = 2,
                    RegionDescription = "Western"
                },
                new Region
                {
                    RegionID = 3,
                    RegionDescription = "Northern"
                },
                new Region
                {
                    RegionID = 4,
                    RegionDescription = "Southern"
                }
            );

            context.Territories.AddOrUpdate(
                new Territory
                {
                    TerritoryID = "01581",
                    TerritoryDescription = "Westboro",
                    RegionID = 1
                },
                new Territory
                {
                    TerritoryID = "03049",
                    TerritoryDescription = "Hollis",
                    RegionID = 3
                },
                new Territory
                {
                    TerritoryID = "29202",
                    TerritoryDescription = "Columbia",
                    RegionID = 4
                },
                new Territory
                {
                    TerritoryID = "85014",
                    TerritoryDescription = "Phoenix",
                    RegionID = 2
                },
                new Territory
                {
                    TerritoryID = "55113",
                    TerritoryDescription = "Roseville",
                    RegionID = 3
                },
                new Territory
                {
                    TerritoryID = "98052",
                    TerritoryDescription = "Redmond",
                    RegionID = 2
                },
                new Territory
                {
                    TerritoryID = "33607",
                    TerritoryDescription = "Tampa",
                    RegionID = 4
                },
                new Territory
                {
                    TerritoryID = "11747",
                    TerritoryDescription = "Mellvile",
                    RegionID = 1
                }
            );
        }
    }
}