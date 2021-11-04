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

        public void OnGet()
        {
            Outbox = MessageService.GetMessages().Where(delegate (Message m)
            {
                return m.FromEmail == HttpContext.User.Identity.Name;
            }).ToList();
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
