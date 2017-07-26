namespace FirstApplication_V2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FirstApplication_V2.Models.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FirstApplication_V2.Models.DataContext context)
        {

            context.Genres.Add(new Models.Genre { Name = "Comedy" });
            context.Genres.Add(new Models.Genre { Name = "Action" });
            context.Genres.Add(new Models.Genre { Name = "Horror" });
            context.Genres.Add(new Models.Genre { Name = "Adventure" });
            context.Genres.Add(new Models.Genre { Name = "Simulation" });
            context.Genres.Add(new Models.Genre { Name = "Puzzle" });
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
