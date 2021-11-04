using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Data;
using Karma.Models;
using Karma.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Karma.Pages
{
    public class MyItemsModel : PageModel
    {
        public MyItemsModel(KarmaDbContext context)
        {
            Context = context;
        }
        public IEnumerable<ItemPost> Submits { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public KarmaDbContext Context { get; }

        public void OnGet()
        {
            Submits = Context.Users.Include(u => u.Items).FirstOrDefault(u => u.Email == User.Identity.Name).Items;
        }
    }
}
