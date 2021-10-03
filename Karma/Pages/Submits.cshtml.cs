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
        public SubmitModel Item { get; set; }
        
        [BindProperty]
        public IFormFile Photo { get; set; }

        public JsonFilePostService<SubmitModel> SubmitService;

        private IWebHostEnvironment WebHostEnvironment { get; }

        public IEnumerable<SubmitModel> Submits { get; private set; }

        public SubmitsModel(
            JsonFilePostService<SubmitModel> submitService,
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
            Submits = SubmitService.GetPosts().ToList();
            
            Submits = Submits.Where(x => x.Picture != Picture).ToList();
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
                    string filePath = Path.Combine(WebHostEnvironment.WebRootPath,
                       "images", Item.Picture);
                    System.IO.File.Delete(filePath);
                }
                Item.Picture = ProcessUploadedFile(); //Check definition
            }

            Submits = SubmitService.GetPosts().
            Append<SubmitModel>(Item);

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
