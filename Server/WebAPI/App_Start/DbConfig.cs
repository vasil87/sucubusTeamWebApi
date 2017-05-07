namespace WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data.Entity;
    using MovieDb.Data;
    using MovieDb.Data.Migrations;
    public static class DbConfig
    {
        public static void Initiliaze()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MoviesContext, ConfigurationMovieDB>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MovieDbAPIContext, ConfigurationApiUserDB>());
            new MovieDbAPIContext().Database.Initialize(true);
            new MoviesContext().Database.Initialize(true);
        }
    }
}