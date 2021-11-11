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
        [PersonalData]
        [Column(TypeName = "int")]
        public int KarmaPoints { get; set; }
       
        public List<Message> Messages { get; set; } = new List<Message>();
        public List<RequestPost> Requests  { get; set; } = new List<RequestPost>();
        public List<ItemPost> Items { get; set; } = new List<ItemPost>();
    }
}
