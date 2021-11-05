using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Models;
using Microsoft.AspNetCore.Hosting;
using Karma.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Karma.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace Karma.Services
{
    public class SqlRequestRepository : IRequestRepository
    {
        public KarmaDbContext Context { get; }
        public UserManager<KarmaUser> UserManager { get; }
        public IWebHostEnvironment WebHostEnvironment { get; private set; }

        public SqlRequestRepository(KarmaDbContext context, UserManager<KarmaUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            Context = context;
            UserManager = userManager;
            WebHostEnvironment = webHostEnvironment;
        }
        public async Task<RequestPost> AddPost(ClaimsPrincipal user, RequestPost post)
        {
            post.Date = DateTime.Now;
            post.ID = Guid.NewGuid().ToString();
            post.State = Post.StateEnum.Recent;
            post.KarmaUserId = UserManager.GetUserId(user);
            await Context.Requests.AddAsync(post);
            await Context.SaveChangesAsync();
            return post;
        }

        public async Task<RequestPost> DeletePost(string id)
        {
            RequestPost request = await Context.Requests.FindAsync(id);

            if (request == null) return null;

            Context.Requests.Remove(request);
            await Context.SaveChangesAsync();
            return request;
        }

        public async Task<RequestPost> GetPost(string id)
        {
            return await Context.Requests.FindAsync(id);
        }

        public async Task<List<RequestPost>> GetPosts()
        {
            return await Context.Requests.ToListAsync();
        }

        public async Task<List<RequestPost>> SearchPosts(string searchTerm)
        {
            if (searchTerm == null)
                return await Context.Requests.ToListAsync();

            return await Context.Requests.Where(request => request.Title.Contains(searchTerm)).ToListAsync();
        }

        public Task<RequestPost> UpdatePost(RequestPost newPost)
        {
            throw new NotImplementedException();
        }
    }
}
