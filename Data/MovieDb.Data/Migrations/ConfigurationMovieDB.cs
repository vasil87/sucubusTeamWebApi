namespace MovieDb.Data.Migrations
{
    using Models;
    using System.Data.Entity.Migrations;
    public sealed class ConfigurationMovieDB : DbMigrationsConfiguration<MovieDb.Data.MoviesContext>
    {
        public ConfigurationMovieDB()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MovieDb.Data.MoviesContext context)
        {
            //context.Users.AddOrUpdate(new Users { FirstName = "Vasil", LastName = "Kamburov", UserName = "Vasil", Password = "12345" });
        }
    }
}
