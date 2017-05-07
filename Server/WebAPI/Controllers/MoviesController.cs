using MovieDb.Data;
using MovieDb.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI.Models.Movies;

namespace WebAPI.Controllers
{
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MoviesController : ApiController
    {
        private readonly IRepository<Movies> movies;
        private readonly IRepository<Likes> likes;
        private readonly IRepository<Dislikes> dislikes;

        public MoviesController(IRepository<Movies> movies,IRepository<Likes> likes,IRepository<Dislikes> dislikes)
        {
            this.movies = movies;
            this.likes = likes;
            this.dislikes = dislikes;
        }
        public IHttpActionResult GetAll()
        {
            var res = this.movies.All().OrderBy(x => x.Name).ToList();
            return this.Ok(res);
        }

        public IHttpActionResult GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return this.BadRequest("Id of movie can`t be null or empty");
            }

            var res = this.movies.All().Where(x => x.ImdbID == id).FirstOrDefault();

            return this.Ok(res);
        }

        public IHttpActionResult Add(MoviesCreateModel movie)
        {
            var currentMovie = this.movies.All().Where(x => x.ImdbID == movie.ImdbID).FirstOrDefault();
            if (currentMovie == null)
            {
                try
                {
                    var movieToAdd = new Movies { Name = movie.Name, ImdbID = movie.ImdbID };
                    this.movies.Add(movieToAdd);
                    this.movies.SaveChanges();
                }
                catch
                {
                    return this.BadRequest("Invalid movie to add");
                }

                return this.Ok(this.movies.All().Where(x => x.Name == movie.Name).FirstOrDefault().Id);
            }
            else
            {
                return this.BadRequest("This Movie Already Exists");
            }

        }

        public IHttpActionResult LikeAMovie(LikeAMovieModel model)
        {
            string movieImdbid = model.ImdbID;
            int userId = model.Userid;
            var currentMovie = this.movies.All().Where(x => x.ImdbID == movieImdbid).FirstOrDefault();
            if (currentMovie == null)
            {
                return this.BadRequest("No such movie");
            }
            currentMovie.LikesNumber = currentMovie.LikesNumber + 1;

            try
            {
                var isSucces=LikeOrDislikeAMovie(true, userId, currentMovie);
                if (isSucces)
                {
                    this.movies.Update(currentMovie);
                    this.movies.SaveChanges();
                }
                else
                {
                    return this.BadRequest("already liked or disliked");
                }
               

            }
            catch
            {
                return this.BadRequest("Invalid data to like a movie");
            }
            return this.Ok(currentMovie.LikesNumber);
        }

        public IHttpActionResult DislikeAMovie(LikeAMovieModel model)
        {
            string movieImdbid = model.ImdbID;
            int userId = model.Userid;
            var currentMovie = this.movies.All().Where(x => x.ImdbID == movieImdbid).FirstOrDefault();
            if (currentMovie == null)
            {
                return this.BadRequest("No such movie");
            }
            currentMovie.DislikesNumber = currentMovie.DislikesNumber + 1;
            try
            {
                var isSucces = LikeOrDislikeAMovie(false, userId, currentMovie);
                if (isSucces)
                {
                    this.movies.Update(currentMovie);
                    this.movies.SaveChanges();
                }
                else
                {
                    return this.BadRequest("already liked or disliked");
                }
            }
            catch
            {
                return this.BadRequest("Invalid data to dislike a movie");
            }
            return this.Ok(currentMovie.DislikesNumber);
        }
        
        [HttpGet]
        public IHttpActionResult GetTopLikedMovies(int id)
        {
            int number = id;
            var res = this.movies.All().OrderByDescending(x => x.LikesNumber).Take(number).ToList();
            return this.Ok(res.Select(x => x.ImdbID));
        }
        [HttpGet]
        public IHttpActionResult GetTopDisLikedMovies(int id)
        {
            int number = id;
            var res = this.movies.All().OrderByDescending(x => x.DislikesNumber).Take(number).ToList();
            return this.Ok(res.Select(x => x.ImdbID));
        }


        private bool isAlreadyLikedOrDislikedAMovie(int userId, Movies movie)
        {
            dynamic islikedOrDisliked = this.likes.All().Where(x => x.MoviesId == movie.Id && x.UsersId == userId).FirstOrDefault();

            if (islikedOrDisliked == null)
            {
                islikedOrDisliked = this.dislikes.All().Where(x => x.MoviesId == movie.Id && x.UsersId == userId).FirstOrDefault();
            }

            if (islikedOrDisliked != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool LikeOrDislikeAMovie(bool isLike, int userId, Movies movie)
        {
            if (isLike)
            {
                if (!isAlreadyLikedOrDislikedAMovie(userId, movie))
                {
                    this.likes.Add(new Likes { UsersId = userId, MoviesId = movie.Id });
                    return true;
                }
            }
            else
            {
                if (!isAlreadyLikedOrDislikedAMovie(userId, movie))
                {
                    this.dislikes.Add(new Dislikes { UsersId = userId, MoviesId = movie.Id });
                    return true;
                }
            }

            return false;

        }

    }
}
