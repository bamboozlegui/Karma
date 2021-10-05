using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Services;
using Karma.Models;

namespace Karma.Pages
{
    public class GetRequestsModel : PageModel
    {
        private readonly ILogger<RequestPost> _logger;
        public JsonFilePostService<RequestPost> RequestService;   
        public IEnumerable<RequestPost> Requests { get; private set; }

        public GetRequestsModel(
            ILogger<RequestPost> logger,
            JsonFilePostService<RequestPost> requestService)
        {
            _logger = logger;
            RequestService = requestService;
        }

        public void OnGet()
        {
            Requests = RequestService.GetPosts();
        }
    }
}