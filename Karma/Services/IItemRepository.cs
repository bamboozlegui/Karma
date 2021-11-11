using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Karma.Models;
using Microsoft.AspNetCore.Http;

namespace Karma.Services
{
    public interface IItemRepository
    {
        public Task<List<ItemPost>> GetPosts();

        public Task<List<ItemPost>> SearchPosts(string searchTerm);

        public Task<ItemPost> GetPost(int id);
        public Task<ItemPost> AddPost(ItemPost post, string userId);

        public Task<ItemPost> DeletePost(int id);

        public Task<ItemPost> UpdatePost(ItemPost newPost);

        public delegate void ItemPostedEventHandler(object source, EventArgs args);
        public event ItemPostedEventHandler ItemPosted;
    }
}
