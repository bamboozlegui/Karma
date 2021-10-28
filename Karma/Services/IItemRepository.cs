using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Models;
using Microsoft.AspNetCore.Http;

namespace Karma.Services
{
    public interface IItemRepository
    {
        public IEnumerable<ItemPost> GetPosts();

        public IEnumerable<ItemPost> SearchPosts(string searchTerm);

        public ItemPost GetPost(string id);
        public ItemPost AddPost(ItemPost post, IFormFile photo);

        public ItemPost DeletePost(string id);

        public ItemPost UpdatePost(ItemPost newPost, IFormFile newPhoto);
    }
}
