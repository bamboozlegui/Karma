using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Karma.Models;
using Karma.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Karma.Areas.Identity.Pages.Account
{
    public class InboxModel : PageModel
    {

        public InboxModel(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
        public List<Message> Inbox { get; set; }
        public HttpClient HttpClient { get; }

        public async Task<IActionResult> OnGetAsync()
        {
            var email = User.Identity.Name;
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            var jsonString = await HttpClient.GetStringAsync($"https://localhost:5001/api/messages/{email}");
            Inbox = JsonSerializer.Deserialize<List<Message>>(jsonString, options);
            //Inbox = JsonSerializer.Deserialize<List<Message>>("[{\"MessageId\":4,\"FromEmail\":\"d@gmail.com\", \"ToEmail\":\"ayy@gmail\", \"Content\":\"Text\"}]");
            return Page();
        }
    }
}
