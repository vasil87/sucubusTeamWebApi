using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.Movies
{
    public class LikeAMovieModel
    {
        public int Userid { get; set; }

        public string ImdbID { get; set; }
    }
}