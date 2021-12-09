using Karma.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Karma.Models
{
    [Index(nameof(Fulfillment.Id), IsUnique = true)]
    public class Fulfillment
    {
        public enum FulfillmentEnum
        {
            InProgress = 0,
            Done = 1,
        }

        [Required]
        public int Id { get; set; }
        [Required]
        public KarmaUser Fulfiller { get; set; }
        [Required]
        public RequestPost Request { get; set; }
        [Required]
        public  FulfillmentEnum State { get; set; }
    }
}
