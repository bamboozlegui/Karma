using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Data;
using Karma.Models;
using Karma.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Karma.Pages
{
    public class MyRequestsModel : PageModel
    {
        public MyRequestsModel(IRequestRepository requestService)
        {
            RequestService = requestService;
        }
        public IEnumerable<RequestPost> Requests { get; set; }
        public IRequestRepository RequestService { get; }

        public void OnGet()
        {
            Requests = RequestService.GetPosts().Where(r => r.Email == HttpContext.User.Identity.Name);
        }
    }
}
