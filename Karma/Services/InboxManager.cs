using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Data;
using Karma.Models;
using Microsoft.EntityFrameworkCore;

namespace Karma.Services
{
    public class InboxManager : IInboxManager
    {
        public InboxManager(KarmaDbContext context)
        {
            Context = context;
        }
        public KarmaDbContext Context { get; }
        public List<InboxMessage> GetMessages(string email)
        {
            var userInfo = Context.Users.Include(u => u.Inbox).FirstOrDefault(u => u.Email == email);
            var messages = userInfo.Inbox;

            return messages;
        }
    }
}
