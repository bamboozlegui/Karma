using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Karma.Models;
using Karma.Services;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("to/{email}")]
        public async Task<ActionResult<Message>> GetMessagesTo(string email)
        {
            try
            {
                var messages = await MessageService.GetMessagesToEmail(email);
                return Ok(messages);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("from/{email}")]
        public async Task<ActionResult<Message>> GetMessagesFrom(string email)
        {
            try
            {
                var messages = await MessageService.GetMessagesFromEmail(email);
                return Ok(messages);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
    }
}
