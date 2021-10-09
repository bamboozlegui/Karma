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
        public IActionResult OnPostDelete(string id)
        {
            SubmitService.DeletePost(SubmitService, id);

            return RedirectToPage("/Submits");
        }


        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            SubmitService.AddPost(SubmitService, Item, Photo);
                
                
            return RedirectToPage("/Submits");
        }

        //Uploads the parsed pic into ./wwwroot/images/ 
        //Returns uniqueFileName string - a random ID + file name

    }
}
