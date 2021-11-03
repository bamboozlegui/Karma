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

        public InboxModel(IInboxManager inboxManager)
        {
            InboxManager = inboxManager;
        }

        public IInboxManager InboxManager { get; }
        public List<InboxMessage> Inbox { get; set; }

        public void OnGet()
        {
            Inbox = InboxManager.GetMessages(HttpContext.User.Identity.Name);
            AddDummyMessages();
        }

        private void AddDummyMessages()
        {
            var dummyList = new List<InboxMessage>()
            {
                new InboxMessage() { Content = "heyheyhey"},
                new InboxMessage() { Content = "Whatsup"}
            };

            Inbox = dummyList;
        }
    }
}
