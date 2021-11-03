using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Karma.Models
{
    public class OutboxMessage : Message
    {
        [Required]
        [MaxLength(50)]
        public string ToEmail { get; set; }

    }
}
