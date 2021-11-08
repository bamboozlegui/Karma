using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Karma.Models;
using Karma.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Threading;
using Microsoft.AspNetCore.Identity;
using Karma.Areas.Identity.Data;
using Karma.Data;
using Shared.Web.MvcExtensions;
namespace Karma.Pages
{
    public class SubmitsModel : PageModel
    {

        [BindProperty]
        public ItemPost Item { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty]
        public string SelectedCategory { get; set; }

        public IItemRepository ItemService { get; set; }
        public PictureService PictureService { get; }
        private IWebHostEnvironment WebHostEnvironment { get; }

        public List<ItemPost> Submits { get; private set; }

        public SubmitsModel(
            IItemRepository itemService,
            PictureService pictureService,
            IWebHostEnvironment webHostEnvironment)
        {
            ItemService = itemService;
            PictureService = pictureService;
            WebHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Submits = await ItemService.SearchPosts(SearchTerm);
            return Page();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var item = await ItemService.GetPost(id);
            PictureService.DeletePicture(WebHostEnvironment.WebRootPath, item.Picture);
            await ItemService.DeletePost(id);

            return RedirectToPage("/Submits");
        }
        // TO-DO implement filter by Category, get info from checkbox
        // public List<string> Categories = new List<string>(Post.SCategories);
        public IActionResult OnPostFilter(string sCategory)
        {
            //Submits = Submits.Where(x => x.Category == SelectedCategory);
            return RedirectToPage("/Submits");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Item.Picture = PictureService.ProcessUploadedFile(WebHostEnvironment.WebRootPath, Photo); //Check definition
            var userId = User.GetUserId();
            await ItemService.AddPost(Item, userId);

            return RedirectToPage("/Submits");
        }
    }
}
