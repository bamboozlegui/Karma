﻿using System;
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

        public Task<RequestPost> GetPost(int id);
        public Task<RequestPost> AddPost(RequestPost post, string userId);

        public Task<RequestPost> DeletePost(int id);
        public RequestPost MarkAsTaken(int id);

        public Task<RequestPost> UpdatePost(RequestPost newPost);
    }
}
