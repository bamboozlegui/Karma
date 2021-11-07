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

namespace Karma.Pages
{
    public class ItemModel : PageModel
    {
        private IWebHostEnvironment WebHostEnvironment { get; }

        private IItemRepository ItemService { get; }
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
            IMessageRepository messageService,
            PictureService pictureService,
            IWebHostEnvironment webHostEnvironment)
        {
            ItemService = itemService;
            MessageService = messageService;
            PictureService = pictureService;
            WebHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> OnGetAsync(string ID)
        {
            Item = await ItemService.GetPost(ID);

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

        public async Task<IActionResult> OnPostMessage(string itemId)
        {
            Item = await ItemService.GetPost(itemId);
            if (User.Identity != null) Message.FromEmail = User.Identity.Name;
            Message.ToEmail = Item.Email;
            Message.Date = DateTime.Now;
            await MessageService.AddMessage(Message);

            return RedirectToPage("/Submits");
        }
    }
}

