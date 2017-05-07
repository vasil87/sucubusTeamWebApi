namespace MovieDb.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Comments
    {
        public Comments()
        {
            this.CreatedOn = DateTime.Now;
            this.isDeleted = false;
        }
        public int Id
        {
            get; set;
        }

        [MaxLength(500)]
        public string Comment { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool isDeleted { get; set; }

        [Required]
        public virtual int UsersId { get; set; }

        [Required]
        public virtual int MoviesId { get; set; }

    }
}
