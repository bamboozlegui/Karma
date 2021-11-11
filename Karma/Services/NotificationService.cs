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

        public void OnPosted(object source, PostedEventArgs args)
        {
            var notification = new Message()
            {
                Content = $"Hey! You've posted succesfully! (POST_ID) = {args.Post.Id})",
                FromEmail = notificationSystemEmail,
                ToEmail = args.UserEmail,
                Date = DateTime.Now
            };
            MessageRepository.AddMessage(notification);
        }
    }
}
