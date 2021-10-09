using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Karma.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Karma.Services
{
    public class JsonFileRequestService : JsonFilePostService<RequestPost>
    {
        public JsonFileRequestService(IWebHostEnvironment webHostEnvironment) : base(webHostEnvironment) {}

        internal override string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.ContentRootPath, "data", (new RequestPost()).GetJsonName()); }
        }

        public override void AddPost(RequestPost post, IFormFile photo = null)
        {
            post.Date = DateTime.Now;
            post.ID = Guid.NewGuid().ToString();

            _posts = _posts.Append<RequestPost>(post);
            _posts = _posts.OrderByDescending(post => post.State).ThenByDescending(post => post.Title);

            RefreshJsonFile();
        }

        public override void DeletePost(string id)
        {
            RequestPost post = _posts.FirstOrDefault<RequestPost>(post => post.ID == id);

	    _posts = _posts.Where(post => post.ID != id);

	    RefreshJsonFile();
        }

        public override RequestPost UpdatePost(RequestPost newPost)
        {
            throw new System.NotImplementedException();
        }
    }
}
