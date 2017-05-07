using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.Movies
{
    public class MoviesCreateModel
    {
        public string Name { get; set; }

        public string ImdbID { get; set; }
    }
}