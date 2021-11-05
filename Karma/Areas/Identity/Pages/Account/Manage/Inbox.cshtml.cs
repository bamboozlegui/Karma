using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Models;
using Karma.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Karma.Areas.Identity.Pages.Account
{
    public class InboxModel : PageModel
    {

        public InboxModel(IMessageRepository messageService)
        {
            MessageService = messageService;
        }
        public List<Message> Inbox { get; set; }
        public IMessageRepository MessageService { get; }

        public async Task<IActionResult> OnGet()
        {
            Inbox = (await MessageService.GetMessages()).Where(m => m.ToEmail == HttpContext.User.Identity.Name).ToList();
            return Page();
        }

        private void AddDummyMessages()
        {
            var dummyList = new List<Message>()
            {
                new Message() { Content = "heyheyhey", FromEmail = HttpContext.User.Identity.Name},
                new Message() { Content = "Whatsup", FromEmail = HttpContext.User.Identity.Name}
            };

            Inbox = Inbox.Concat<Message>(dummyList).ToList();
        }
    }
}
