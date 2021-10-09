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

        private JsonFilePostService<ItemPost> ItemService { get; }

        [BindProperty]
        public IFormFile NewPhoto { get; set; }

        [BindProperty]
        public ItemPost NewItem { get; set; }
        public ItemPost Item { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ID { get; set; }

        public ItemModel(
            JsonFilePostService<ItemPost> itemService,
            IWebHostEnvironment webHostEnvironment)
        {
            ItemService = itemService;
            WebHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet()
        {
            Item = ItemService.GetPost(ID);

            if (Item.Picture == null)
            {
                Item.Picture = "noimage.jpg";
            }
            
            return Page();
        }

        public IActionResult OnPostEdit()
        {
            ItemService.UpdatePost(ItemService, NewItem, ID, NewPhoto);
            return RedirectToPage("/Submits");
        }
    }
}

