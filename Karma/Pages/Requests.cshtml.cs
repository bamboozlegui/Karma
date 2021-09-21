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

        public JsonFileRequestService RequestService;   
        public IEnumerable<RequestModel> Requests { get; private set; }

        public RequestsModel(JsonFileRequestService requestService)
        {
            RequestService = requestService;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid == false)
            {
                return Page();
            }
            List<RequestModel> newRequest = new List<RequestModel>();
            newRequest.Add(Item);

            Requests = RequestService.GetRequests().
            Concat<RequestModel>(newRequest);

            RequestService.RefreshRequests(Requests);

            return RedirectToPage("/Index");
        }
    }
}
