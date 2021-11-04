using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Data;
using Karma.Models;

namespace Karma.Services
{
    public class SQLMessageRepository : IMessageRepository
    {

        public SQLMessageRepository(KarmaDbContext context)
        {
            Context = context;
        }

        public KarmaDbContext Context { get; }

        public Message AddMessage(Message message)
        {
            Context.Messages.Add(message);
            Context.SaveChanges();
            return message;
        }

        public List<Message> GetMessages()
        {
            return Context.Messages.ToList();
        }
    }
}
