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

namespace Karma.Pages
{
    public class SubmitsModel : PageModel
    {

        [BindProperty]
        public ItemPost Item { get; set; }   
        
        [BindProperty]
        public IFormFile Photo { get; set; }

        public JsonFilePostService<ItemPost> SubmitService;

        private IWebHostEnvironment WebHostEnvironment { get; }

        public IEnumerable<ItemPost> Submits { get; private set; }

        public SubmitsModel(
            JsonFilePostService<ItemPost> submitService,
            IWebHostEnvironment webHostEnvironment)
        {
            SubmitService = submitService;
            WebHostEnvironment = webHostEnvironment;    
        }
        public void OnGet()
        {
            Submits = SubmitService.GetPosts();
        }

        // Deletes Post on button trigger, refreshes posts afterwards : )
        public IActionResult OnPostDelete(string Picture)
        {
            Submits = SubmitService.GetPosts();

            string filePath = Path.Combine(WebHostEnvironment.WebRootPath, "images", Picture);
            System.IO.File.Delete(filePath);
            
            Submits = Submits.Where(x => x.Picture != Picture);
            SubmitService.RefreshPosts(Submits);
            return RedirectToPage("/Submits");
        }


        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            if (Photo != null)
            {
                if (Item.Picture != null) //If our Item already has a picture path string, we should delete it first to upload a new one
                {
                    string filePath = Path.Combine(WebHostEnvironment.WebRootPath, "images", Item.Picture);
                    System.IO.File.Delete(filePath);
                }

                Item.Picture = ProcessUploadedFile(); //Check definition
            }
	        else
	        {
		        Item.Picture = "noimage.jpg";
	        }
                Item.Date = DateTime.Now;

                Submits = SubmitService.GetPosts().
                Append<ItemPost>(Item);

	            Submits = Submits.OrderByDescending(item => item.State).ThenByDescending(item => item.Title);

                SubmitService.RefreshPosts(Submits);

                return RedirectToPage("/Submits");
            }

        //Uploads the parsed pic into ./wwwroot/images/ 
        //Returns uniqueFileName string - a random ID + file name
        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;

            if (Photo != null)
            {
                string uploadsFolder =
                    Path.Combine(WebHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
