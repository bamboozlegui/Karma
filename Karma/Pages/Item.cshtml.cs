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
        [BindProperty]
        public IFormFile Photo { get; set; }

        public ItemPost Item { get; set; }

        [BindProperty]
        public Message Message { get; set; }

        public ItemModel(
            IItemRepository itemService,
            IMessageRepository messageService,
            IWebHostEnvironment webHostEnvironment)
        {
            ItemService = itemService;
            MessageService = messageService;
            WebHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> OnGetAsync(string ID)
        {
            Item = await ItemService.GetPost(ID);

            return Page();
        }

        public async Task<IActionResult> OnPost(ItemPost item)
        {
		    Item = await ItemService.UpdatePost(item, Photo);

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

