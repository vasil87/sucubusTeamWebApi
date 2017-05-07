using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieDb.Models
{
    public class Users
    {
        private ICollection<Comments> comments;
        private ICollection<Likes> likes;
        private ICollection<Dislikes> dislikes;

        public Users()
        {
            this.comments = new HashSet<Comments>();
            this.dislikes = new HashSet<Dislikes>();
            this.likes = new HashSet<Likes>();
            this.Expire = false;
        }
        public int UsersId { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        public bool isMale { get; set; }

        public bool Expire { get; set; }

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
