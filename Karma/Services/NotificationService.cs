using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Data;
using Karma.Models;
using Microsoft.Extensions.Logging;

namespace Karma.Services
{
    public class NotificationService
    {

        private readonly ILogger<NotificationService> _logger;
        private readonly string notificationSystemEmail = "karmanotifier@karma.com";

        public NotificationService(ILogger<NotificationService> logger, IMessageRepository messageRepository, KarmaDbContext karmaDbContext)
        {
            _logger = logger;
            MessageRepository = messageRepository;
            KarmaDbContext = karmaDbContext;
        }

        public IMessageRepository MessageRepository { get; }
        public KarmaDbContext KarmaDbContext { get; }

        public async Task<Message> AddNotification(Post post)
        {
            var notification = new Message()
            {
                Content = $"Hey! You've posted succesfully! (POST_ID) = {post.Id})",
                FromEmail = notificationSystemEmail,
                ToEmail = post.KarmaUser.Email,
                Date = DateTime.Now
            };
            var sentMessage = await MessageRepository.AddMessage(notification);
            return sentMessage;
        }
    }
}
