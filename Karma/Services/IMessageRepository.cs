using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Models;

namespace Karma.Services
{
    public interface IMessageRepository
    {
        List<Message> GetMessages();
        Message AddMessage(Message message);
    }
}
