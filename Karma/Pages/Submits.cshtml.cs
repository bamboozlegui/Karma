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
        public NotificationService NotificationService { get; }
        public KarmaPointService KarmaPointService { get; }
        public IItemRepository ItemService { get; set; }
        public PictureService PictureService { get; }
        private IWebHostEnvironment WebHostEnvironment { get; }

        public List<ItemPost> Submits { get; private set; }

        public SubmitsModel(
            NotificationService notificationService,
            KarmaPointService karmaPointService,
            IItemRepository itemService,
            PictureService pictureService,
            IWebHostEnvironment webHostEnvironment
            )
        {
            NotificationService = notificationService;
            KarmaPointService = karmaPointService;
            ItemService = itemService;
            PictureService = pictureService;
            WebHostEnvironment = webHostEnvironment;

        }

        public async Task<IActionResult> OnGetAsync()
        {
            Submits = await ItemService.SearchPosts(SearchTerm);
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var userId = User.GetUserId();

            Func<int, int> Subtract = (x => x - 6);
            await KarmaPointService.ProcessKarmaBalanceAsync(userId, Subtract);

            var item = await ItemService.GetPost(id);

            PictureService.DeletePicture(WebHostEnvironment.WebRootPath, item.Picture);
            await ItemService.DeletePost(id);

            return RedirectToPage("/Submits");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.GetUserId();

            Func<int, int> Add = (x => x + 6);
            await KarmaPointService.ProcessKarmaBalanceAsync(userId, Add);

            Item.Picture = PictureService.ProcessUploadedFile(WebHostEnvironment.WebRootPath, Photo); //Check definition
            var postedItem = await ItemService.AddPostAsync(Item, userId);

            await NotificationService.AddNotification(postedItem);

            return RedirectToPage("/Submits");
        }
    }
}
