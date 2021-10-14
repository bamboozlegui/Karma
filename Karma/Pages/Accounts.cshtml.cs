using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Models;
using Karma.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Karma.Pages
{
    public class AccountsModel : PageModel
    {

        private JsonFileRequestService RequestService { get; }

        private JsonFileItemService ItemService { get; }

        public IEnumerable<RequestPost> Requests { get; private set; }

        public IEnumerable<ItemPost> Submits { get; private set; }

        public IEnumerable<Account> Accounts { get; private set; }

        public AccountsModel(JsonFileRequestService requestService, JsonFileItemService itemService)
        {
            RequestService = requestService;
            ItemService = itemService;
        }

        public void OnGet()
        {
            Accounts = GetDummyAccounts();
            Submits = ItemService.GetPosts();
        }

        public static List<Account> GetDummyAccounts()
        {
            return new List<Account>()
        {
				new Account { Name = "Karolis"},
				new Account { Name = "Ignas"},
				new Account { Name = "Edvin"},
				new Account { Name = "Boy"}

        };


        }
    }
}
