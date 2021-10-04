
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Karma.Models
{
    public abstract class PostModel
    {
        public string State { get; set; }

        public enum _StateEnum
        {
            Available,
            Unavailable,
            Pending,
            None
        }

        public _StateEnum StateEnum;


        public string PosterName { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public abstract override string ToString();

    }
}
