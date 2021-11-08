using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Models;
using Karma.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Karma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        public MessagesController(IMessageRepository messageService)
        {
            MessageService = messageService;
        }

        public IMessageRepository MessageService { get; }

        [HttpGet("{email}")]
        public async Task<ActionResult<Message>> Get(string email)
        {
            var messages = await MessageService.GetMessagesByEmail(email);
            return Ok(messages);
        }
    }
}
