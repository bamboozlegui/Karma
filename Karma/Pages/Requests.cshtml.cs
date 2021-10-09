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

        public JsonFileRequestService RequestService;   

        public IEnumerable<RequestPost> Requests { get; private set; }

        public RequestsModel(JsonFileRequestService requestService)
        {
            RequestService = requestService;
        }

        public void OnGet()
        {
            Requests = RequestService.GetPosts();
        }

        public IActionResult OnPostDelete(string id)
        {
            RequestService.DeletePost(id);

            return RedirectToPage("/Requests");

        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid == false)
            {
                return Page();
            }

            RequestService.AddPost(Item);

            return RedirectToPage("/Requests");
        }
    }
}
