using Karma.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Karma.Models
{
    [Index(nameof(Giveaway.Id), IsUnique = true)]
    public class Giveaway
    {
        public enum GiveawayEnum
        {
            InProgress = 0,
            Done = 1,
        }
        [Required]
        public int Id { get; set; }

        [Required]
        public KarmaUser Receiver { get; set; }
        [Required]
        public ItemPost Item { get; set; }
        [Required]
        public GiveawayEnum State { get; set; }
    }
}
