
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Karma.Models
{
    public abstract class Post
    {

        public enum StateEnum
        {
            Available = 1,
            Recent = 2,
            Taken = 4,
            Hidden = 8,
            None = 0
        }


        public StateEnum State { get; set; }

        public string PosterName { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string Category {get; set;}

        public static string[] Categories = { "Cat1", "Cat2", "Cat3", "Cat4", "Cat5" };
        public abstract override string ToString();

    }
}
