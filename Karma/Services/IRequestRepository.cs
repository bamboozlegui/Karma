using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Models;
using Microsoft.AspNetCore.Http;

namespace Karma.Services
{
    public interface IRequestRepository
    {
        public IEnumerable<RequestPost> GetPosts();

        public IEnumerable<RequestPost> SearchPosts(string searchTerm);

        public RequestPost GetPost(string id);
        public RequestPost AddPost(RequestPost post);

        public RequestPost DeletePost(string id);

        public RequestPost UpdatePost(RequestPost newPost);
    }
}
