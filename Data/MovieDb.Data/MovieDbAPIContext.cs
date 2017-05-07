namespace MovieDb.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using MovieDb.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MovieDbAPIContext : IdentityDbContext<MovieDBApiUsers>
    {
        public MovieDbAPIContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static MovieDbAPIContext Create()
        {
            return new MovieDbAPIContext();
        }
    }
}
