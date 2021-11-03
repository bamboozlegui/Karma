using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Data;
using Karma.Models;
using Microsoft.EntityFrameworkCore;

namespace Karma.Services
{
    public class OutboxManager : IOutboxManager
    {
        public OutboxManager(KarmaDbContext context)
        {
            Context = context;
        }
        public KarmaDbContext Context { get; }
        public List<OutboxMessage> GetMessages(string email)
        {
            var userInfo = Context.Users.Include(u => u.Outbox).FirstOrDefault(u => u.Email == email);
            var messages = userInfo.Outbox;

            return messages;
        }
    }
}
