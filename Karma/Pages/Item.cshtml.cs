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

        private JsonFileItemService ItemService { get; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public ItemPost Item { get; set; }

        public ItemModel(
            JsonFileItemService itemService,
            IWebHostEnvironment webHostEnvironment)
        {
            ItemService = itemService;
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
    }
}

