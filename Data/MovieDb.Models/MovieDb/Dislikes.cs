using System.ComponentModel.DataAnnotations;

namespace MovieDb.Models
{
    public class Dislikes
    {
        public int Id { get; set; }
        [Required]
        public virtual int UsersId { get; set; }
        [Required]
        public virtual int MoviesId { get; set; }
    }
}
