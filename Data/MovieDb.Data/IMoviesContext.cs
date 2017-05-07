using System.Data.Entity;
using MovieDb.Models;
using System.Data.Entity.Infrastructure;

namespace MovieDb.Data
{
    public interface IMoviesContext
    {
        DbSet<Comments> Comments { get; set; }
        DbSet<Dislikes> Dislikes { get; set; }
        DbSet<Likes> Likes { get; set; }
        DbSet<Movies> Movies { get; set; }
        DbSet<Users> Users { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        void Dispose();
        int SaveChanges();
    }
}