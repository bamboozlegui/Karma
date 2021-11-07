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
    public class OutboxModel : PageModel
    {

        public OutboxModel(IMessageRepository messageService)
        {
            MessageService = messageService;
        }

        public List<Message> Outbox { get; set; }
        public IMessageRepository MessageService { get; }

        public async Task<IActionResult> OnGetAsync()
        {
            Outbox = (await MessageService.GetMessages()).Where(delegate (Message m)
            {
                return m.FromEmail == HttpContext.User.Identity.Name;
            }).ToList();
            return Page();
        }

        private void AddDummyMessages()
        {
            var dummyList = new List<Message>()
            {
                new Message() { Content = "heyheyhey"},
                new Message() { Content = "Whatsup"}
            };

            Outbox = dummyList;
        }
    }
}
