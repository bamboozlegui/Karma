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

namespace Karma.Pages
{
    public class ItemModel : PageModel
    {
        private readonly ILogger<ItemModel> _logger;

        private IWebHostEnvironment WebHostEnvironment { get; }

        public ItemPost Item { get; set; }

        [BindProperty(SupportsGet = true)]
        public string itemJson { get; set; }

        public ItemModel(
            ILogger<ItemModel> logger,
            IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            WebHostEnvironment = webHostEnvironment;
        }

        public void OnGet()
        {
            try
            {
                ItemPost deserializedItem = JsonSerializer.Deserialize<ItemPost>(itemJson);
                if (deserializedItem.Picture == null)
                    deserializedItem.Picture = "noimage.jpg";
                Item = deserializedItem;
            }
            catch (JsonException)
            {
                Item = new ItemPost() { 
                    State   = Post.StateEnum.None,
                    Title   = "No such item",
                    Picture = "noimage.jpg",};
            }
        }
    }
}

