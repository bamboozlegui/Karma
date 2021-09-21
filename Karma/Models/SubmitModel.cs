
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Karma.Models
{
    public class SubmitModel
    {
        public string BuyerName { get; set; }

        public string City { get; set; }

        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("item_title")]

        public string Title { get; set; }

        public string Description { get; set; }

        public string Picture { get; set;}
        
        public override string ToString() => JsonSerializer.Serialize<SubmitModel>(this);
    }
}