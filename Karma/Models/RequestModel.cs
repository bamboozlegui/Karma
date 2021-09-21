using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Karma.Models
{
    public class RequestModel
    {
         public string RequesterName { get; set; }

        public string City { get; set; }

        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("item_title")]

        public string Title { get; set; }

        public string Description { get; set; }

        public override string ToString() => JsonSerializer.Serialize<RequestModel>(this);
    }
}
