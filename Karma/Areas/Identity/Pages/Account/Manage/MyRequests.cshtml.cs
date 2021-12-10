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
using static Karma.Models.Fulfillment;

namespace Karma.Pages
{
    public class MyRequestsModel : PageModel
    {
        public MyRequestsModel(KarmaDbContext context, IFulfillmentRepository fulfillmentService, IRequestRepository requestService)
        {
            Context = context;
            FulfillmentService = fulfillmentService;
            RequestService = requestService;
        }
        public List<RequestPost> Requests { get; set; }
        public IRequestRepository RequestService { get; }
        public KarmaDbContext Context { get; }
        public IFulfillmentRepository FulfillmentService { get; }
        [BindProperty]
        public int fulfillmentId { get; set; }

        public async Task OnGet()
        {
            Requests = await RequestService.GetPosts();
        }

        public async Task<IActionResult> OnPostAccept()
        {
            var fulfillment = await FulfillmentService.GetFulfillmentAsync(fulfillmentId);
            var newFulfillment = new Fulfillment
            {
                Id = fulfillment.Id,
                Request = fulfillment.Request,
                Fulfiller = fulfillment.Fulfiller,
                State = FulfillmentEnum.Done
            };
            var updated = await FulfillmentService.UpdateFulfillmentAsync(newFulfillment);
            if (updated.State == FulfillmentEnum.Done)
                RequestService.MarkAsTaken(newFulfillment.Request.Id);
            return RedirectToPage("/MyRequests");
        }
    }
}
