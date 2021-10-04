using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Models;
using Karma.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Karma.Pages
{
    public class IndexModel : PageModel
    {
        //Request dalykai
        [BindProperty]
        public RequestModel RequestItem { get; set; }
        public JsonFilePostService<RequestModel> RequestService;
        public IEnumerable<RequestModel> Requests { get; private set; }
        
        //Submit dalykai
        [BindProperty]
        public SubmitModel SubmitItem { get; set; }
        [BindProperty]
        public IFormFile Photo { get; set; }
        public JsonFilePostService<SubmitModel> SubmitService;

        public IEnumerable<SubmitModel> Submits { get; private set; }

        public IndexModel(JsonFilePostService<RequestModel> requestService, JsonFilePostService<SubmitModel> submitService)
        {
            RequestService = requestService;
            SubmitService = submitService;
        }


        private readonly ILogger<IndexModel> _logger;

        public void OnGet()
        {
            Requests = RequestService.GetPosts();
            Submits = SubmitService.GetPosts();
        }
    }
}
