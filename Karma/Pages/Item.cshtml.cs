using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Karma.Services;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;

namespace Karma.Pages
{
    public class ItemModel : PageModel
    {
        private IWebHostEnvironment WebHostEnvironment { get; }

        private IItemRepository ItemService { get; }
        public HttpClient HttpClient { get; }
        public IMessageRepository MessageService { get; }
        [BindProperty]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public ItemPost Item { get; set; }

        [BindProperty]
        public Message Message { get; set; }

        public ItemModel(
            HttpClient httpClient,
            IMessageRepository messageService)
        {
            HttpClient = httpClient;
            MessageService = messageService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Item = await RequestItemGetAsync(id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Item.Picture = await RequestPictureUpdateAsync();
            await HttpClient.PutAsJsonAsync<ItemPost>($"https://localhost:5001/api/items/{Item.Id}", Item);

            return RedirectToPage("/Submits");
        }

        public async Task<IActionResult> OnPostMessageAsync(int itemId)
        {
            Item = await RequestItemGetAsync(itemId);
            Message.FromEmail = User.Identity.Name;
            Message.ToEmail = Item.KarmaUser.Email;
            Message.Date = DateTime.Now;
            await MessageService.AddMessage(Message);

            return RedirectToPage("/Submits");
        }

        private async Task<ItemPost> RequestItemGetAsync(int id)
        {
            return Item = await HttpClient.GetFromJsonAsync<ItemPost>($"https://localhost:5001/api/items/{id}",
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                });

        }

        private async Task<string> RequestPictureUpdateAsync()
        {
            var newFileName = "";
            if (Photo != null) 
            {
                if (Item.Picture != null)
                {
                    await HttpClient.DeleteAsync($"https://localhost:5001/api/image/{Item.Picture}");
                }

                var fileName = ContentDispositionHeaderValue.Parse(Photo.ContentDisposition).FileName.Trim('"');

                using (var content = new MultipartFormDataContent())
                {
                    var photoContent = new StreamContent(Photo.OpenReadStream())
                    {
                        Headers =
                        {
                            ContentLength = Photo.Length,
                            ContentType = new MediaTypeHeaderValue(Photo.ContentType)
                        }
                    };

                    content.Add(photoContent, "File", fileName);
                    HttpResponseMessage response = await HttpClient.PostAsync($"https://localhost:5001/api/image", content);
                    newFileName = await response.Content.ReadAsStringAsync();
                }
            }
            return newFileName;
        }
    }
}

