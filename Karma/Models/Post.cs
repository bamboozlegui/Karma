
using System;
using System.ComponentModel.DataAnnotations;
using Karma.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace Karma.Models
{
    [Index(nameof(Post.Id), IsUnique = true)]
    public abstract class Post : IComparable<Post>
    {

        public enum StateEnum
        {
            None = 0,
            Recent = 1,
            Available = 2,
            Taken = 4,
            Hidden = 8
        }

        public StateEnum State { get; set; }

        [Key]
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; } 

        [Required]
        public string Category { get; set; }
        public KarmaUser KarmaUser { get; set; }

        public static string[] Categories = { "Electronics", "Transportation", "Clothes", "Toys", "Other" };

        //IComparable implementation. CompareTo is used by majority of LINQ methods.
        public int CompareTo(Post otherPost)
        {
            if (otherPost != null)
                return this.Date.CompareTo(otherPost.Date);
            else
                throw new NullReferenceException("Object is null");
        }
    }
}
