using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Karma.Models;
using Microsoft.AspNetCore.Http;

namespace Karma.Services
{
    public interface IRequestRepository
    {
        public Task<List<RequestPost>> GetPosts();

        public Task<List<RequestPost>> SearchPosts(string searchTerm);

        public Task<RequestPost> GetPost(string id);
        public Task<RequestPost> AddPost(ClaimsPrincipal user, RequestPost post);

        public Task<RequestPost> DeletePost(string id);

        public Task<RequestPost> UpdatePost(RequestPost newPost);
    }
}
