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

        public SqlRequestRepository(KarmaDbContext context)
        {
            Context = context;
        }
        public async Task<RequestPost> AddPost(RequestPost post, string userId)
        {
            post.Date = DateTime.Now;
            post.State = Post.StateEnum.Recent;
            post.KarmaUser = await Context.Users.FindAsync(userId);
            if (post.KarmaUser == null)
                return null;
            await Context.Requests.AddAsync(post);
            await Context.SaveChangesAsync();
            return post;
        }

        public async Task<RequestPost> DeletePost(int id)
        {
            RequestPost request = await Context.Requests.FindAsync(id);

            if (request == null) return null;

            Context.Requests.Remove(request);
            await Context.SaveChangesAsync();
            return request;
        }

        public async Task<RequestPost> GetPost(int id)
        {
            return await Context.Requests.Include(r => r.KarmaUser).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<RequestPost>> GetPosts()
        {
            return await Context.Requests.Include(r => r.KarmaUser).ToListAsync();
        }

        public async Task<List<RequestPost>> SearchPosts(string searchTerm)
        {
            if (searchTerm == null)
                return await GetPosts();

            return await Context.Requests.Include(r => r.KarmaUser).Where(request => request.Title.Contains(searchTerm)).ToListAsync();
        }

        public Task<RequestPost> UpdatePost(RequestPost newPost)
        {
            throw new NotImplementedException();
        }
    }
}
