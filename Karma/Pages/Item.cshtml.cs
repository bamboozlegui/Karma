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

namespace Karma.Pages
{
    public class ItemModel : PageModel
    {
        private IWebHostEnvironment WebHostEnvironment { get; }

        private IItemRepository ItemService { get; }
        public HttpClient HttpClient { get; }
        public IMessageRepository MessageService { get; }
        public PictureService PictureService { get; }
        [BindProperty]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public ItemPost Item { get; set; }

        [BindProperty]
        public Message Message { get; set; }

        public ItemModel(
            IItemRepository itemService,
            HttpClient httpClient,
            IMessageRepository messageService,
            PictureService pictureService,
            IWebHostEnvironment webHostEnvironment)
        {
            ItemService = itemService;
            HttpClient = httpClient;
            MessageService = messageService;
            PictureService = pictureService;
            WebHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            Item = await HttpClient.GetFromJsonAsync<ItemPost>($"https://localhost:5001/api/items/{id}", options);

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if(Photo != null)
            {
                if (Item.Picture != null)
                {
                    string filePath = Path.Combine(WebHostEnvironment.WebRootPath, "images", Item.Picture);
                    System.IO.File.Delete(filePath);
                }

                Item.Picture = PictureService.ProcessUploadedFile(WebHostEnvironment.WebRootPath, Photo);
            }
		    Item = await ItemService.UpdatePost(Item);

            return RedirectToPage("/Submits");
        }

        public async Task<IActionResult> OnPostMessage(int itemId)
        {
            Item = await ItemService.GetPost(itemId);
            if (User.Identity != null) Message.FromEmail = User.Identity.Name;
            Message.ToEmail = Item.KarmaUser.Email;
            Message.Date = DateTime.Now;
            await MessageService.AddMessage(Message);

            return RedirectToPage("/Submits");
        }
    }
}

