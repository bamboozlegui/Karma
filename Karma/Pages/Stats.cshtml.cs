using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Areas.Identity.Data;
using Karma.Data;
using Karma.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Karma.Pages
{
    public class StatsModel : PageModel
    {
        public StatsModel(KarmaDbContext context)
        {
            Context = context;
        }

        public KarmaDbContext Context { get; }

        public List<KarmaUser> Users { get; set; }
        public List<ItemPost> Items { get; set; }

        public void OnGet()
        {
            Users = Context.Users.OrderBy(u => u.KarmaPoints).Take(3).ToList();

        }
    }
}
