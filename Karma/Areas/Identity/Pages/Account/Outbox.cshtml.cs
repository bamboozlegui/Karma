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

        public OutboxModel(IOutboxManager inboxManager)
        {
            OutboxManager = inboxManager;
        }

        public IOutboxManager OutboxManager { get; }
        public List<OutboxMessage> Outbox { get; set; }

        public void OnGet()
        {
            Outbox = OutboxManager.GetMessages(HttpContext.User.Identity.Name);
            AddDummyMessages();
        }

        private void AddDummyMessages()
        {
            var dummyList = new List<OutboxMessage>()
            {
                new OutboxMessage() { Content = "heyheyhey"},
                new OutboxMessage() { Content = "Whatsup"}
            };

            Outbox = dummyList;
        }
    }
}
