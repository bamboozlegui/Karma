using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Karma.Models
{
    public class RequestModel : PostModel, IJsonStorable
    {
        public override string ToString() => JsonSerializer.Serialize<RequestModel>(this);

        public string GetJsonName() => "requests.json";
    }
}
