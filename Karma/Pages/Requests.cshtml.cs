using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Karma.Models;
using Karma.Services;
using Microsoft.AspNetCore.Identity;
using Karma.Areas.Identity.Data;
using System.Web;

namespace Karma.Pages
{
    public class RequestsModel : PageModel
    {

        [BindProperty]
        public RequestPost Item { get; set; }

        private IRequestRepository RequestService { get; }
        public IMessageRepository MessageService { get; }
        public IEnumerable<RequestPost> Requests { get; private set; }

        [BindProperty]
        public Message Message { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public string SqlConnectionString = "Server=(localdb)\\mssqllocaldb;Database=Karma;Trusted_Connection=True;MultipleActiveResultSets=true";


        public RequestsModel(IRequestRepository requestService, IMessageRepository messageService)
        {
            RequestService = requestService;
            MessageService = messageService;
        }

        public void OnGet()
        {
            Requests = RequestService.SearchPosts(SearchTerm);
        }

        public IActionResult OnPostDelete(string id)
        {
            RequestService.DeletePost(id);

            return RedirectToPage("/Requests");
        }

        public IActionResult OnPostMessage(string itemId)
        {
            Item = RequestService.GetPost(itemId);
            if (User.Identity != null) Message.FromEmail = User.Identity.Name;
            Message.ToEmail = Item.Email;
            Message.Date = DateTime.Now;
            MessageService.AddMessage(Message);
            
            return RedirectToPage("/Requests");
        }

        public IActionResult OnPost()
        {
            using (SqlConnection conn = new SqlConnection(SqlConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT UserName, FirstName, City, PhoneNumber FROM Karma.dbo.AspNetUsers", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (HttpContext.User.Identity != null && HttpContext.User.Identity.Name == reader.GetString(0))
                        {
                            Item.Email = reader.GetString(0);
                            Item.PosterName = reader.GetString(1);
                            Item.City = reader.GetString(2);
                            Item.PhoneNumber = reader.GetString(3);
                            break;
                        }
                    }
                }
                reader.Close();
                conn.Close();
            }

            RequestService.AddPost(HttpContext.User, Item);

            return RedirectToPage("/Requests");
        }
    }
}
