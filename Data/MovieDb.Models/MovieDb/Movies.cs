using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieDb.Models
{
    public class Movies
    {
        private ICollection<Comments> comments;
        private ICollection<Likes> likes;
        private ICollection<Dislikes> dislikes;

        public Movies()
        {
            this.LikesNumber = 0;
            this.DislikesNumber = 0;
            this.comments = new HashSet<Comments>();
            this.dislikes = new HashSet<Dislikes>();
            this.likes = new HashSet<Likes>();

        }
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string ImdbID { get; set; }

        public int LikesNumber { get; set; }

        public int DislikesNumber { get; set; }
        public virtual ICollection<Comments> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<Likes> Likes
        {
            get { return this.likes; }
            set { this.likes = value; }
        }

        public virtual ICollection<Dislikes> Dislikes
        {
            get { return this.dislikes; }
            set { this.dislikes = value; }
        }
    }
}
