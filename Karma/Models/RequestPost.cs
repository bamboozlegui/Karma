using System.Text.Json;

namespace Karma.Models
{
    public class RequestPost : Post, IJsonStorable
    {
        public override string ToString() => JsonSerializer.Serialize<RequestPost>(this);

        public string GetJsonName() => "requests.json";
    }
}
