using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Karma.Models
{
    [Index(nameof(Message.MessageId), IsUnique = true)]
    public class Message
    {
        [Required]
        [MaxLength(450)]
        public int MessageId { get; set; }
        [Required]
        [MaxLength(50)]
        public string FromEmail { get; set; }
        [Required]
        [MaxLength(50)]
        public string ToEmail { get; set; }
        [Required]
        [MaxLength(250)]
        public string Content { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

    }
}
