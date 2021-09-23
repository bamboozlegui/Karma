
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Karma.Models
{
    public class SubmitModel : PostModel, IJsonStorable
    {
        public string Picture { get; set;}
        
        public override string ToString() => JsonSerializer.Serialize<SubmitModel>(this);
        public string GetJsonName() => "items.json";

    }
}