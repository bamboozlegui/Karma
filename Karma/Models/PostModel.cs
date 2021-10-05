
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
            Recent,
            Available,
            Taken,
            Pending,
            None
        }

        public StateEnum State { get; set; }

        public string PosterName { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public abstract override string ToString();

    }
}
