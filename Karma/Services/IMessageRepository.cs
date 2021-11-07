using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Models;

namespace Karma.Services
{
    public interface IMessageRepository
    {
        public Task<List<Message>> GetMessages();
        public Task<Message> AddMessage(Message message);
    }
}
