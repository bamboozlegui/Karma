using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Karma.Models
{
    public class RequestPost : Post, IJsonStorable
    {
        public override string ToString() => JsonSerializer.Serialize<RequestPost>(this);

        public string GetJsonName() => "requests.json";
    }
}
