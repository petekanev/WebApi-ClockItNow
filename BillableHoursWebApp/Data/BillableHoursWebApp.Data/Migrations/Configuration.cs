namespace BillableHoursWebApp.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<BillableHoursWebAppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BillableHoursWebAppDbContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.Add(new Category { Name = "ASP.NET, NodeJS, PHP" });
                context.Categories.Add(new Category { Name = ".NET" });
                context.Categories.Add(new Category { Name = "Server and Cloud" });
                context.Categories.Add(new Category { Name = "Typewriting" });
                context.Categories.Add(new Category { Name = "Data Entry" });
                context.Categories.Add(new Category { Name = "Translation and Languages" });
                context.Categories.Add(new Category { Name = "Graphic Design" });
                context.Categories.Add(new Category { Name = "Video and Video Editting" });
                context.Categories.Add(new Category { Name = "Education" });
                context.Categories.Add(new Category { Name = "Other" });

                context.SaveChanges();
            }
        }
    }
}
