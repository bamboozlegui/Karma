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
        private readonly UserManager<KarmaUser> _userManager;
        private readonly SignInManager<KarmaUser> _signInManager;

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
            IWebHostEnvironment webHostEnvironment,
            UserManager<KarmaUser> userManager,
            SignInManager<KarmaUser> signInManager
            )
        {
            ItemService = itemService;
            PictureService = pictureService;
            WebHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Submits = await ItemService.SearchPosts(SearchTerm);
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
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

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            user.KarmaPoints += 1;
            KarmaUser.ProcessKarmaBalance(user.IncreaseKarmaPoints, 5);
            await _userManager.UpdateAsync(user);
            Console.Out.WriteLine(user.KarmaPoints);


            return RedirectToPage("/Submits");
        }

        public async Task<KarmaUser> GetCurrentUser()
        {
            return null;
        }
    }
}
