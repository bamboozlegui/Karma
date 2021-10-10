
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace Karma.Models
{
    public abstract class Post
    {

        public enum StateEnum
        {
            Recent = 1,
            Available = 2,
            Taken = 4,
            Hidden = 8,
            None = 0
        }


        public StateEnum State { get; set; }

        public string ID { get; set; }
        public string PosterName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public string Category {get; set;}
        [Required]
        public static string[] Categories = { "Cat1", "Cat2", "Cat3", "Cat4", "Cat5" };
        public abstract override string ToString();

    }
}
