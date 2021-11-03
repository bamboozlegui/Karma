using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Data;
using Karma.Models;
using Karma.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Karma.Pages
{
    public class MyItemsModel : PageModel
    {
        public MyItemsModel(IItemRepository itemService)
        {
            ItemService = itemService;
        }
        public IEnumerable<ItemPost> Submits { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public IItemRepository ItemService { get; }

        public void OnGet()
        {
            Submits = ItemService.SearchPosts(SearchTerm).Where(i => i.Email == HttpContext.User.Identity.Name);
            
        }
    }
}
