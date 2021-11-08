using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Karma.Models;
using Karma.Services;
using Microsoft.AspNetCore.Identity;
using Karma.Areas.Identity.Data;
using System.Web;
using Shared.Web.MvcExtensions;

namespace Karma.Pages
{
    public class RequestsModel : PageModel
    {

        [BindProperty]
        public RequestPost Item { get; set; }
        private IRequestRepository RequestService { get; }
        public IMessageRepository MessageService { get; }
        public List<RequestPost> Requests { get; private set; }

        [BindProperty]
        public Message Message { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public RequestsModel(IRequestRepository requestService, IMessageRepository messageService)
        {
            RequestService = requestService;
            MessageService = messageService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Requests = await RequestService.SearchPosts(SearchTerm);
            return Page();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            await RequestService.DeletePost(id);

            return RedirectToPage("/Requests");
        }

        public async Task<IActionResult> OnPostMessage(int itemId)
        {
            Item = await RequestService.GetPost(itemId);
            if (User.Identity != null) Message.FromEmail = User.Identity.Name;
            Message.ToEmail = Item.KarmaUser.Email;
            Message.Date = DateTime.Now;
            await MessageService.AddMessage(Message);
            
            return RedirectToPage("/Requests");
        }

        public async Task<IActionResult> OnPost()
        {
            var userId = User.GetUserId();
            await RequestService.AddPost(Item, userId);

            return RedirectToPage("/Requests");
        }
    }
}
