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
        public RequestModel Item { get; set; }

        public JsonFilePostService<RequestModel> RequestService;   
        public IEnumerable<RequestModel> Requests { get; private set; }

        public RequestsModel(JsonFilePostService<RequestModel> requestService)
        {
            RequestService = requestService;
        }
        public void OnGet()
        {
            Requests = RequestService.GetPosts();
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid == false)
            {
                return Page();
            }

            Requests = RequestService.GetPosts().
            Append<RequestModel>(Item);

            RequestService.RefreshPosts(Requests);

            return Page();
        }
    }
}
