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

        public IActionResult OnGet(string ID)
        {
            Item = ItemService.GetPost(ID);

            return Page();
        }

        public IActionResult OnPost(ItemPost item)
        {
	        if(ModelState.IsValid)
		        Item = ItemService.UpdatePost(item, Photo);

            return RedirectToPage("/Submits");
        }

        public IActionResult OnPostMessage(string itemId)
        {
            Item = ItemService.GetPost(itemId);
            Message.FromEmail = User.Identity.Name;
            Message.ToEmail = Item.Email;
            Message.Date = DateTime.Now;
            MessageService.AddMessage(Message);

            return RedirectToPage("/Submits");
        }
    }
}

