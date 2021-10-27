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

        private JsonFileRequestService RequestService { get; }

        public IEnumerable<RequestPost> Requests { get; private set; }
        
        public string sqlConnectionString = "Server=(localdb)\\mssqllocaldb;Database=Karma;Trusted_Connection=True;MultipleActiveResultSets=true";


        public RequestsModel(JsonFileRequestService requestService)
        {
            RequestService = requestService;
        }

        public void OnGet()
        {
            Requests = RequestService.GetPosts();



            // Further testing required
            using(SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT UserName, FirstName FROM Karma.dbo.AspNetUsers", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (HttpContext.User.Identity.Name == reader.GetString(0))
                        {
                            System.Diagnostics.Debug.WriteLine(reader.GetString(1));
                            break;
                        }
                    }
                }              
            }


        }

        public IActionResult OnPostDelete(string id)
        {
            RequestService.DeletePost(id);

            return RedirectToPage("/Requests");
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid == false)
            {
                return Page();
            }

            RequestService.AddPost(Item);

            return RedirectToPage("/Requests");
        }
    }
}
