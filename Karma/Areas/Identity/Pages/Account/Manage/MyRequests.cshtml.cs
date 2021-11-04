using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Areas.Identity.Data;
using Karma.Data;
using Karma.Models;
using Karma.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Karma.Pages
{
    public class MyRequestsModel : PageModel
    {
        public MyRequestsModel(KarmaDbContext context)
        {
            Context = context;
        }
        public IEnumerable<RequestPost> Requests { get; set; }
        public IRequestRepository RequestService { get; }
        public KarmaDbContext Context { get; }

        public void OnGet()
        {
            Requests = Context.Users.Include(u => u.Requests).FirstOrDefault(u => u.Email == User.Identity.Name).Requests;
        }
    }
}
