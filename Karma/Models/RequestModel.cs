using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karma.Models
{
    public class RequestModel
    {
        public string RequestName { get; set; }
        public string Description { get; set; }

        public string BuyerName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
    }
}
