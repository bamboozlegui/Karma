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

        public string SqlConnectionString = "Server=(localdb)\\mssqllocaldb;Database=Karma;Trusted_Connection=True;MultipleActiveResultSets=true";

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

        // Deletes Post on button trigger, refreshes posts afterwards : )
        public async Task<IActionResult> OnPostDelete(string id)
        {
            var item = await ItemService.GetPost(id);
            PictureService.DeletePicture(WebHostEnvironment, item.Picture);
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
            Thread.Sleep(5000);
            /*if (ModelState.IsValid == false)
            {
                return Page();
            }*/

            using (SqlConnection conn = new SqlConnection(SqlConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT UserName, FirstName, City, PhoneNumber, Id FROM Karma.dbo.AspNetUsers", conn);
                conn.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (HttpContext.User.Identity != null && HttpContext.User.Identity.Name == reader.GetString(0))
                        {
                            Item.Email = reader.GetString(0);
                            Item.PosterName = reader.GetString(1);
                            Item.City = reader.GetString(2);
                            Item.PhoneNumber = reader.GetString(3);
                            Item.KarmaUserId = reader.GetString(4);
                            break;
                        }
                    }
                }
                reader.Close();
                conn.Close();
            }

            Item.Picture = PictureService.ProcessUploadedFile(WebHostEnvironment, Photo); //Check definition

            await ItemService.AddPost(Item);


            return RedirectToPage("/Submits");
        }

        /*
        public IActionResult Validate(ItemPost Item)
        {
            if (!Regex.Match(Item.PosterName, "^[A-Z][a-zA-Z]*$").Success)
            {
                ItemService.DeletePost(Item.ID);
                return RedirectToPage("/Error");
            }   

            return RedirectToPage("/Accounts");

        }*/
    }
}
