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
    public class RequestsModel : PageModel
    {
        [BindProperty]
        public RequestPost Item { get; set; }

        public JsonFilePostService<RequestPost> RequestService;   
        public IEnumerable<RequestPost> Requests { get; private set; }

        public RequestsModel(JsonFilePostService<RequestPost> requestService)
        {
            RequestService = requestService;
        }
        public void OnGet()
        {
            Requests = RequestService.GetPosts();
        }

        public IActionResult OnPostDelete(string Description)
        {
            Requests = RequestService.GetPosts();

            Requests = Requests.Where(x => x.Description != Description);
            RequestService.RefreshPosts(Requests);

            return RedirectToPage("/Requests");


        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid == false)
            {
                return Page();
            }

            Requests = RequestService.GetPosts().
            Append<RequestPost>(Item);

            RequestService.RefreshPosts(Requests);

            return RedirectToPage("/Requests");
        }
    }
}
