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
        public RequestPost RequestItem { get; set; }

        private IRequestRepository RequestService { get;  }

        public List<RequestPost> Requests { get; private set; }
        
        //Submit dalykai
        [BindProperty]
        public ItemPost SubmitItem { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        private IItemRepository ItemService { get;  }

        public List<ItemPost> Submits { get; private set; }

        public IndexModel(IRequestRepository requestService, IItemRepository itemService)
        {
            RequestService = requestService;
            ItemService = itemService;
        }
        
        public async Task<IActionResult> OnGetAsync()
        {
            Requests = await RequestService.GetPosts();
            Submits = await ItemService.GetPosts();
            return Page();
        }
    }
}
