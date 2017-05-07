namespace MovieDb.Data
{
    using MovieDb.Models;
    using System.Data.Entity;
    public class MoviesContext : DbContext, IMoviesContext
    {
        public MoviesContext()
            :base("MovieDbConnection")
        {

        }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<Dislikes> Dislikes { get; set; }

    }
}
