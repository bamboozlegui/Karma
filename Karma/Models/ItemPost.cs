using System.Text.Json;

namespace Karma.Models
{
    public class ItemPost : Post, IJsonStorable
    {
        public string Picture { get; set; }
        
        public override string ToString() => JsonSerializer.Serialize<ItemPost>(this);

        public string GetJsonName() => "items.json";
    }
}
