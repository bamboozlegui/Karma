using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Karma.Models;
using Microsoft.AspNetCore.Identity;

namespace Karma.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the KarmaUser class
    public class KarmaUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName ="nvarchar(50)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string City { get; set; }

        public List<OutboxMessage> Outbox { get; set; } = new List<OutboxMessage>();
        public List<InboxMessage> Inbox { get; set; } = new List<InboxMessage>();
    }
}
