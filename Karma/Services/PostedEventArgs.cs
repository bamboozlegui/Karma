using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Models;

namespace Karma.Services
{
    public class PostedEventArgs : EventArgs
    {
        public Post Post { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
    }
}
