using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Karma.Models;
using Karma.Services;

namespace Karma.Pages
{
    public class SubmitsModel : PageModel
    {
        [BindProperty]
        public SubmitModel Item { get; set; }

        public JsonFilePostService<SubmitModel> SubmitService;
        public IEnumerable<SubmitModel> Submits { get; private set; }

        public SubmitsModel(JsonFilePostService<SubmitModel> submitService)
        {
            SubmitService = submitService;
        }
        public void OnGet()
        {
            Submits = SubmitService.GetPosts();
        }


        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            Submits = SubmitService.GetPosts().
            Append<SubmitModel>(Item);

            SubmitService.RefreshPosts(Submits);

            return Page();
        }
    }

}
