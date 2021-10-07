
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Karma.Models
{
    public class ItemPost : Post, IJsonStorable
    {
        public string Picture { get; set; }
        
        public override string ToString() => JsonSerializer.Serialize<ItemPost>(this);
        public string GetJsonName() => "items.json";

    }
}