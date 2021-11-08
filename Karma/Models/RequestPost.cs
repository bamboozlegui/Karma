using System.Text.Json;

namespace Karma.Models
{
    public class RequestPost : Post
    {
        public string CollapseId()
        {
            return "collapse" + Id;
        }
    }
}
